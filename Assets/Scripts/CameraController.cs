using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public Transform _player;

    public Vector3 _offset = new Vector3(0, 3.5f, 8f);


    private void FixedUpdate()
    {
       
        transform.position = _player.position + _offset;
    }


}
