using UnityEngine;

public class CameraAutoScroll : MonoBehaviour
{
    public Transform target;      // Игрок
    public float xOffset = 2f;    // Смещение камеры относительно игрока по X
    public float followSpeed = 5f; // Скорость следования камеры

    private float initialY;       // Зафиксированное значение Y камеры
    private float fixedZ = -10f;  // Z-координата камеры для 2D

    void Start()
    {
        // Сохраняем текущую позицию Y как фиксированную
        initialY = transform.position.y;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Целевая позиция X — немного позади игрока (или точно за ним)
        float targetX = target.position.x + xOffset;

        // Плавное следование за игроком только по X
        float newX = Mathf.Lerp(transform.position.x, targetX, followSpeed * Time.deltaTime);

        // Y и Z остаются фиксированными
        transform.position = new Vector3(newX, initialY, fixedZ);
    }
}
