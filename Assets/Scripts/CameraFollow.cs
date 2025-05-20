using UnityEngine;

public class CameraAutoScroll : MonoBehaviour
{
    public float scrollSpeed = 2f;  // Скорость движения камеры по X
    public float yOffset = 0f;      // Смещение по Y (если следим за игроком)
    public Transform target;        // Игрок (можно null)

    void LateUpdate()
    {
        float newX = transform.position.x + scrollSpeed * Time.deltaTime;

        float newY = yOffset;

        // Если есть игрок — следим по Y
        if (target != null)
        {
            newY = target.position.y + yOffset;
        }

        transform.position = new Vector3(newX, newY, -10f);
    }
}
