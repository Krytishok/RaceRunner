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
    [SerializeField] private float _turningSpeed;
    [SerializeField] private float _tiltAngle;

    [SerializeField] private float _maxAngleOfWheel;

    // ����������� ��� ������� ������
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;

    [SerializeField] private float _minYangle;
    [SerializeField] private float _maxYangle;

    //�������� ������
    public float _speed;


    //auxiliary variables
    private float _tilt;


    void Start()
    {
        gameObject.tag = "Player";

        _wheelColliderFL.motorTorque = 1000f;
        _wheelColliderFR.motorTorque = 1000f;
    }

    private void FixedUpdate()
    {
        _speed = Math.Abs(GetComponent<Rigidbody>().linearVelocity.magnitude);
        Debug.Log("Speed: " + _speed);

        if (_speed <= 20)
        {
            _wheelColliderFL.motorTorque = 5000f;
            _wheelColliderFR.motorTorque = 5000f;
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
        else if (_speed < 20)
        {
            _wheelColliderFL.brakeTorque = 0;
            _wheelColliderFR.brakeTorque = 0;
            _wheelColliderBL.brakeTorque = 0;
            _wheelColliderBR.brakeTorque = 0;
            _wheelColliderFL.motorTorque = 1000f;
            _wheelColliderFR.motorTorque = 1000f;
        }

        // Получаем ввод от клавиатуры (стрелки влево и вправо или A и D)
        float move = Input.GetAxis("Horizontal") * -1;

        // Рассчитываем новое положение объекта
        Vector3 newPosition = transform.position + new Vector3(move, 0, 0) * _turningSpeed * Time.deltaTime;

        // Ограничиваем движение по оси X
        newPosition.x = Mathf.Clamp(newPosition.x, _minX, _maxX);

        // Применяем новое положение к объекту
        transform.position = newPosition;

        _tilt = move * _tiltAngle;

        // Применяем наклон по оси Z
        transform.rotation = Quaternion.Euler(0, 180, _tilt);



        RotateWheel(_wheelColliderFL, _transformFL);
        RotateWheel(_wheelColliderFR, _transformFR);
        RotateWheel(_wheelColliderBL, _transformBL);
        RotateWheel(_wheelColliderBR, _transformBR);

        UpdateVisualWheels(move);

    }



    private void RotateWheel(WheelCollider collider, Transform transform)
    {
        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);

        transform.rotation = rotation;
        transform.position = position;
    }


    private void UpdateVisualWheels(float steerInput)
    {
        float steerAngle = steerInput * _maxAngleOfWheel;

        // Поворачиваем только передние визуальные колеса
        _transformFL.localRotation = Quaternion.Euler(0, -steerAngle, 0);
        _transformFR.localRotation = Quaternion.Euler(0, -steerAngle, 0);
    }
}
