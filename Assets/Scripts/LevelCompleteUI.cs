using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // ���� � ���� ������������ TextMeshPro ��� ������ �������

public class LevelCompleteUI : MonoBehaviour
{
    public GameObject winCanvas;
    public TextMeshProUGUI attemptText;  // ������� � ���������� UI ������� � ������� �������

    private static int currentAttempt = 0;

    void Start()
    {
        // ��� ������ � ������� ������� 0
        UpdateAttemptText();

        // ����������� ����� ���������� ������� � PlayerPrefs
        int levelNumber = SceneManager.GetActiveScene().buildIndex;
        LevelLoader.IncrementAttempts(levelNumber);
    }

    public void OnPlayerDied()
    {
        // ����������� ������� ������� �� 1 ��� ������
        currentAttempt++;
        UpdateAttemptText();

        // ������������� �����
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnLevelCompleted()
    {
        currentAttempt = 0;

        // ��������� ���� ����������� ������
        int levelNumber = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("Level" + levelNumber + "_Completed", 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene("LevelSelect");
    }


    public void GoToLevelSelect()
    {
        // ��� ������ ���������� ������� �������
        currentAttempt = 0;
        SceneManager.LoadScene("LevelSelect");
    }

    private void UpdateAttemptText()
    {
        if (attemptText != null)
        {
            attemptText.text = $"������� {currentAttempt}";
            Debug.Log("��������� ����� �������: " + currentAttempt);
        }
        else
        {
            Debug.LogWarning("attemptText �� ��������!");
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
