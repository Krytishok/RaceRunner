using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private BoxCollider _colliderObstacle;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private GameObject _body;

    private bool _IsTriggered = false;
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("Player") && !_IsTriggered)
        {
            Debug.Log("Collision" + " HP:" + FindFirstObjectByType<CarController>()._hp.ToString());
            FindFirstObjectByType<CarController>().GetDamage(1);
            _IsTriggered = true;
            _colliderObstacle.enabled = false;
            _body.SetActive(false);
            _explosion.Play();




        }
    }

   
}
