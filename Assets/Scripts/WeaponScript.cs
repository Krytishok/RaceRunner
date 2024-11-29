using System.Collections;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    [SerializeField] private float _shootDuration;


    private bool _IsTriggered = false;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && !_IsTriggered)
        {
            _IsTriggered = true;
            
            FindFirstObjectByType<GameManager>()._IsTimeToShoot = true;
            StartCoroutine(ShootDuration(_shootDuration, other.GetComponent<CarController>()));
            


        }
    }

    

    private IEnumerator ShootDuration(float duration, CarController _player)
    {
        _player._speedModificator = 0.2f;
        FindFirstObjectByType<EnemyController>()._speedModifier = 0.2f;
        yield return new WaitForSecondsRealtime(duration);
        _player._speedModificator = 1f;
        FindFirstObjectByType<EnemyController>()._speedModifier = 1f;
    }

}
