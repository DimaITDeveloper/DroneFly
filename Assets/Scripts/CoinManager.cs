using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public int sessionCoins = 0;
    public int totalCoins = 0;

    public TMP_Text coinText;
    public TMP_Text totalCoinText;

    private int currentLevel;
    private List<int> sessionCollectedCoinIDs = new();
    private int levelCoinCount = 0;

    private HashSet<int> countedCoinIDs = new(); // Уникальные монеты уровня

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        currentLevel = SceneManager.GetActiveScene().buildIndex;
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
    }

    void Start()
    {
        TryFindCoinText();
        UpdateUI();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentLevel = scene.buildIndex;

        sessionCoins = 0;
        sessionCollectedCoinIDs.Clear();
        countedCoinIDs.Clear();
        levelCoinCount = 0; 

        StartCoroutine(DelayedFindAndUpdateUI());
    }


    private System.Collections.IEnumerator DelayedFindAndUpdateUI()
    {
        yield return new WaitForSeconds(0.1f); // подождать пока сцена прогрузится
        TryFindCoinText();
        UpdateUI();
    }

    private void TryFindCoinText()
    {
        if (coinText == null)
        {
            GameObject obj = GameObject.Find("CoinText");
            if (obj != null)
                coinText = obj.GetComponent<TMP_Text>();
        }

        if (totalCoinText == null)
        {
            GameObject obj = GameObject.Find("TotalCoinText");
            if (obj != null)
                totalCoinText = obj.GetComponent<TMP_Text>();
        }
    }

    public void RegisterCoinInLevel(int coinID)
    {
        if (!countedCoinIDs.Contains(coinID))
        {
            countedCoinIDs.Add(coinID);
            levelCoinCount++;
            UpdateUI();
        }
    }

    public void AddCoin(int coinID)
    {
        if (IsCoinCollected(currentLevel, coinID) || sessionCollectedCoinIDs.Contains(coinID))
            return;

        sessionCoins++;
        sessionCollectedCoinIDs.Add(coinID);
        UpdateUI();
    }

    public void ConfirmCoins()
    {
        if (sessionCoins > 0)
        {
            foreach (int coinID in sessionCollectedCoinIDs)
            {
                SetCoinCollected(currentLevel, coinID);
            }

            totalCoins += sessionCoins;
            SaveTotalCoins();
            SaveLevelCoins(currentLevel, GetLevelCollectedCoins(currentLevel) + sessionCoins);

            sessionCoins = 0;
            sessionCollectedCoinIDs.Clear();
            UpdateUI();
        }
    }

    public void ResetSession()
    {
        sessionCoins = 0;
        sessionCollectedCoinIDs.Clear();
        UpdateUI();
    }

    public bool IsCoinCollected(int level, int coinID)
    {
        return PlayerPrefs.GetInt($"L{level}_Coin{coinID}", 0) == 1;
    }

    private void SetCoinCollected(int level, int coinID)
    {
        PlayerPrefs.SetInt($"L{level}_Coin{coinID}", 1);
    }

    private void SaveLevelCoins(int level, int count)
    {
        PlayerPrefs.SetInt($"L{level}_CollectedTotal", count);
    }

    private int GetLevelCollectedCoins(int level)
    {
        return PlayerPrefs.GetInt($"L{level}_CollectedTotal", 0);
    }

    private void SaveTotalCoins()
    {
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();
    }

    public void SpendCoins(int amount)
    {
        totalCoins -= amount;
        if (totalCoins < 0) totalCoins = 0;
        SaveTotalCoins();
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = $"{sessionCoins}/{levelCoinCount}";
        }

        if (totalCoinText != null)
        {
            totalCoinText.text = $"{totalCoins}";
        }
    }

    public void ResetCoins()
    {
        totalCoins = 0;
        sessionCoins = 0;
        sessionCollectedCoinIDs.Clear();

        PlayerPrefs.DeleteAll(); // очищаем ВСЁ: собранные монеты и общее
        PlayerPrefs.Save();

        UpdateUI();
    }

    public void UpdateTotalCoins()
    {
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();
        UpdateUI();
    }

    public void OnLevelCompleted()
    {
        ConfirmCoins();
    }
}
