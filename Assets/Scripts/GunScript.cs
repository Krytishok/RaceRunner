using UnityEngine;

public class GunScript : MonoBehaviour
{

    public Transform _target = null; // Цель, за которой следует следить
    public float rotationSpeed = 5f; // Скорость поворота
    public Transform _parent;

    [SerializeField] private ParticleSystem[] _shots;
    private int _weaponIndex;



    private Quaternion initialRotation; // Изначальный поворот

    void Start()
    {
        // Сохраняем изначальный поворот
        initialRotation = transform.rotation;
        _weaponIndex = DataManager.Instance._currentCarData._weaponId;
    }
    public void Shot()
    {
        _shots[_weaponIndex].Play();
    }

    public void SetTarget(Transform target=null)
    {
        _target = target;
    }

    public void Shoot()
    {

    }

   

    void FixedUpdate()
    {
        if (_target != null)
        {
            // Направление к цели
            Vector3 direction = (_target.position - transform.position).normalized;
            // Убираем смещение по оси Y, если нужно поворачиваться только в горизонтальной плоскости
            direction.y = 0;

            // Получаем поворот к цели
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Плавный поворот к цели
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed);
        }
        else
        {
            // Возвращаем пушку в исходное положение
            transform.rotation = _parent.rotation;
        }
    }
}

