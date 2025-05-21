using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public int sessionCoins = 0;
    public int totalCoins = 0;
    public TMP_Text coinText;

    private int currentLevel;
    private List<int> sessionCollectedCoinIDs = new();  // временный список ID монет

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Этот объект будет сохраняться между сценами
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        UpdateUI();
    }

    void Start()
    {
        if (coinText == null)
        {
            coinText = FindObjectOfType<TMP_Text>();  // Ищем текстовый объект в сцене
            if (coinText == null)
            {
                Debug.LogError("coinText не найден!");
            }
        }
        UpdateUI();  // Обновляем UI при старте сцены
    }

    public void AddCoin(int coinID)
    {
        // Проверяем: не собрана ли уже и не собрана ли в этой попытке
        if (IsCoinCollected(currentLevel, coinID) || sessionCollectedCoinIDs.Contains(coinID))
            return;

        sessionCoins++;
        sessionCollectedCoinIDs.Add(coinID);
        UpdateUI();  // Обновляем UI после добавления монеты
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
            UpdateUI();  // Обновляем UI после подтверждения монет
        }
    }

    public void ResetSession()
    {
        sessionCoins = 0;
        sessionCollectedCoinIDs.Clear();
        UpdateUI();  // Обновляем UI при сбросе сессии
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
        UpdateUI();  // Обновляем UI после траты монет
    }

    // Сделаем метод UpdateUI() публичным, чтобы его можно было вызывать из других скриптов
    public void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = $"Монет: {totalCoins}";  // Обновляем текст
        }
        else
        {
            Debug.LogWarning("coinText не назначен!");  // Логируем предупреждение, если coinText не привязан
        }
    }

    public void ResetCoins()
    {
        totalCoins = 0;
        sessionCoins = 0;
        sessionCollectedCoinIDs.Clear();
        PlayerPrefs.SetInt("TotalCoins", 0);  // Обнуляем сохраненные монеты
        PlayerPrefs.Save();
        UpdateUI();  // Обновляем UI после сброса
    }

    public void UpdateTotalCoins()
    {
        // Обновляем сохраненные монеты и UI
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();
        UpdateUI();  // Обновляем UI после обновления монет
    }

    // Новый метод, который можно вызвать после прохождения уровня
    public void OnLevelCompleted()
    {
        ConfirmCoins();  // Подтверждаем монеты
        // Можно добавить дополнительные действия, например, загрузку следующего уровня
    }
}
