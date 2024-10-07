using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{
    [SerializeField] Transform FinishPositionOfCamera;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<CameraController>()._player = FinishPositionOfCamera;
            FindObjectOfType<CameraController>().transform.rotation = FinishPositionOfCamera.transform.rotation;
            FindObjectOfType<UI_Manager>().SpawnButtonBackToMenu();
        }
    }
}
