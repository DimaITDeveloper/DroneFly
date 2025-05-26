using UnityEngine;

public class ResetProgressButton : MonoBehaviour
{
    public void ResetAllProgress()
    {
        PlayerPrefs.DeleteAll(); // ������� ��� ����������

        if (CoinManager.instance != null)
        {
            CoinManager.instance.ResetCoins(); // ��������� UI � ����������
        }

        Debug.Log("�������� �������!");
    }
}
