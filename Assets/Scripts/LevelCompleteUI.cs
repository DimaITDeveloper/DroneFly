using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // если у тебя используется TextMeshPro для текста попыток

public class LevelCompleteUI : MonoBehaviour
{
    public GameObject winCanvas;
    public TextMeshProUGUI attemptText;  // Привяжи в инспекторе UI элемент с текстом попытки

    private static int currentAttempt = 0;

    void Start()
    {
        // При заходе в уровень попытка 0
        UpdateAttemptText();

        // Увеличиваем общее количество попыток в PlayerPrefs
        int levelNumber = SceneManager.GetActiveScene().buildIndex;
        LevelLoader.IncrementAttempts(levelNumber);
    }

    public void OnPlayerDied()
    {
        // Увеличиваем текущую попытку на 1 при смерти
        currentAttempt++;
        UpdateAttemptText();

        // Перезагружаем сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnLevelCompleted()
    {
        currentAttempt = 0;

        // Сохраняем факт прохождения уровня
        int levelNumber = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("Level" + levelNumber + "_Completed", 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene("LevelSelect");
    }


    public void GoToLevelSelect()
    {
        // При выходе сбрасываем текущие попытки
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


    public void ShowWinPanel()
    {
        winCanvas.SetActive(true);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
