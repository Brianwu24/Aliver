using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Net.NetworkInformation;


public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public int coins = 300;
    public Upgrade[] upgrades;

    public Text coinText;
    public GameObject shopUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SortUpgradesByCost();
        DisplayUpgrades();
    }

    public void ToggleShop()
    {
        shopUI.SetActive(!shopUI.activeSelf);
        if (shopUI.activeSelf)
        {
            DisplayUpgrades();
        }
    }

    private void OnGUI()
    {
        coinText.text = "Coins: " + coins.ToString();
    }

    // Bubble sort method to sort upgrades by cost
    public void SortUpgradesByCost()
    {
        int n = upgrades.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (upgrades[j].cost > upgrades[j + 1].cost)
                {
                    // Swap the upgrades
                    Upgrade temp = upgrades[j];
                    upgrades[j] = upgrades[j + 1];
                    upgrades[j + 1] = temp;
                }
            }
        }
    }

    // Display upgrades in the shop UI (pseudo-code, replace with actual logic)
    public void DisplayUpgrades()
    {
        // This is a placeholder method; replace with your actual display logic
        foreach (Upgrade upgrade in upgrades)
        {
            // Example: Debug.Log or create UI elements for each upgrade
            Debug.Log("Upgrade: " + upgrade.name + " - Cost: " + upgrade.cost);
        }
    }

    // Check if the player has a specific upgrade
    public bool HasUpgrade(string upgradeName)
    {
        foreach (Upgrade upgrade in upgrades)
        {
            if (upgrade.name == upgradeName)
            {
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public class Upgrade
{
    public string name;
    public int cost;
    public Sprite image;
    [HideInInspector] public int quantity;
    [HideInInspector] public GameObject itemRef;
}

// Example usage of the ShopManager class
public class TestShopManager : MonoBehaviour
{
    private void Start()
    {
        // Checking if the player has a specific upgrade
        string upgradeToCheck = "DoubleJump";
        if (ShopManager.instance.HasUpgrade(upgradeToCheck))
        {
            Debug.Log("Player has the upgrade: " + upgradeToCheck);
        }
        else
        {
            Debug.Log("Player does not have the upgrade: " + upgradeToCheck);
        }
    }
}