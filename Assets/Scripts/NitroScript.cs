using UnityEngine;

public class NitroScript : MonoBehaviour
{

    private bool _IsTriggered = false;
    private CarController _player;

    private void Start()
    {
        _player = FindFirstObjectByType<CarController>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && !_IsTriggered)
        {
            _IsTriggered = true;
            _player.GetNitro();
            Destroy(gameObject);

        }
    }
}
