using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class WeaponTrigger : MonoBehaviour
{
    private bool _IsTriggered = false;
    [SerializeField] GameObject _body;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && !_IsTriggered)
        {
            _IsTriggered = true;
            FindFirstObjectByType<WeaponScript>().StartSlowMo();
            _body.SetActive(false);


        }
    }
}
