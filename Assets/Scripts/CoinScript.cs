using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] float _rotation_speed;
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        FindFirstObjectByType<CoinManager>().AddOne();
        Destroy(gameObject);
        
    }
}
