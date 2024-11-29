using UnityEngine;

public class RoadTrigger : MonoBehaviour
{
    private RoadManager roadManager;
    private bool isGenerating = false; // ���� ��� �������� ���������

    private void Start()
    {
        // ����� RoadManager � �����
        roadManager = FindFirstObjectByType<RoadManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !(isGenerating))
        {
            // ��������� ����� ������ � �������� ������
            roadManager.SpawnSection();
            roadManager.DespawnOldSection();
            Debug.Log("Colision");
            isGenerating = true;
        }

    }

}
