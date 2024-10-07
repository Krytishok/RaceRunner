using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CarController : MonoBehaviour
{
    //Connecting wheels and colliders to script
    [SerializeField] Transform _transformFL;
    [SerializeField] Transform _transformBL;
    [SerializeField] Transform _transformFR;
    [SerializeField] Transform _transformBR;

    [SerializeField] WheelCollider _wheelColliderFL;
    [SerializeField] WheelCollider _wheelColliderBL;
    [SerializeField] WheelCollider _wheelColliderFR;
    [SerializeField] WheelCollider _wheelColliderBR;

    //Box colliders
    [SerializeField] public BoxCollider BodyCar;
    [SerializeField] public CapsuleCollider FrontOfCar;

    //Car properties
    [SerializeField] private float _forceEngine;

    [SerializeField] private float _maxAngleOfWheel;

    // ����������� ��� ������� ������
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;

    [SerializeField] private float _minYangle;
    [SerializeField] private float _maxYangle;

    //�������� ������
    public float _speed;

    void Start()
    {
        gameObject.tag = "Player";
    }

    private void FixedUpdate()
    {
        _speed = Math.Abs(GetComponent<Rigidbody>().linearVelocity.magnitude);
        Debug.Log("Speed: " + _speed);

        if ( _speed <= 20) 
        {
            _wheelColliderFL.motorTorque = 1000f;
            _wheelColliderFR.motorTorque = 1000f;
        }
        _wheelColliderFL.motorTorque = Input.GetAxis("Vertical") * _forceEngine;
        _wheelColliderFR.motorTorque = Input.GetAxis("Vertical") * _forceEngine;



        if (Input.GetKey(KeyCode.Space) && _speed > 20)
        {
            _wheelColliderFL.brakeTorque = 3000f;
            _wheelColliderFR.brakeTorque = 3000f;
            _wheelColliderBL.brakeTorque = 3000f;
            _wheelColliderBR.brakeTorque = 3000f;
        }
        else if(_speed < 20)
        {
            _wheelColliderFL.brakeTorque = 0;
            _wheelColliderFR.brakeTorque = 0;
            _wheelColliderBL.brakeTorque = 0;
            _wheelColliderBR.brakeTorque = 0;
            _wheelColliderFL.motorTorque = 1000f;
            _wheelColliderFR.motorTorque = 1000f;
        }

        // Rotating collider of wheels
        _wheelColliderFL.steerAngle = _maxAngleOfWheel * Input.GetAxis("Horizontal");
        _wheelColliderFR.steerAngle = _maxAngleOfWheel * Input.GetAxis("Horizontal");

        RotateWheel(_wheelColliderFL, _transformFL);
        RotateWheel(_wheelColliderFR, _transformFR);
        RotateWheel(_wheelColliderBL, _transformBL);
        RotateWheel(_wheelColliderBR, _transformBR);

        // ����������� ������� ������
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, _minX, _maxX);
        transform.position = position;

        float currentRotationY = transform.eulerAngles.y;
        currentRotationY = Mathf.Clamp(currentRotationY, _minYangle, _maxYangle);
        Quaternion newRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, currentRotationY, transform.eulerAngles.z));
        transform.rotation = newRotation;

    }



    private void RotateWheel(WheelCollider collider, Transform transform)
    {
        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);

        transform.rotation = rotation;
        transform.position = position;
    }
}
