using UnityEngine;

public class DestructibleObstacleScript : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator animator;

    public float _straightForce = 1f;
    public float _upForce = 0.1f;
    public float _destroyDelay = 1f;      // ¬рем€ через которое преп€тствие будет удалено


    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            Vector3 collisionDirection = collision.contacts[0].normal;

            Vector3 force = collisionDirection * _straightForce;
            Vector3 upwardForce = Vector3.up * _upForce;
            rb.AddForce(upwardForce, ForceMode.Impulse);
            rb.AddForce(force, ForceMode.Impulse);

            GetComponent<Collider>().isTrigger = true;
            animator.SetBool("IsTouched", true);
            Destroy(gameObject, _destroyDelay);
        }
    }
}
