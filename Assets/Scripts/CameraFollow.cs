using UnityEngine;

public class CameraAutoScroll : MonoBehaviour
{
    public float scrollSpeed = 2f;  // �������� �������� ������ �� X
    public float yOffset = 0f;      // �������� �� Y (���� ������ �� �������)
    public Transform target;        // ����� (����� null)

    void LateUpdate()
    {
        float newX = transform.position.x + scrollSpeed * Time.deltaTime;

        float newY = yOffset;

        // ���� ���� ����� � ������ �� Y
        if (target != null)
        {
            newY = target.position.y + yOffset;
        }

        transform.position = new Vector3(newX, newY, -10f);
    }
}
