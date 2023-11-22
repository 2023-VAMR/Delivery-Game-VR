using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum OrderProgress
    {
        TakeFood,
        DeliverFood,
        Done
    }
    private class Order
    {
        public DeliveryPoint foodPoint;
        public DeliveryPoint destPoint;
        public float progressTime = 0;
        public float LimitTime = 120;
        public OrderProgress progress;
    }

    public static GameManager Instance;
    private UIManager UM;

    [SerializeField] private GameObject foodPointsContainer;
    private DeliveryPoint[] _foods;
    [SerializeField] private GameObject destinationPointsContainer;
    private DeliveryPoint[] _destinations;

    [SerializeField] private float orderAdditionThreshold = 60;
    private float _orderAdditionCount = 0;

    Order _inProgressOrder = null;
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
        if (_inProgressOrder is not null) return false;
        _inProgressOrder = StartOrder(food, dest, limitTime);
        return true;
    }

    private Order StartOrder(DeliveryPoint food, DeliveryPoint dest, float limitTime = 120)
    {
        Order newOrder = new()
        {
            foodPoint = food,
            destPoint = dest,
            progressTime = 0,
            LimitTime = limitTime,
            progress = OrderProgress.TakeFood
        };
        StartCoroutine(OrderCoroutine(newOrder));
        return newOrder;
    }

    IEnumerator OrderCoroutine(Order order)
    {
        
        order.foodPoint.EnablePoint();
        UM.SetMinimapTarget(order.foodPoint.transform);
        Debug.Log("음식 받으러 가기");
        yield return new WaitWhile(() => order.progress == OrderProgress.TakeFood);
        
        order.destPoint.EnablePoint();
        UM.SetMinimapTarget(order.destPoint.transform);
        Debug.Log("배달하기");
        yield return new WaitWhile(() => order.progress == OrderProgress.DeliverFood);

        UM.DisableNavigation();
        _inProgressOrder = null;
        Debug.Log("배달완료");
    }

    public void ArriveDelivaryPoint(DeliveryPoint point)
    {
        DeliveryPoint curPoint;
        if (_inProgressOrder.progress == OrderProgress.TakeFood)
        {
            curPoint = _inProgressOrder.foodPoint;
        }
        else //if(_inProgressOrder.progress == OrderProgress.DeliverFood)
        {
            curPoint = _inProgressOrder.destPoint;
        }
        if (curPoint == point)
        {
            _inProgressOrder.progress++;
            point.DisablePoint();
        }
    }
}
