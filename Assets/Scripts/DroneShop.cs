using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DroneSkinShop : MonoBehaviour
{
    public int drone2Price = 100;
    public Button buyButton;
    public Button selectButton;
    public TextMeshProUGUI coinText;

    void Start()
    {
        ReassignCoinText();
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

            UpdateCoinText();
        }
        else
        {
            Debug.Log("Недостаточно монет");
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
            coinText.text = $"Монеты: {coins}";

            // если CoinManager используется, можно также
            if (CoinManager.instance != null)
                CoinManager.instance.coinText = coinText;
        }
        else
        {
            Debug.LogWarning("coinText не найден!");
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ReassignCoinText();
        UpdateCoinText();
    }

    private void ReassignCoinText()
    {
        if (coinText == null)
        {
            GameObject found = GameObject.Find("CoinText");
            if (found != null)
            {
                coinText = found.GetComponent<TextMeshProUGUI>();
                if (CoinManager.instance != null)
                {
                    CoinManager.instance.coinText = coinText;
                }
            }
        }
    }
}
