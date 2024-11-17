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

    //BodyCar
    [SerializeField] public Animator _bodyCarAnimator;

    //Car properties
    [SerializeField] private float _tiltAngle;
    [SerializeField] public float _turningSpeed;
    [SerializeField] public int _hp;
    [SerializeField] public int _firePower;
    [SerializeField] public float _nitroTime;

    [SerializeField] private float _maxAngleOfWheel;

    // ����������� ��� ������� ������
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;

    [SerializeField] private float _minYangle;
    [SerializeField] private float _maxYangle;

    [SerializeField] private float _minSpeed = 60;
    [SerializeField] private float _maxSpeed = 80;

    //Applying customization
    [SerializeField] CustomizationScript _customizationScript;



    //�������� ������
    public float _speed;


    //auxiliary variables
    private float _tilt;
    private Rigidbody _rigidbody;



    void Start()
    {
        gameObject.tag = "Player";
        _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.linearVelocity = new Vector3(0, 0, -40);

        InitializeCustomization();


    }

    private void FixedUpdate()
    {
        _speed = Mathf.Abs(_rigidbody.linearVelocity.magnitude);
        //Debug.Log("Speed: " + _speed);

        //_wheelColliderBL.motorTorque = Input.GetAxis("Vertical") * _forceEngine;
        //_wheelColliderBR.motorTorque = Input.GetAxis("Vertical") * _forceEngine;


        _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.y,
            -Math.Clamp(Mathf.Abs(_rigidbody.linearVelocity.z), _minSpeed, _maxSpeed));


        
        
        

        // Получаем ввод от клавиатуры (стрелки влево и вправо или A и D)
        float move = Input.GetAxis("Horizontal") * -1;

        UpdatePositionAndRotation(Input.GetAxis("Horizontal") * -1);

        RotateWheel(_wheelColliderBL, _transformBL);
        RotateWheel(_wheelColliderBR, _transformBR);
        RotateWheel(_wheelColliderFL, _transformFL);
        RotateWheel(_wheelColliderFR, _transformFR);


        UpdateVisuaFrontlWheels(move);
       

    }




    private void UpdateVisuaFrontlWheels(float steerInput)
    {
        float steerAngle = steerInput * _maxAngleOfWheel;

        // Поворачиваем только передние визуальные колеса
        _transformFL.localRotation = Quaternion.Euler(_transformFL.localRotation.eulerAngles.x, -steerAngle, 0);
        _transformFR.localRotation = Quaternion.Euler(_transformFR.localRotation.eulerAngles.x, -steerAngle, 0);
    }

    private void UpdatePositionAndRotation(float move)
    {
        // Новая позиция с ограничением по X
        Vector3 newPosition = transform.position + new Vector3(move, 0, 0) * _turningSpeed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, _minX, _maxX);

        // Применяем только при необходимости
        if (transform.position != newPosition)
            transform.position = newPosition;

        // Рассчитываем наклон и применяем плавное вращение
        float newTilt = move * _tiltAngle;
        float bodyRotation = move * 5;
        Quaternion targetRotation = Quaternion.Euler(0, 180 - bodyRotation, newTilt);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    private void RotateWheel(WheelCollider collider, Transform wheelTransform)
    {
        // Получаем позицию и вращение колес
        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);

        // Устанавливаем позицию колес
        wheelTransform.position = position;

        // Поворачиваем колесо на основе вращения коллайдера
        wheelTransform.rotation = rotation;


    }

    public void CollisionWithObstacle()
    {
        _bodyCarAnimator.SetTrigger("CollisionWithObstacle");
    }

    private void InitializeCustomization()
    {
        //Визуальная часть
        CarDataScript _cardata = DataManager.Instance._currentCarData;
        _customizationScript.ShowBodyAtIndex(_cardata._bodyId);
        _customizationScript.ShowWeaponyAtIndex(_cardata._weaponId);

        //Программная часть
        _turningSpeed = _cardata._tiltSpeedConfig[_cardata._wheelsId];
        _hp = _cardata._hpConfig[_cardata._bodyId];
        _firePower = _cardata._damageConfig[_cardata._weaponId];
        _nitroTime = _cardata._nitroConfig[_cardata._engineId];

    }

}
