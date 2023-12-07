using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemShopselect : MonoBehaviour
{
    public string ItemShop;

    // Start is called before the first frame update
    private void Start()
    {
        Button button = GetComponent<Button>();

        if(button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    // Update is called once per frame
    private void OnButtonClick()
    {
        SceneManager.LoadScene(ItemShop);
    }
}
