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
    private List<int> sessionCollectedCoinIDs = new();  // ��������� ������ ID �����

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // ���� ������ ����� ����������� ����� �������
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
            coinText = FindObjectOfType<TMP_Text>();  // ���� ��������� ������ � �����
            if (coinText == null)
            {
                Debug.LogError("coinText �� ������!");
            }
        }
        UpdateUI();  // ��������� UI ��� ������ �����
    }

    public void AddCoin(int coinID)
    {
        // ���������: �� ������� �� ��� � �� ������� �� � ���� �������
        if (IsCoinCollected(currentLevel, coinID) || sessionCollectedCoinIDs.Contains(coinID))
            return;

        sessionCoins++;
        sessionCollectedCoinIDs.Add(coinID);
        UpdateUI();  // ��������� UI ����� ���������� ������
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
            UpdateUI();  // ��������� UI ����� ������������� �����
        }
    }

    public void ResetSession()
    {
        sessionCoins = 0;
        sessionCollectedCoinIDs.Clear();
        UpdateUI();  // ��������� UI ��� ������ ������
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
        UpdateUI();  // ��������� UI ����� ����� �����
    }

    // ������� ����� UpdateUI() ���������, ����� ��� ����� ���� �������� �� ������ ��������
    public void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = $"�����: {totalCoins}";  // ��������� �����
        }
        else
        {
            Debug.LogWarning("coinText �� ��������!");  // �������� ��������������, ���� coinText �� ��������
        }
    }

    public void ResetCoins()
    {
        totalCoins = 0;
        sessionCoins = 0;
        sessionCollectedCoinIDs.Clear();
        PlayerPrefs.SetInt("TotalCoins", 0);  // �������� ����������� ������
        PlayerPrefs.Save();
        UpdateUI();  // ��������� UI ����� ������
    }

    public void UpdateTotalCoins()
    {
        // ��������� ����������� ������ � UI
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();
        UpdateUI();  // ��������� UI ����� ���������� �����
    }

    // ����� �����, ������� ����� ������� ����� ����������� ������
    public void OnLevelCompleted()
    {
        ConfirmCoins();  // ������������ ������
        // ����� �������� �������������� ��������, ��������, �������� ���������� ������
    }
}
