using UnityEngine;

public class RoadTrigger : MonoBehaviour
{
    private RoadManager roadManager;
    private bool isGenerating = false; // Флаг для контроля генерации

    private void Start()
    {
        // Найти RoadManager в сцене
        roadManager = FindFirstObjectByType<RoadManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !(isGenerating))
        {
            // Генерация новой секции и удаление старой
            roadManager.SpawnSection();
            roadManager.DespawnOldSection();
            Debug.Log("Colision");
            isGenerating = true;
        }

    }

}
