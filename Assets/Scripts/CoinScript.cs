using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] float _rotation_speed;
    [SerializeField] GameObject _body;
    [SerializeField] ParticleSystem _pickUpParticle;

    [SerializeField] AudioSource _pickUpSound;
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _pickUpParticle.Play();
            FindFirstObjectByType<CoinManager>().AddOne();
            _body.SetActive(false);
            _pickUpSound.Play();
            Destroy(gameObject, 1f);
            
        }
        
    }
    
}
