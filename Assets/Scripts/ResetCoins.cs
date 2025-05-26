using UnityEngine;

public class ResetProgressButton : MonoBehaviour
{
    public void ResetAllProgress()
    {
        PlayerPrefs.DeleteAll(); // Удаляет все сохранения

        if (CoinManager.instance != null)
        {
            CoinManager.instance.ResetCoins(); // Обновляем UI и переменные
        }

        Debug.Log("Прогресс сброшен!");
    }
}
