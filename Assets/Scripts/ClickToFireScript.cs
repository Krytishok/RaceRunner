using UnityEngine;

public class ClickToFireScript : MonoBehaviour
{
    [SerializeField] GameObject _buttonBody;

    private CarController _player;
    private EnemyController _enemy;
    private CameraController _camera;
    private GunScript _guns;

    private int _numberOfShots = 3;
    private int _shotCounter = 0;


    private void Start()
    {
        _buttonBody.SetActive(false);
        _player = FindFirstObjectByType<CarController>();
        _camera = FindFirstObjectByType<CameraController>();
        _guns = FindFirstObjectByType<GunScript>();

        InitializeNumberOfShots();



    }
    private void InitializeNumberOfShots()
    {
        int weaponIndex = DataManager.Instance._currentCarData._weaponId;
        if(weaponIndex == 0 )
        {
            _numberOfShots = 3;
        } else if(weaponIndex == 1 )
        {
            _numberOfShots = 5;
        } else if(weaponIndex == 2 )
        {
            _numberOfShots = 2;
        }
    }
    public void SetClickButton(bool flag)
    {
        _buttonBody.SetActive(flag);
        _shotCounter = 0;
    }

    public void OnClick()
    {
        if (_numberOfShots > _shotCounter)
        {
            FindFirstObjectByType<EnemyController>().GetDamage(_player._firePower);
            _camera.CameraShake();
            _guns.Shot();

            _shotCounter++;
        }
        else
        {
            FindFirstObjectByType<WeaponScript>().StopSlowMo();
            FindFirstObjectByType<EnemyController>().RestartTargetting();
            _shotCounter = 0;
        }
    }


}
