using UnityEngine;
using UnityEngine.SceneManagement;

public class DroneController : MonoBehaviour
{
    public float forwardSpeed = 6.5f;
    public float liftSpeed = 5f;
    private Rigidbody2D rb;

    public LevelCompleteUI levelUI;

    // Ограничения по Y
    private float minY;
    private float maxY;

    // Буст скорости
    private bool isBoosted = false;
    private float boostTimer = 0f;
    public float boostedSpeed = 9f;
    public float boostDuration = 3f;

    // Магнит
    private bool coinMagnetActive = false;
    public float magnetRadius = 3f;

    // Щит
    private bool shieldActive = false;
    public float shieldDuration = 5f;
    private float shieldTimer = 0f;
    public GameObject shieldEffect;

    private Collider2D myCollider;

    void Start()
    {
        if (levelUI == null)
            levelUI = FindObjectOfType<LevelCompleteUI>();

        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();

        float camHeight = 2f * Camera.main.orthographicSize;
        float camBottom = Camera.main.transform.position.y - camHeight / 2;
        float camTop = Camera.main.transform.position.y + camHeight / 2;

        minY = camBottom + 0.5f;
        maxY = camTop - 0.5f;
    }

    void Update()
    {
        if (isBoosted)
        {
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0)
                isBoosted = false;
        }

        if (shieldActive)
        {
            shieldTimer -= Time.deltaTime;
            if (shieldTimer <= 0)
                DeactivateShield();
        }

        float currentSpeed = isBoosted ? boostedSpeed : forwardSpeed;
        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);

        if (Input.GetMouseButton(0))
        {
            rb.velocity = new Vector2(rb.velocity.x, liftSpeed);
        }

        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);

        if (coinMagnetActive)
            AttractCoins();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            if (shieldActive)
            {
                Debug.Log("Щит активен — препятствие проигнорировано");
                return;
            }

            Debug.Log("Дрон столкнулся, смерть!");
            if (levelUI != null)
                levelUI.OnPlayerDied();
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            rb.velocity = Vector2.zero;
            levelUI.ShowWinPanel();
            this.enabled = false;
            levelUI.OnLevelCompleted();
        }
        else if (other.CompareTag("Boost"))
        {
            Debug.Log("Подобран буст!");
            isBoosted = true;
            boostTimer = boostDuration;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("CoinMagnet"))
        {
            Debug.Log("Активирован магнит!");
            coinMagnetActive = true;
            Destroy(other.gameObject);
            Invoke("DeactivateCoinMagnet", 5f);
        }
        else if (other.CompareTag("Shield"))
        {
            Debug.Log("Активирован щит!");
            ActivateShield();
            Destroy(other.gameObject);
        }
    }

    void AttractCoins()
    {
        Collider2D[] coins = Physics2D.OverlapCircleAll(transform.position, magnetRadius);
        foreach (var col in coins)
        {
            if (col.CompareTag("Coin"))
            {
                Vector3 direction = (transform.position - col.transform.position).normalized;
                col.transform.position += direction * Time.deltaTime * 5f;
            }
        }
    }

    void ActivateShield()
    {
        shieldActive = true;
        shieldTimer = shieldDuration;

        if (shieldEffect != null)
            shieldEffect.SetActive(true);

        // Игнорировать столкновения с препятствиями
        Collider2D[] allColliders = FindObjectsOfType<Collider2D>();
        foreach (var col in allColliders)
        {
            if (col.CompareTag("Obstacle"))
                Physics2D.IgnoreCollision(myCollider, col, true);
        }
    }

    void DeactivateShield()
    {
        shieldActive = false;

        if (shieldEffect != null)
            shieldEffect.SetActive(false);

        // Включить обратно столкновения
        Collider2D[] allColliders = FindObjectsOfType<Collider2D>();
        foreach (var col in allColliders)
        {
            if (col.CompareTag("Obstacle"))
                Physics2D.IgnoreCollision(myCollider, col, false);
        }
    }

    void DeactivateCoinMagnet()
    {
        coinMagnetActive = false;
    }
}
