using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Vector3 _spawnOffset;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _health;
    [SerializeField] private float _movementSpeedOfEnemy = 5f; // скорость перемещени€ врага к новой позиции
    [SerializeField] private float _timeToReachPosition;
    [SerializeField] private float _timeToGetPositionMin;
    [SerializeField] private float _timeToGetPositionMax;
    [SerializeField] private float _reloadingTimeMin;
    [SerializeField] private float _reloadingTimeMax;

    [SerializeField] private GameObject _bomb;
    [SerializeField] GameObject _visualBomb;



    private CarController _player;
    private GameManager _gameManager;
    private Vector3 _targetPosition;
    private Vector3 _velocity;
    private float _xOffset;
    public float _speedModifier;

    private bool _targetting;



    private void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        _player = FindFirstObjectByType<CarController>();
        _gameManager = FindFirstObjectByType<GameManager>();
        transform.position = _player.transform.position + _spawnOffset;
        //_rb.linearVelocity = new Vector3(0, 0, -_player.MinSpeedOfCar()); 

        _gameManager._IsEnemyOnRoad = true;

        StartCoroutine(UpdateTargetPosition(_timeToGetPositionMin, _timeToGetPositionMax));
        
    }

    private IEnumerator UpdateTargetPosition(float delayMin, float delayMax)
    {
        while (!_gameManager._IsTimeToShoot)
        {
            // ќбновл€ем целевую позицию
            _targetting = true;
            yield return new WaitForSeconds(Random.Range(delayMin, delayMax));
            _visualBomb.SetActive(false);
            BombDrop();
            _targetting = false;
            yield return new WaitForSeconds(Random.Range(_reloadingTimeMin, _reloadingTimeMax));
            _visualBomb.SetActive(true);
        }
    }
    

    private void FixedUpdate()
    {
        if (_targetting)
        {
            _xOffset = _player.transform.position.x;
        }

        // ѕлавное перемещение врага к целевой позиции
        _targetPosition = new Vector3(_xOffset, transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x, _player.transform.position.y, _player.transform.position.z) + _spawnOffset; //ƒвижение по Z перед игроком
        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _velocity, _timeToReachPosition, _movementSpeedOfEnemy);
        
    }

    private void BombDrop()
    {
        var bomb = _bomb;
        Vector3 SpawnPosition = _visualBomb.transform.position;
        Quaternion SpawnRotation = _visualBomb.transform.rotation;
        Instantiate(bomb, SpawnPosition, SpawnRotation);


    }

    

    public void GetDamage(float damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            _gameManager._IsEnemyOnRoad = false;

            
        }
    }

}
