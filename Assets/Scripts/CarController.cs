using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.UIElements.InputSystem;
using SimpleInputNamespace;

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


    //Car properties
    [SerializeField] private float _tiltAngle;
    [SerializeField] public float _turningSpeed;
    [SerializeField] public float _hp;
    [SerializeField] public float _firePower;
    [SerializeField] public float _nitroTime;

    [SerializeField] private float _maxAngleOfWheel;
    [SerializeField] private float _bodyRotation;
    [SerializeField] private float _speedOfBodyRotation;

    // ����������� ��� ������� ������
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;

    [SerializeField] private float _minYangle;
    [SerializeField] private float _maxYangle;

    [SerializeField] private float _minSpeed = 60;
    [SerializeField] private float _maxSpeed = 80;

    //Applying customization
    [SerializeField] CustomizationScript _customizationScript;

    //EffectController
    [SerializeField] private CarEffectController _carEffectController;
    [SerializeField] private CarAudioScript _audio;
    [SerializeField] private Animator _blinkAnimator;





    //�������� ������
    public float _speed;


    //auxiliary variables
    private float _tilt;
    private float _damageCoef = 1;
    private float _moveCoef = 1;
    private bool _isMoving = true;

    private float _distanceTraveled = 0f;


    private Rigidbody _rigidbody;

    private UI_Manager _uiManager;
    private CameraController _camera;



    public float _speedModificator = 1f;

    public float move = 0;



    void Start()
    {
        gameObject.tag = "Player";
        _rigidbody = GetComponent<Rigidbody>();
        
        InitializeCustomization();


        _camera = FindFirstObjectByType<CameraController>();
        _uiManager = FindFirstObjectByType<UI_Manager>();
        _uiManager._health = _hp;



        
    }
    public void StartCar()
    {
        _audio.PlayEngine();
    }

    public void ChangeMoveCoef(float coef)
    {
        _moveCoef = coef;
    }






    

    private void FixedUpdate()
    {
        _speed = Mathf.Abs(_rigidbody.linearVelocity.magnitude);
        //Debug.Log(_speed);
        
        MoveForward();

        move = SimpleInput.GetAxis( "Horizontal" ) * _speedModificator * _moveCoef * -1;

        //Получаем ввод от клавиатуры (стрелки влево и вправо или A и D)
        //move = Input.GetAxis("Horizontal") * _speedModificator * _moveCoef * -1;

        


        
        

        UpdatePositionAndRotation(move);

        RotateWheel(_wheelColliderBL, _transformBL);
        RotateWheel(_wheelColliderBR, _transformBR);
        RotateWheel(_wheelColliderFL, _transformFL);
        RotateWheel(_wheelColliderFR, _transformFR);


        UpdateVisuaFrontlWheels(move);

        _carEffectController.UpdateEffect(move);

       

    }

    private void MoveForward()
    {
        if (_isMoving)
        {
            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.y,
                -Math.Clamp(Mathf.Abs(_rigidbody.linearVelocity.z), _minSpeed, _maxSpeed) * _speedModificator);
        }
    }






    private void UpdateVisuaFrontlWheels(float steerInput)
    {
        float steerAngle = steerInput * _maxAngleOfWheel;

        // Поворачиваем только передние визуальные колеса
        _transformFL.localRotation = Quaternion.Euler(_transformFL.localRotation.eulerAngles.x, steerAngle, 0);
        _transformFR.localRotation = Quaternion.Euler(_transformFR.localRotation.eulerAngles.x, steerAngle, 0);

    }

    private void UpdatePositionAndRotation(float move)
    {
        // Новая позиция с ограничением по X
        Vector3 newPosition = transform.position + new Vector3(move, 0, 0) * _turningSpeed * Time.deltaTime;
        //Костыль
        newPosition.x = Mathf.Clamp(newPosition.x, -10, 17);

        // Применяем только при необходимости
        if (transform.position != newPosition)
            transform.position = newPosition;

        // Рассчитываем наклон и применяем плавное вращение
        float newTilt = move * _tiltAngle;
        float bodyRotation = move * _bodyRotation * _speedOfBodyRotation;
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
    
    

    

    private void InitializeCustomization()
    {
        //Визуальная часть
        CarDataScript _cardata = DataManager.Instance._currentCarData;
        _customizationScript.ShowBodyAtIndex(_cardata._bodyId);
        _customizationScript.ShowWeaponyAtIndex(_cardata._weaponId);
        _customizationScript.InitializeWheels(_cardata._wheelsId);

        //Программная часть
        _turningSpeed = _cardata._tiltSpeedConfig[_cardata._wheelsId];
        _hp = _cardata._hpConfig[_cardata._bodyId];
        _firePower = _cardata._damageConfig[_cardata._weaponId];
        _nitroTime = _cardata._nitroConfig[_cardata._engineId];

    }

    public void GetDamage(int damage)
    {
        _hp -= damage * _damageCoef;

        _uiManager.UpdateHealthBar(_hp);
        if (_hp <= 0)
        {
            DestroyCar();
            FindFirstObjectByType<UI_Manager>().GameOverUI();
            FindFirstObjectByType<AudioManagerController>().StopMusic();
            _audio.StopEngine();
        }
        else
        {
            _camera.CameraShake();

            StartCoroutine(Blinking());
        }
    }

    public void GetNitro()
    {
        _rigidbody.linearVelocity += new Vector3(0, 0, -_maxSpeed);
        _damageCoef = 0;
        _camera.CameraAcceleration(true);
        _carEffectController.SetNitro();
        _audio.PlayNitro();
        StartCoroutine(NitroDelay(_nitroTime));
        
    }

    private IEnumerator NitroDelay(float delay)
    {

        yield return new WaitForSecondsRealtime(delay); // Ждём реальное время
        _rigidbody.linearVelocity -= new Vector3(0, 0, -_minSpeed);
        _camera.CameraAcceleration(false);
        _carEffectController.ResetNitro();
        yield return new WaitForSecondsRealtime(1f);
        ChangeDamageCoef(1f);
    }
    public void ChangeDamageCoef(float damageCoef)
    {
        _damageCoef = damageCoef;
    }

    private void DestroyCar()
    {
        _isMoving = false;
        _rigidbody.linearVelocity = new Vector3(0, 2, 10);

        

        _moveCoef = 0f;
        _carEffectController.Explosion();
        _wheelColliderBL.gameObject.SetActive(false);
        _wheelColliderBR.gameObject.SetActive(false);
        _wheelColliderFL.gameObject.SetActive(false);
        _wheelColliderFR.gameObject.SetActive(false);
    }
    public void RespawnCar()
    {
        _isMoving = true;
        _moveCoef = 1f;

        _wheelColliderBL.gameObject.SetActive(true);
        _wheelColliderBR.gameObject.SetActive(true);
        _wheelColliderFL.gameObject.SetActive(true);
        _wheelColliderFR.gameObject.SetActive(true);

        _audio.PlayEngine();

        InitializeCustomization();
        _uiManager.UpdateHealthBar(_hp);

        StartCoroutine(RespawnDelay(2f));

    }


    public void PushFromBorder(Vector3 direction, float force, int damage)
    {
        _rigidbody.linearVelocity = new Vector3(direction.x * force, _rigidbody.linearVelocity.y, _rigidbody.linearVelocity.z);
        GetDamage(damage);
        StartCoroutine(Delay(0.8f));
    }

    private IEnumerator Blinking()
    {
        if (_isMoving && _damageCoef != 0)
        {
            
            _blinkAnimator.SetBool("Blink", true);
            yield return new WaitForSeconds(0.5f);
            _blinkAnimator.SetBool("Blink", false);

        }
    }

    private IEnumerator Delay(float delay)
    {
        if (_isMoving)
        {
            ChangeMoveCoef(0f);
            yield return new WaitForSeconds(delay);
            ChangeMoveCoef(1f);
        }
    }
    private IEnumerator RespawnDelay(float delay)
    {
        ChangeDamageCoef(0f);
        yield return new WaitForSeconds(delay);
        ChangeDamageCoef(1f);

    }

}
