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
    [SerializeField] GameObject _buttonMuscle;
    [SerializeField] GameObject _buttonHatchback;

    [SerializeField] GameObject _carCollection;


    [SerializeField] GameObject _mainCamera;

    [SerializeField] GameObject _garagePositionOfCamera;
    [SerializeField] GameObject _mainMenuPositionOfCamera;


    [SerializeField] TextMeshProUGUI _NumberOfCoinsText;
    private int _NumberOfCoins = 0;


    



    private void Start()
    {
        Time.timeScale = 1.0f;
        _NumberOfCoinsText.text = PlayerPrefs.GetInt("Coins").ToString();

        _buttonHatchback.SetActive(false);
        _buttonMuscle.SetActive(false);

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

        _buttonMuscle.SetActive(true);
        _buttonHatchback.SetActive(true);


    }
    public void ChooseHatch()
    {
        _carCollection.GetComponentAtIndex(0).gameObject.SetActive(true);
        _carCollection.GetComponentAtIndex(1).gameObject.SetActive(false);
        PlayerPrefs.SetString("Vehicle", "Hatchback");
    }
    public void ChooseMuscle()
    {
        _carCollection.GetComponentAtIndex(0).gameObject.SetActive(false);
        _carCollection.GetComponentAtIndex(1).gameObject.SetActive(true);
        PlayerPrefs.SetString("Vehicle", "Muscle");
    }
}
