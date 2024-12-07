using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int NumberOfCoinsAtLevel = 0;
    private int incrementCoinToAll;

    //Link to Camera Script
    [SerializeField] CameraController _cameraController;

    //test ScriptableObject
    private CarDataScript _currentCarData;

    //List of Playable cars
    public GameObject[] _carPrefabs;
    public CarDataScript[] cars;
    public GameObject _selectedCar;

    //Events
    public bool _IsEnemyOnRoad = false;
    public bool _IsTimeToShoot = false;

    public void StopGame()
    {
        //Add collected coins in level to total
        NumberOfCoinsAtLevel += FindFirstObjectByType<CoinManager>()._numberOfCoinsInLevel;

        DataManager.Instance._numberOfCoins += NumberOfCoinsAtLevel;

        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + NumberOfCoinsAtLevel);


        SceneManager.LoadScene("MainMenu");
        Debug.Log("Stop");
    }

    public void Awake()
    {
        DataManager.Instance._currentCarData._carPrefab = _selectedCar = _carPrefabs[PlayerPrefs.GetInt("currentCarIndex")];
        _selectedCar = DataManager.Instance._currentCarData._carPrefab.gameObject;

        //if (DataManager.Instance._currentCarData._carPrefab == null)
        //{
        //    Debug.Log("SelectedCarIsNull");
        //    _selectedCar = _carPrefabs[PlayerPrefs.GetInt("currentCarIndex")];
        //}
        //else
        //{
        //    _selectedCar = DataManager.Instance._currentCarData._carPrefab.gameObject;
        //}


        _selectedCar = Instantiate(_selectedCar, new Vector3(3,0, 30), new Quaternion(0, 180, 0, 0));

        Time.timeScale = 0f;

    }

    public void SetTimeScaleTo(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        _selectedCar.GetComponent<CarController>().StartCar();
    }


    

    


}
