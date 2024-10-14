using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject _buttonPlay;
    [SerializeField] GameObject _buttonExit;
    [SerializeField] GameObject _buttonToGarage;

    [SerializeField] GameObject _mainCamera;

    [SerializeField] GameObject _garagePositionOfCamera;
    [SerializeField] GameObject _mainMenuPositionOfCamera;


    [SerializeField] TextMeshProUGUI _NumberOfCoinsText;
    private int _NumberOfCoins = 0;


    



    private void Start()
    {
        Time.timeScale = 1.0f;
        _NumberOfCoinsText.text = PlayerPrefs.GetInt("Coins").ToString();

    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ToGarage()
    {
        _mainCamera.transform.position = _garagePositionOfCamera.transform.position;
        _mainCamera.transform.rotation = _garagePositionOfCamera.transform.rotation;

        _buttonExit.SetActive(false);
        _buttonPlay.SetActive(false);
        _buttonToGarage.SetActive(false);


    }
}
