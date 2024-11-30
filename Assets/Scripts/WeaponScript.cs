using System.Collections;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    [SerializeField] private float _shootDuration;
    [SerializeField] private GameObject _body;

    [SerializeField] private CarController _player;


    private bool _IsTriggered = false;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && !_IsTriggered)
        {
            _IsTriggered = true;
            _body.SetActive(false);
            _player = other.GetComponent<CarController>();
            FindFirstObjectByType<GameManager>()._IsTimeToShoot = true;
            FindFirstObjectByType<ClickToFireScript>().SetClickButton(true);
            StartCoroutine(ShootDuration(_shootDuration));
            


        }
    }
    public void StopSlowMo()
    {
        _player._speedModificator = 1f;
        StopAllCoroutines();
    }

    

    private IEnumerator ShootDuration(float duration)
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
