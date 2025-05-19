using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoPopup : MonoBehaviour
{
    public GameObject popupPanel; // ������ � �������, ������� ����������/��������
    public TextMeshProUGUI popupText;       // ����� �� ������
    public int levelNumber;       // ����� ������ ��� ����������

    public void ShowPopup()
    {
        int totalAttempts = LevelLoader.GetTotalAttempts(levelNumber);

        popupText.text = $"������� {levelNumber}\n" +
                         $"����� ��������: {totalAttempts}";

        popupPanel.SetActive(true);
    }


    public void HidePopup()
    {
        popupPanel.SetActive(false);
    }
}
