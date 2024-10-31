using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // Статическая переменная для хранения единственного экземпляра
    private static DataManager _instance;

    //Data
    public int _vehicleId;
    public int _currentIndexOfCar;
    public int _numberOfCoins = 100;
    public GameObject _currentCar;

   public List<List<string>> _playerData = new List<List<string>>();

    
   

    // Публичное статическое свойство для доступа к экземпляру
    public static DataManager Instance
    {
        get
        {
            // Если экземпляр не существует, создаем его
            if (_instance == null)
            {
                // Создаем новый объект и добавляем к нему компонент DataManager
                _instance = new GameObject("GameManager").AddComponent<DataManager>();
            }
            return _instance;
        }
    }

    // Метод Awake вызывается при инициализации объекта
    private void Awake()
    {
        // Проверяем, существует ли уже экземпляр
        if (_instance == null)
        {
            // Если экземпляр не существует, назначаем текущий объект и не уничтожаем его при загрузке новой сцены
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Если экземпляр уже существует, уничтожаем текущий объект, чтобы сохранить единственность
            Destroy(gameObject);
        }
    }

    // Пример метода для управления состоянием игры
    
}