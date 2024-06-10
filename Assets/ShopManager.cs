using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public GameObject Shop;

    // Start is called before the first frame update
    void Start()
    {
        Shop = GetComponent<GameObject>();
        if (Shop != null)
        {
            Shop.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleShop();
        }
    }
    void ToggleShop()
    {
        if (Shop != null)
        {
            // Toggle the active state of the shop panel
            Shop.SetActive(!Shop.activeSelf);
        }
    }
}
