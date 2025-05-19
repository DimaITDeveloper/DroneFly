using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoPopup : MonoBehaviour
{
    public GameObject popupPanel; // Панель с текстом, которую показываем/скрываем
    public TextMeshProUGUI popupText;       // Текст на панели
    public int levelNumber;       // Номер уровня для статистики

    public void ShowPopup()
    {
        int totalAttempts = LevelLoader.GetTotalAttempts(levelNumber);

        popupText.text = $"Уровень {levelNumber}\n" +
                         $"Всего запусков: {totalAttempts}";

        popupPanel.SetActive(true);
    }


    public void HidePopup()
    {
        popupPanel.SetActive(false);
    }
}
