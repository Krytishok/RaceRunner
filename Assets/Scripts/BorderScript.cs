using Boxophobic.Utils;
using UnityEngine;

public class BorderScript : MonoBehaviour
{
    private int _damage = 7;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // ���������� ����������� ������������ (�� ������ ������� � �������)
            Vector3 pushDirection = other.transform.position - transform.position;

            // ��������� ���� ������������
            float pushForce = 2f; // ������������� ��������

            // ����������� �������
            other.GetComponent<CarController>().PushFromBorder(pushDirection, pushForce, _damage);
        }
    }

   
}
