using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] float _rotation_speed;
    [SerializeField] GameObject _body;

    [SerializeField] AudioSource _pickUpSound;
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindFirstObjectByType<CoinManager>().AddOne();
            _body.SetActive(false);
            _pickUpSound.Play();
            Destroy(gameObject, 1f);
            
        }
        
    }
    
}
