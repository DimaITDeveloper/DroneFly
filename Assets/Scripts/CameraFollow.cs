using UnityEngine;

public class CameraAutoScroll : MonoBehaviour
{
    public Transform target;      // �����
    public float xOffset = 2f;    // �������� ������ ������������ ������ �� X
    public float followSpeed = 5f; // �������� ���������� ������

    private float initialY;       // ��������������� �������� Y ������
    private float fixedZ = -10f;  // Z-���������� ������ ��� 2D

    void Start()
    {
        // ��������� ������� ������� Y ��� �������������
        initialY = transform.position.y;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // ������� ������� X � ������� ������ ������ (��� ����� �� ���)
        float targetX = target.position.x + xOffset;

        // ������� ���������� �� ������� ������ �� X
        float newX = Mathf.Lerp(transform.position.x, targetX, followSpeed * Time.deltaTime);

        // Y � Z �������� ��������������
        transform.position = new Vector3(newX, initialY, fixedZ);
    }
}
