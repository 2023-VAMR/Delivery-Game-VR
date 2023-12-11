using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    private UIManager UM;

    [NonSerialized]
    public PlayerController player;

    [SerializeField] private Transform respawnPoint;

    [SerializeField] private GameObject foodPointsContainer;
    private DeliveryPoint[] _foods;
    [SerializeField] private GameObject destinationPointsContainer;
    private DeliveryPoint[] _destinations;

    [SerializeField] private float orderAdditionThreshold = 60;
    private float _orderAdditionCount = 0;

    public bool isProgessOrder
    {
        get; 
        private set;
    } = false;
    private OrderData _inProgressOrderData = null;
    private readonly static string inProgressOrderDataAddress = "Assets/Data/OrderData/InProgressOrderData.asset";

    private PlayerData _playerData = null;
    private readonly static string playerDataAddress = "Assets/Data/Player/PlayerData.asset";
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        UM = UIManager.Instance;

        _foods = foodPointsContainer.GetComponentsInChildren<DeliveryPoint>();
        _destinations = destinationPointsContainer.GetComponentsInChildren<DeliveryPoint>();

        Addressables.LoadAssetAsync<OrderData>(inProgressOrderDataAddress).Completed += (handle) => _inProgressOrderData = handle.Result;
        Addressables.LoadAssetAsync<PlayerData>(playerDataAddress).Completed += (handle) => _playerData = handle.Result;

        _orderAdditionCount = orderAdditionThreshold - 5;
    }

    private void FixedUpdate()
    {
        _orderAdditionCount += Time.deltaTime;
        if(_orderAdditionCount > orderAdditionThreshold )
        {
            AddNewOrder();
            _orderAdditionCount = 0;
        }
        if (isProgessOrder)
        {
            _inProgressOrderData.progressTime += Time.deltaTime;
        }
    }

    public void RespawnPlayer()
    {
        _playerData.coin += -1000;
        player.transform.SetPositionAndRotation(respawnPoint.position, respawnPoint.rotation);
    }

    private void AddNewOrder()
    {
        DeliveryPoint food = _foods[UnityEngine.Random.Range(0, _foods.Length)];
        DeliveryPoint dest = _destinations[UnityEngine.Random.Range(0, _destinations.Length)];
        int timeLimit = CalculateLimitTime(food.transform.position, dest.transform.position);
        UM.AddOrderInList(food, dest, timeLimit);
    }

    private int CalculateLimitTime(Vector3 startPos, Vector3 endPos)
    {
        int result = (int)Vector3.Magnitude(startPos - endPos);
        result = Mathf.Clamp(result, 60, 240);
        return result;
    }

    public bool TryStartOrder(DeliveryPoint food, DeliveryPoint dest, float limitTime = 120)
    {
        if (isProgessOrder) return false;
        StartOrder(food, dest, limitTime);
        isProgessOrder = true;
        return true;
    }

    private void StartOrder(DeliveryPoint food, DeliveryPoint dest, float limitTime = 120)
    {
        _inProgressOrderData.foodPoint = food;
        _inProgressOrderData.destPoint = dest;
        _inProgressOrderData.progressTime = 0;
        _inProgressOrderData.limitTime = limitTime;
        _inProgressOrderData.progress = OrderData.Progress.TakeFood;
        _inProgressOrderData.reward = PlayerDataHelper.CalculateSalary(_inProgressOrderData.relibility);
        _inProgressOrderData.relibility = 3;
        _inProgressOrderData.result = OrderData.Result.NotDecided;
        StartCoroutine(OrderCoroutine(_inProgressOrderData));
    }

    IEnumerator OrderCoroutine(OrderData order)
    {
        
        order.foodPoint.EnablePoint();
        UM.SetMinimapTarget(order.foodPoint.transform);
        //Debug.Log("음식 받으러 가기");
        yield return new WaitWhile(() => order.progress == OrderData.Progress.TakeFood && order.result != OrderData.Result.Canceled);
        
        order.destPoint.EnablePoint();
        UM.SetMinimapTarget(order.destPoint.transform);
        //Debug.Log("배달하기");
        yield return new WaitWhile(() => order.progress == OrderData.Progress.DeliverFood && order.result != OrderData.Result.Canceled);

        CalculateOrderResult();
        FinishOrder();
        //Debug.Log("배달완료");
    }

    public void ArriveDelivaryPoint(DeliveryPoint point)
    {
        DeliveryPoint curPoint;
        if (_inProgressOrderData.progress == OrderData.Progress.TakeFood)
        {
            curPoint = _inProgressOrderData.foodPoint;
        }
        else //if(_inProgressOrder.progress == OrderData.Progress.DeliverFood)
        {
            curPoint = _inProgressOrderData.destPoint;
        }
        if (curPoint == point)
        {
            _inProgressOrderData.progress++;
            point.DisablePoint();
        }
    }

    private void CalculateOrderResult()
    {
        if(_inProgressOrderData.result == OrderData.Result.Canceled)
        {
            _inProgressOrderData.reward = 0;
            _inProgressOrderData.relibility = -3;
        }
        else
        {
            bool isSuccess = _inProgressOrderData.limitTime >= _inProgressOrderData.progressTime;
            if (isSuccess)
            {
                _inProgressOrderData.result = OrderData.Result.Success;
            }
            else
            {
                _inProgressOrderData.result = OrderData.Result.Fail;
                _inProgressOrderData.relibility = -1;
            }
        }
    }

    private void FinishOrder()
    {
        _playerData.coin += _inProgressOrderData.reward;
        _playerData.reliability += _inProgressOrderData.relibility;
        UM.DisableNavigation();
        UM.EnableOrderResultUI();
        isProgessOrder = false;
    }

    public void CancelOrder()
    {
        _inProgressOrderData.result = OrderData.Result.Canceled;
    }
}
