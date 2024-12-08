using UnityEngine;

public class SnowBallScript : MonoBehaviour
{

    [SerializeField] int _damage;

    [SerializeField] Rigidbody rb;
    [SerializeField] Collider _physicalCollider;
    [SerializeField] GameObject _visualBody;
    [SerializeField] ParticleSystem _SnowBoom;
    [SerializeField] AudioSource _audio;


    private bool _IsTrigger = false;
    void Start()
    {
        rb.linearVelocity = new Vector3(0, 0, 40f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_IsTrigger)
        {
            _IsTrigger = true;
            other.GetComponent<CarController>().GetDamage(_damage);
            _physicalCollider.enabled = false;  
            _visualBody.SetActive(false);
            _SnowBoom.Play();
            _audio.Play();
            FindFirstObjectByType<CameraController>().CameraShake();


        }
    }


}
