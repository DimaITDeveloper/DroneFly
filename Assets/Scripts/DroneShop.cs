using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DroneSkinShop : MonoBehaviour
{
    public int drone2Price = 100;
    public Button buyButton;
    public Button selectButton;
    public TextMeshProUGUI coinText; //  ��������� ���� ��� ����������� �����

    void Start()
    {
        // ��������� ����������� �����
        UpdateCoinText();

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
            coins -= drone2Price;
            PlayerPrefs.SetInt("Coins", coins);
            PlayerPrefs.SetInt("Drone2Purchased", 1);

            buyButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);

            UpdateCoinText(); //  ��������� ����� ����� �������
        }
        else
        {
            Debug.Log("������������ �����");
        }
    }

    void UpdateCoinText()
    {
        if (coinText == null)
        {
            coinText = GameObject.Find("CoinText")?.GetComponent<TextMeshProUGUI>();
        }

        if (coinText != null)
        {
            int coins = PlayerPrefs.GetInt("Coins", 0);
            coinText.text = $"������: {coins}";
        }
        else
        {
            Debug.LogWarning("coinText �� ������!");
        }
    }
}
