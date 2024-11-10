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
    public GameObject[] cars;
    public GameObject _selectedCar;

    public void StopGame()
    {
        //Add collected coins in level to total
        NumberOfCoinsAtLevel += FindFirstObjectByType<CoinManager>()._numberOfCoinsInLevel;

        DataManager.Instance._numberOfCoins += NumberOfCoinsAtLevel;

        //PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + NumberOfCoinsAtAll);


        SceneManager.LoadScene("MainMenu");
        Debug.Log("Stop");
    }

    public void Awake()
    {

        _selectedCar = DataManager.Instance._currentCarData._carPrefab.gameObject;
        _selectedCar = Instantiate(_selectedCar, new Vector3(3,0, 30), new Quaternion(0, 180, 0, 0));

    }

}
