using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressResetter : MonoBehaviour
{
    public CoinManager coinManager;  // Ссылка на CoinManager

    public void ResetProgress()
    {
        // Сбрасываем прогресс в PlayerPrefs
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Прогресс сброшен!");

        // Сбрасываем монеты
        if (coinManager != null)
        {
            coinManager.ResetCoins();
        }

        // Перезагрузка сцены, чтобы обновились кнопки уровней
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

