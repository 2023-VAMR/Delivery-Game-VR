using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI gearStateUI;
    [SerializeField] private Minimap minimap;
    [SerializeField] private Phone phone;
    [SerializeField] private Shop shop;

    public GameObject UIHelper;
    [SerializeField] private int _enableUIHelperCount = 0;
    private OVRInputModule ovrInputModule;
    private readonly static string uiHelperAddress = "Assets/Prefabs/Delivary/UIHelpers.prefab";
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
        Addressables.LoadAssetAsync<GameObject>(uiHelperAddress).Completed 
            += (handle) =>
            {
                UIHelper = Instantiate(handle.Result, transform);
                ovrInputModule = UIHelper.GetComponentInChildren<OVRInputModule>();
                DisableUIInput();
            };
    }

    public void SetInputTargetTransform(Transform target)
    {
        ovrInputModule.rayTransform = target;
    }


    public void SetGearText(string text)
    {
        gearStateUI.text = text;
    }

    public void AddOrderInList(DeliveryPoint food, DeliveryPoint dest, float limitTime)
    {
        phone.AddNewButton(food, dest, limitTime);
    }

    public void SetMinimapTarget(Transform target)
    {
        minimap.SetTarget(target);
    }

    public void DisableNavigation()
    {
        minimap.RemoveTarget();
    }

    public void EnableOrderResultUI() 
    {
        phone.FinishOrder();
    }

    public void EnableShopUI()
    {
        shop.EnableShopUI();
    }

    public void DisableShopUI()
    {
        shop.DisableShopUI();
    }


    public void EnableUIInput()
    {
        if (UIHelper is null) return;
        _enableUIHelperCount++;
        UIHelper.SetActive(_enableUIHelperCount > 0);
    }

    public void DisableUIInput()
    {
        if (UIHelper is null) return;
        _enableUIHelperCount--;
        _enableUIHelperCount = _enableUIHelperCount < 0 ? 0 : _enableUIHelperCount;
        UIHelper.SetActive(_enableUIHelperCount > 0);
    }
}
