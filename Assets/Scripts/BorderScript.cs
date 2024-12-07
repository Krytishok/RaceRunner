using UnityEngine;

public class BorderScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            Rigidbody rb = other.GetComponent<Rigidbody>();
            float sign = rb.linearVelocity.x / Mathf.Abs(rb.linearVelocity.x);
            rb.linearVelocity = new Vector3(-sign*10f, rb.linearVelocity.y, rb.linearVelocity.z);


        }
    }
}
