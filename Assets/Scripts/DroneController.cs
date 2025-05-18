using UnityEngine;
using UnityEngine.SceneManagement;


public class DroneController : MonoBehaviour
{
    public float forwardSpeed = 2f;
    public float liftSpeed = 5f;
    private Rigidbody2D rb;

    public LevelCompleteUI levelUI;
    // ����������� �� Y
    private float minY;
    private float maxY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // �������������� ����������� ������ ������
        float camHeight = 2f * Camera.main.orthographicSize;
        float camBottom = Camera.main.transform.position.y - camHeight / 2;
        float camTop = Camera.main.transform.position.y + camHeight / 2;

        // ������������� ���������� �������, ������� �������� �� ����
        minY = camBottom + 0.5f;
        maxY = camTop - 0.5f;
    }

    void Update()
    {
        // ���� ��������� �������� ������ �� X
        rb.velocity = new Vector2(forwardSpeed, rb.velocity.y);

        // ������ ��� ��������� ���� / �������
        if (Input.GetMouseButton(0))
        {
            rb.velocity = new Vector2(rb.velocity.x, liftSpeed);
        }

        // ��������� ���������� Y �������, ����� �� ������� �� �����
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            Debug.Log("���� ����������, ������!");
            SceneManager.LoadScene("LevelSelect");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            rb.velocity = Vector2.zero;
            levelUI.ShowWinPanel();
            this.enabled = false;
        }


    }

}
