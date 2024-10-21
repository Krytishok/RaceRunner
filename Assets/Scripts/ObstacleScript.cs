using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");

        if(other.gameObject.CompareTag("Player"))
        {
            FindFirstObjectByType<UI_Manager>().SpawnButtonBackToMenu();
            Time.timeScale = 0;
        }
    }
}
