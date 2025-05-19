using UnityEngine;
using UnityEngine.UI;

public class DroneSkinShop : MonoBehaviour
{
    public int drone2Price = 100;
    public Button buyButton;
    public Button selectButton;

    void Start()
    {
        // Проверка: уже куплен?
        if (PlayerPrefs.GetInt("Drone2Purchased", 0) == 1)
        {
            buyButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(false);
        }

        buyButton.onClick.AddListener(BuyDrone2);
    }

    void BuyDrone2()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);

        if (coins >= drone2Price)
        {
            // Списать валюту
            coins -= drone2Price;
            PlayerPrefs.SetInt("Coins", coins);

            // Пометить дрона как купленного
            PlayerPrefs.SetInt("Drone2Purchased", 1);

            // Обновить кнопки
            buyButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Недостаточно монет");
        }
    }
}
