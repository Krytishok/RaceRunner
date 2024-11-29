using System.Collections;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    [SerializeField] private float _shootDuration;
    [SerializeField] private GameObject _body;


    private bool _IsTriggered = false;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && !_IsTriggered)
        {
            _IsTriggered = true;
            _body.SetActive(false);
            
            FindFirstObjectByType<GameManager>()._IsTimeToShoot = true;
            FindFirstObjectByType<ClickToFireScript>().SetClickButton(true);
            StartCoroutine(ShootDuration(_shootDuration, other.GetComponent<CarController>()));
            


        }
    }

    

    private IEnumerator ShootDuration(float duration, CarController _player)
    {
        EnemyController _enemy = FindFirstObjectByType<EnemyController>();
        _player._speedModificator = 0.2f;
        _enemy._speedModifier = 0.2f;
        yield return new WaitForSecondsRealtime(duration);
        _player._speedModificator = 1f;
        _enemy._speedModifier = 1f;
        FindFirstObjectByType<GameManager>()._IsTimeToShoot = false;
        FindFirstObjectByType<ClickToFireScript>().SetClickButton(false);
        _enemy.RestartTargetting();
    }

}
