using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;      // Игрок (птичка)
    public float speed = 2f;      // Скорость движения камеры
    public float yOffset = 0f;    // Смещение по Y

    void LateUpdate()
    {
        float newX = transform.position.x + speed * Time.deltaTime;
        float newY = target != null ? target.position.y + yOffset : 0;

        transform.position = new Vector3(newX, newY, -10);
    }
}

