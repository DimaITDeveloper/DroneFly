using UnityEngine;

public class CameraAutoScroll : MonoBehaviour
{
    public Transform drone1;       // ������ ����
    public Transform drone2;       // ������ ����
    public float xOffset = 2f;     // �������� ������ ������������ ������ �� X
    public float followSpeed = 5f; // �������� ���������� ������

    public Transform target;      // �������� ����
    private float initialY;        // ��������������� �������� Y ������
    private float fixedZ = -10f;   // Z-���������� ������ ��� 2D

    void Start()
    {
        initialY = transform.position.y;

        // ���������� ��������� �����
        if (drone1 != null && drone1.gameObject.activeInHierarchy)
            target = drone1;
        else if (drone2 != null && drone2.gameObject.activeInHierarchy)
            target = drone2;
    }

    void LateUpdate()
    {
        if (target == null)
        {
            // ��������� �������� � ����� ���� ��������
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
