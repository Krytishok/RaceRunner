using System.Collections;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    [SerializeField] private float _shootDuration;

    [SerializeField] private CarController _player;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private ClickToFireScript _clickController;
    [SerializeField] private AudioManagerController _audioController;

    private void Start()
    {
        _player = FindFirstObjectByType<CarController>();
    }

    public void StopSlowMo()
    {
        _player._speedModificator = 1f;
        _player.ChangeDamageCoef(1f);
        FindFirstObjectByType<CameraController>().CameraToGun(false);
        _clickController.SetClickButton(false);
        _audioController.SlowMoEffect(1f);
        _gameManager._IsTimeToShoot = false;
        StopAllCoroutines();
    }

    public void StartSlowMo()
    {
        _gameManager._IsTimeToShoot = true;
        _player.ChangeDamageCoef(0f);
        _clickController.SetClickButton(true);
        _audioController.SlowMoEffect(0.2f);
        FindFirstObjectByType<CameraController>().CameraToGun(true);
        StartCoroutine(ShootDuration(_shootDuration));
    }
    

    private IEnumerator ShootDuration(float duration)
    {
        EnemyController _enemy = FindFirstObjectByType<EnemyController>();
        _player._speedModificator = 0.2f;
        _enemy.SetSlowMo(0.2f);
        FindFirstObjectByType<CameraController>().CameraToGun(true);
        yield return new WaitForSecondsRealtime(duration);
        _player._speedModificator = 1f;
        _enemy.SetSlowMo(1f);
        _gameManager._IsTimeToShoot = false;
        _clickController.SetClickButton(false);
        _enemy.RestartTargetting();
        StopSlowMo();
    }

}
