using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;      // ����� (������)
    public float speed = 2f;      // �������� �������� ������
    public float yOffset = 0f;    // �������� �� Y

    void LateUpdate()
    {
        float newX = transform.position.x + speed * Time.deltaTime;
        float newY = target != null ? target.position.y + yOffset : 0;

        transform.position = new Vector3(newX, newY, -10);
    }
}

