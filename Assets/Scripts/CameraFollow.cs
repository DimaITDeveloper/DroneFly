using UnityEngine;

public class CameraAutoScroll : MonoBehaviour
{
    public Transform drone1;       // Первый дрон
    public Transform drone2;       // Второй дрон
    public float xOffset = 2f;     // Смещение камеры относительно игрока по X
    public float followSpeed = 5f; // Скорость следования камеры

    public Transform target;      // Активный дрон
    private float initialY;        // Зафиксированное значение Y камеры
    private float fixedZ = -10f;   // Z-координата камеры для 2D

    void Start()
    {
        initialY = transform.position.y;

        // Определяем активного дрона
        if (drone1 != null && drone1.gameObject.activeInHierarchy)
            target = drone1;
        else if (drone2 != null && drone2.gameObject.activeInHierarchy)
            target = drone2;
    }

    void LateUpdate()
    {
        if (target == null)
        {
            // Повторная проверка — вдруг дрон сменился
            if (drone1 != null && drone1.gameObject.activeInHierarchy)
                target = drone1;
            else if (drone2 != null && drone2.gameObject.activeInHierarchy)
                target = drone2;
        }

        if (target == null) return;

        float targetX = target.position.x + xOffset;
        float newX = Mathf.Lerp(transform.position.x, targetX, followSpeed * Time.deltaTime);
        transform.position = new Vector3(newX, initialY, fixedZ);
    }
}
