using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private Collider _colliderObstacle;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private GameObject _body;
    [SerializeField] private AudioSource _boomSound;
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private float _bombSpeed;

    private bool _IsTriggered = false;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && !_IsTriggered)
        {
            Debug.Log("Collision" + " HP:" + FindFirstObjectByType<CarController>()._hp.ToString());
            FindFirstObjectByType<CarController>().GetDamage(_damage);
            _IsTriggered = true;
            _colliderObstacle.enabled = false;
            _body.SetActive(false);
            _explosion.Play();
            _boomSound.Play();

        }
    }
    private void Start()
    {
        _rb.linearVelocity = new Vector3(0, -20, -_bombSpeed);
        Destroy(gameObject, 3f);
    }
}
