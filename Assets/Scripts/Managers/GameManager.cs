using Oculus.Interaction.HandGrab.Recorder.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    private UIManager UM;

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

    private void AddNewOrder()
    {
        DeliveryPoint food = _foods[Random.Range(0, _foods.Length)];
        DeliveryPoint dest = _destinations[Random.Range(0, _destinations.Length)];
        int timeLimit = (int)Vector3.Magnitude(food.transform.position - dest.transform.position);
        UM.AddOrderInList(food, dest, timeLimit);
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
        _inProgressOrderData.LimitTime = limitTime;
        _inProgressOrderData.progress = OrderData.Progress.TakeFood;
        StartCoroutine(OrderCoroutine(_inProgressOrderData));
    }

    IEnumerator OrderCoroutine(OrderData order)
    {
        
        order.foodPoint.EnablePoint();
        UM.SetMinimapTarget(order.foodPoint.transform);
        //Debug.Log("음식 받으러 가기");
        yield return new WaitWhile(() => order.progress == OrderData.Progress.TakeFood);
        
        order.destPoint.EnablePoint();
        UM.SetMinimapTarget(order.destPoint.transform);
        //Debug.Log("배달하기");
        yield return new WaitWhile(() => order.progress == OrderData.Progress.DeliverFood);

        UM.DisableNavigation();
        UM.EnableOrderResultUI();
        isProgessOrder = false;

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
}
