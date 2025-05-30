using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelCompleteUI : MonoBehaviour
{
    public GameObject winCanvas;
    public TextMeshProUGUI attemptText;

    private static int currentAttempt = 0;

    void Start()
    {
        UpdateAttemptText();
        int levelNumber = SceneManager.GetActiveScene().buildIndex;
        LevelLoader.IncrementAttempts(levelNumber);
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
        if (attemptText == null)
        {
            attemptText = GameObject.Find("AttemptText")?.GetComponent<TextMeshProUGUI>();
            if (attemptText != null)
            {
                UpdateAttemptText();
            }
            else
            {
                Debug.LogWarning("AttemptText не найден в новой сцене!");
            }
        }
    }

    public void OnPlayerDied()
    {
        currentAttempt++;
        UpdateAttemptText();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowWinPanel()
    {
        winCanvas.SetActive(true);
    }

    public void OnLevelCompleted()
    {
        currentAttempt = 0;

        int levelNumber = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("Level" + levelNumber + "_Completed", 1);
        PlayerPrefs.Save();

        CoinManager.instance.OnLevelCompleted();

        ShowWinPanel();
    }

    public void GoToLevelSelect()
    {
        currentAttempt = 0;
        SceneManager.LoadScene("LevelSelect");
    }

    private void UpdateAttemptText()
    {
        if (attemptText != null)
        {
            attemptText.text = $"Попытка {currentAttempt}";
            Debug.Log("Обновляем текст попытки: " + currentAttempt);
        }
        else
        {
            Debug.LogWarning("attemptText не назначен!");
        }
    }
}
