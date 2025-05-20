using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinID; // уникальный ID монеты в уровне

    void Start()
    {
        int level = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        // Если монета уже собрана — уничтожаем
        if (CoinManager.instance != null && CoinManager.instance.IsCoinCollected(level, coinID))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CoinManager.instance.AddCoin(coinID);
            Destroy(gameObject);
        }
    }
}
