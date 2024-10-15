using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int NumberOfCoinsAtAll = 0;
    private int incrementCoinToAll;

    //Link to Camera Script
    [SerializeField] CameraController _cameraController;

    //List of Playable cars
    public GameObject[] cars;
    public GameObject _selectedCar;

    public void StopGame()
    {
        //Add collected coins in level to total
        NumberOfCoinsAtAll += FindFirstObjectByType<CoinManager>()._numberOfCoinsInLevel;

        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + NumberOfCoinsAtAll);


        SceneManager.LoadScene("MainMenu");
        Debug.Log("Stop");
    }

    public void Awake()
    {
        _selectedCar = cars[PlayerPrefs.GetInt("Vehicle")].gameObject;
        _selectedCar = Instantiate(_selectedCar, new Vector3(0,0, 20), new Quaternion(0, 180, 0, 0));

    }

}
