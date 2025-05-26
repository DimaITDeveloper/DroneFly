using UnityEngine;

public enum BoostType { Speed, Shield, Magnet, DoubleCoins }

public class Boost : MonoBehaviour
{
    public BoostType boostType;
    public float duration = 5f; // ������������ �������� �����

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBoostController boostController = other.GetComponent<PlayerBoostController>();
            if (boostController != null)
            {
                boostController.ActivateBoost(boostType, duration);
                Destroy(gameObject); // ������� ���� ����� �������
            }
        }
    }
}

