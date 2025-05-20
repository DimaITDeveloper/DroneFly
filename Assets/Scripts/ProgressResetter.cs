using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressResetter : MonoBehaviour
{
    public CoinManager coinManager;  // ������ �� CoinManager

    public void ResetProgress()
    {
        // ���������� �������� � PlayerPrefs
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("�������� �������!");

        // ���������� ������
        if (coinManager != null)
        {
            coinManager.ResetCoins();
        }

        // ������������ �����, ����� ���������� ������ �������
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

