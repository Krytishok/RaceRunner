using System.Collections;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    [SerializeField] private float _shootDuration;

    [SerializeField] private CarController _player;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private ClickToFireScript _clickController;

    private void Start()
    {
        _player = FindFirstObjectByType<CarController>();
    }

    public void StopSlowMo()
    {
        _player._speedModificator = 1f;
        StopAllCoroutines();
    }

    public void StartSlowMo()
    {
        _gameManager._IsTimeToShoot = true;
        _clickController.SetClickButton(true);
        StartCoroutine(ShootDuration(_shootDuration));
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
