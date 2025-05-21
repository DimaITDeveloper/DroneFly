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

    public Button level4Button;  // Новая кнопка

    public TMP_Text coinText;

    void Start()
    {
        ReassignCoinText();

        CoinManager.instance?.UpdateUI();

        // Уровень 1 всегда доступен
        level1Button.interactable = true;

        // Уровень 2 доступен, если пройден 1
        level2Button.interactable = LevelLoader.IsLevelCompleted(1);

        // Уровень 3 доступен, если пройден 2
        level3Button.interactable = LevelLoader.IsLevelCompleted(2);

        // Уровень 4 будет:
        // - виден и активен, если пройден 3-й
        // - скрыт, если 3-й не пройден
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
        statsText.text = $"Всего попыток: {total}";
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
                Debug.LogWarning("Не удалось найти CoinText на сцене LevelSelect");
            }
        }
    }
}
