using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    public int coinID; // ���������� ID ������ � ������

    void Start()
    {
        int level = SceneManager.GetActiveScene().buildIndex;

        if (CoinManager.instance != null)
        {
            if (CoinManager.instance.IsCoinCollected(level, coinID))
            {
                Destroy(gameObject); // ��� ������� � ����������
            }
            else
            {
                CoinManager.instance.RegisterCoinInLevel(coinID); 
            }
        }
    }






    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (CoinManager.instance != null)
            {
                CoinManager.instance.AddCoin(coinID);
            }
            else
            {
                Debug.LogError("CoinManager.instance is NULL! �������, ��� �� ���� � �����.");
            }

            Destroy(gameObject);
        }
    }

}
