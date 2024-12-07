using UnityEngine;

public class BorderScript : MonoBehaviour
{
    [SerializeField] private float bounceForce = 20f; // ���� ������������

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // ���������� ����������� ������������ (����������� �� ������� � ������)
                Vector3 bounceDirection = (other.transform.position - transform.position).normalized;

                // ������� �������� ����� ��� Z (������ ��������������)
                bounceDirection.z = 0;

                // ��������� ���� ������������
                rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);

                // ��������� �������� ������ ��������
                other.GetComponent<CarController>()?.ChangeMoveCoef(0f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // ��������������� ��������
            other.GetComponent<CarController>()?.ChangeMoveCoef(1f);
        }
    }
}
