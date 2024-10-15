using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public GameObject _player;

    public Vector3 _offset = new Vector3(0, 3.5f, 8f);




    public void Start()
    {
        _player = FindFirstObjectByType<GameManager>()._selectedCar.gameObject;
    }

    

    private void LateUpdate()
    {
        transform.position = _player.transform.position + _offset;

    }
}
