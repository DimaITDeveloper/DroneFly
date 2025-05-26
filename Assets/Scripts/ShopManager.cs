using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Button[] buyButtons;
    public int[] itemPrices;
    public string[] itemKeys; // уникальные ключи для PlayerPrefs
    public TMP_Text totalCoinsText;

    void Start()
    {
        UpdateShopUI();
    }

    public void BuyItem(int index)
    {
        if (CoinManager.instance.totalCoins >= itemPrices[index] && !IsItemPurchased(index))
        {
            CoinManager.instance.SpendCoins(itemPrices[index]);
            PlayerPrefs.SetInt(itemKeys[index], 1);
            PlayerPrefs.Save();
            UpdateShopUI();
        }
    }

    bool IsItemPurchased(int index)
    {
        return PlayerPrefs.GetInt(itemKeys[index], 0) == 1;
    }

    void UpdateShopUI()
    {
        for (int i = 0; i < buyButtons.Length; i++)
        {
            if (IsItemPurchased(i))
            {
                buyButtons[i].interactable = false;
                buyButtons[i].GetComponentInChildren<TMP_Text>().text = "Куплено";
            }
        }

        if (totalCoinsText != null)
            totalCoinsText.text = CoinManager.instance.totalCoins.ToString();
    }
}
