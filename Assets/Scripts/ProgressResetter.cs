using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressResetter : MonoBehaviour
{
    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("�������� �������!");

        // ������������ �����, ����� ���������� ������ �������
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
