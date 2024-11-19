using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private BoxCollider _colliderObstacle;

    private bool _IsTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision" + " HP:" + FindFirstObjectByType<CarController>()._hp.ToString());

        if(other.gameObject.CompareTag("Player") && !_IsTriggered)
        {
            FindFirstObjectByType<CarController>().GetDamage(1);
            _IsTriggered = true;
            _colliderObstacle.enabled = false;


        }
    }

   
}
