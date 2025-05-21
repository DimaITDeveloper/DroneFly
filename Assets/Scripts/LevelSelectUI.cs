using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectUI : MonoBehaviour
{
    public Button level1Button;
    public Text level1StatsText;

    public Button level2Button;
    public Text level2StatsText;

    public Button level3Button;
    public Text level3StatsText;

    public Button level4Button;  // ����� ������

    public TMP_Text coinText;

    void Start()
    {
        ReassignCoinText();

        CoinManager.instance?.UpdateUI();

        // ������� 1 ������ ��������
        level1Button.interactable = true;

        // ������� 2 ��������, ���� ������� 1
        level2Button.interactable = LevelLoader.IsLevelCompleted(1);

        // ������� 3 ��������, ���� ������� 2
        level3Button.interactable = LevelLoader.IsLevelCompleted(2);

        // ������� 4 �����:
        // - ����� � �������, ���� ������� 3-�
        // - �����, ���� 3-� �� �������
        if (LevelLoader.IsLevelCompleted(3))
        {
            level4Button.gameObject.SetActive(true);
            level4Button.interactable = true;
        }
        else
        {
            level4Button.gameObject.SetActive(false);
        }

        UpdateStats(1, level1StatsText);
        UpdateStats(2, level2StatsText);
        UpdateStats(3, level3StatsText);
    }

    void UpdateStats(int levelNumber, Text statsText)
    {
        int total = PlayerPrefs.GetInt("Level" + levelNumber + "_AttemptsTotal", 0);
        statsText.text = $"����� �������: {total}";
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
        CoinManager.instance?.UpdateUI();
    }

    private void ReassignCoinText()
    {
        if (coinText == null)
        {
            var found = GameObject.Find("CoinText");
            if (found != null)
            {
                coinText = found.GetComponent<TMP_Text>();
                CoinManager.instance.coinText = coinText;
            }
            else
            {
                Debug.LogWarning("�� ������� ����� CoinText �� ����� LevelSelect");
            }
        }
    }
}
