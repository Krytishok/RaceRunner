using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] float _rotation_speed;

    [SerializeField] AudioSource _pickUpSound;
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindFirstObjectByType<CoinManager>().AddOne();
            Destroy(gameObject);
        }
        
    }
    
}
