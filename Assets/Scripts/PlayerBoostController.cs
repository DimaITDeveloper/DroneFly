using UnityEngine;

public class PlayerBoostController : MonoBehaviour
{
    private float originalSpeed;
    public float boostedSpeed = 10f;
    public bool isShieldActive = false;
    public bool isMagnetActive = false;
    public bool isDoubleCoinsActive = false;

    private float boostTimer = 0f;
    private BoostType currentBoost;

    public float playerSpeed = 5f;
    public GameObject shieldVisual;

    void Start()
    {
        originalSpeed = playerSpeed;
        shieldVisual?.SetActive(false);
    }

    void Update()
    {
        if (boostTimer > 0)
        {
            boostTimer -= Time.deltaTime;

            if (boostTimer <= 0)
            {
                DeactivateBoost();
            }
        }
    }

    public void ActivateBoost(BoostType type, float duration)
    {
        currentBoost = type;
        boostTimer = duration;

        switch (type)
        {
            case BoostType.Speed:
                playerSpeed = boostedSpeed;
                break;
            case BoostType.Shield:
                isShieldActive = true;
                shieldVisual?.SetActive(true);
                break;
            case BoostType.Magnet:
                isMagnetActive = true;
                break;
            case BoostType.DoubleCoins:
                isDoubleCoinsActive = true;
                break;
        }
    }

    private void DeactivateBoost()
    {
        switch (currentBoost)
        {
            case BoostType.Speed:
                playerSpeed = originalSpeed;
                break;
            case BoostType.Shield:
                isShieldActive = false;
                shieldVisual?.SetActive(false);
                break;
            case BoostType.Magnet:
                isMagnetActive = false;
                break;
            case BoostType.DoubleCoins:
                isDoubleCoinsActive = false;
                break;
        }
    }
}
