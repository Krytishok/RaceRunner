using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject _buttonPlay;
    [SerializeField] GameObject _buttonExit;
    [SerializeField] GameObject _buttonToGarage;
    [SerializeField] GameObject _buttonNext;
    [SerializeField] GameObject _buttonPrevious;
    [SerializeField] GameObject _buttonBuy;
    [SerializeField] GameObject _buttonBody;
    [SerializeField] GameObject _buttonEngine;
    [SerializeField] GameObject _buttonWheels;
    [SerializeField] GameObject _groupOfCustomizationButtons;

    [SerializeField] Image _infoBar;



    [SerializeField] GameObject _mainCamera;

    [SerializeField] GameObject _garagePositionOfCamera;
    [SerializeField] GameObject _mainMenuPositionOfCamera;


    [SerializeField] TextMeshProUGUI _NumberOfCoinsText;
    private int _NumberOfCoins = 0;



    private GameObject _hatchbackBox;
    private GameObject _muscleBox;



    private void Start()
    {
        Time.timeScale = 1.0f;
        _NumberOfCoinsText.text = DataManager.Instance._numberOfCoins.ToString();

        _buttonNext.SetActive(false);
        _buttonPrevious.SetActive(false);
        _buttonBuy.SetActive(false);

    }

    public void UpdateVisualCoins()
    {
        _NumberOfCoinsText.text = DataManager.Instance._numberOfCoins.ToString();
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
        _buttonToGarage.SetActive(false);

        _buttonNext.SetActive(true);
        _buttonPrevious.SetActive(true);
        _buttonBuy.SetActive(true);


    }
    public void HideOrSpawnBuyButton(bool flag)
    {
        _buttonBuy.SetActive(flag);
    }
    public void HideOrSpawnArrows(bool flag)
    {
        _buttonNext.SetActive(flag);
    }
    public void BodyKit2ButtonIsPressed()
    {
        //FindFirstObjectByType<CustomizationScript>().SetBodyKitToSecondLevel();
    }
    public void BodyKit1ButtonIsPressed()
    {
        //FindFirstObjectByType<CustomizationScript>().SetBodyKitToFirstLevel();
    }

}
