using UnityEngine;

public class GunScript : MonoBehaviour
{

    public Transform _target = null; // ����, �� ������� ������� �������
    public float rotationSpeed = 5f; // �������� ��������
    public Transform _parent;


    private Quaternion initialRotation; // ����������� �������

    void Start()
    {
        // ��������� ����������� �������
        initialRotation = transform.rotation;
    }
    public void SetTarget(Transform target=null)
    {
        _target = target;
    }

   

    void FixedUpdate()
    {
        if (_target != null)
        {
            // ����������� � ����
            Vector3 direction = (_target.position - transform.position).normalized;
            // ������� �������� �� ��� Y, ���� ����� �������������� ������ � �������������� ���������
            direction.y = 0;

            // �������� ������� � ����
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // ������� ������� � ����
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed);
        }
        else
        {
            // ���������� ����� � �������� ���������
            transform.rotation = _parent.rotation;
        }
    }
}

