using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressResetter : MonoBehaviour
{
    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Прогресс сброшен!");

        // Перезагрузка сцены, чтобы обновились кнопки уровней
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
