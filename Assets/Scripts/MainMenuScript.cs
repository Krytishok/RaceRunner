using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using YG;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject[] _buttonsChooseTuning;

    [SerializeField] GameObject _buttonPlay;
    [SerializeField] GameObject _buttonExit;
    [SerializeField] GameObject _buttonToGarage;
    [SerializeField] GameObject _groupArrowButtons;
    [SerializeField] GameObject _buttonBuy;
    [SerializeField] GameObject _buttonBody;
    [SerializeField] GameObject _buttonEngine;
    [SerializeField] GameObject _buttonWheels;
    [SerializeField] GameObject _groupOfCustomizationButtons;
    [SerializeField] GameObject _buttonSelect;
    [SerializeField] GameObject _buttonBuyTuning;
    [SerializeField] GameObject _buttonBackToPreviousUI;

    [SerializeField] GameObject _infoBar;
    [SerializeField] GameObject _lockIcon;

    [SerializeField] TextMeshProUGUI _priceForBuy;
    [SerializeField] TextMeshProUGUI _priceForBuyTuning;

    [SerializeField] TextMeshProUGUI _hpConfigNumber;
    [SerializeField] TextMeshProUGUI _nitroTimeNumber;
    [SerializeField] TextMeshProUGUI _controllabilityNumber;
    [SerializeField] TextMeshProUGUI _firePowerNumber;
    [SerializeField] TextMeshProUGUI _educationText;




    [SerializeField] TextMeshProUGUI _NumberOfCoinsText;
    private int _NumberOfCoins = 0;

    //Вспомогательная переменная для хранения информации о текущем окне кастомизации
    public string _currentCustomization;

    public string _currentUIstyle;

    CarCollectionController CarCollectionController;




    private void Start()
    {
        Time.timeScale = 1.0f;
        //Костыль
        DataManager.Instance._numberOfCoins = PlayerPrefs.GetInt("Coins");
        _NumberOfCoinsText.text = DataManager.Instance._numberOfCoins.ToString();


        _currentUIstyle = "MainMenu";

        CarCollectionController = FindFirstObjectByType<CarCollectionController>();

    }

    public void UpdateVisualCoins()
    {
        _NumberOfCoinsText.text = DataManager.Instance._numberOfCoins.ToString();
    }

    public void PlayGame()
    {

        // Дополнительная мера передачи информации о текущей машинке в следующую сцену
        if (CarCollectionController.GetCurrentCarData() != null && CarCollectionController.GetCurrentCarData()._costForBuyCar == 0)
        {
            DataManager.Instance._currentCarData = CarCollectionController.GetCurrentCarData();
            SceneManager.LoadScene("FirstLevel");
            YandexGame.FullscreenShow();
        }
        else
        {
            Debug.Log("Incorrect Car");
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
   
    public void HideOrSpawnBuyButton(bool flag)
    {
        _buttonBuy.SetActive(flag);
    }

    private CarDataScript GetCardata()
    {
        return CarCollectionController.GetCurrentCarData();
    }

    public void HideOrSpawnSelectButton(bool flag)
    {
        _buttonSelect.SetActive(flag);
    }
    
    
    public void UpdatePriceForCar()
    {
        if (GetCardata()._costForBuyCar == 0)
        {
            _buttonBuy.SetActive(false);
            _lockIcon.SetActive(false);
            _buttonSelect.SetActive(true);
        }
        else
        {
            _buttonSelect.SetActive(false);
            _buttonBuy.SetActive(true);
            _lockIcon.SetActive(true);
            _priceForBuy.text = GetCardata()._costForBuyCar.ToString();
        }

    }

    public void UpdatePriceForTuning(int index)
    {
        CarDataScript _cardata = GetCardata();
        if (_currentCustomization == "Body")
        {
            if (_cardata._priceForBodies[index] == 0)
            {
                _buttonBuyTuning.SetActive(true);
                _lockIcon.SetActive(false);
                _buttonBackToPreviousUI.SetActive(true);
                _groupOfCustomizationButtons.SetActive(true);
                _priceForBuyTuning.text = "КУПЛЕНО";

            }
            else
            {
                _buttonBuyTuning.SetActive(true);
                _lockIcon.SetActive(true);
                _buttonBackToPreviousUI.SetActive(false);
                _groupOfCustomizationButtons.SetActive(false);
                _priceForBuyTuning.text = _cardata._priceForBodies[index].ToString();
            }
        }
        else if (_currentCustomization == "Engine")
        {
            Debug.Log(index);
            if (_cardata._priceForEngines[index] == 0)
            {
                _buttonBuyTuning.SetActive(true);
                _lockIcon.SetActive(false);
                _buttonBackToPreviousUI.SetActive(true);
                _groupOfCustomizationButtons.SetActive(true);
                _priceForBuyTuning.text = "КУПЛЕНО";
            }
            else
            {
                _buttonBuyTuning.SetActive(true);
                _lockIcon.SetActive(true);
                _buttonBackToPreviousUI.SetActive(false);
                _groupOfCustomizationButtons.SetActive(false);
                _priceForBuyTuning.text = _cardata._priceForEngines[index].ToString();
            }
        }
        else if (_currentCustomization == "Wheels")
        {
            if (_cardata._priceForWheels[index] == 0)
            {
                _buttonBuyTuning.SetActive(true);
                _lockIcon.SetActive(false);
                _buttonBackToPreviousUI.SetActive(true);
                _groupOfCustomizationButtons.SetActive(true);
                _priceForBuyTuning.text = "КУПЛЕНО";
            }
            else
            {
                _buttonBuyTuning.SetActive(true);
                _lockIcon.SetActive(true);
                _buttonBackToPreviousUI.SetActive(false);
                _groupOfCustomizationButtons.SetActive(false);
                _priceForBuyTuning.text = _cardata._priceForWheels[index].ToString();
            }
        }
        else if (_currentCustomization == "Weapon")
        {
            if (_cardata._priceForWeapons[index] == 0)
            {
                _buttonBuyTuning.SetActive(true);
                _lockIcon.SetActive(false);
                _buttonBackToPreviousUI.SetActive(true);
                _groupOfCustomizationButtons.SetActive(true);
                _priceForBuyTuning.text = "КУПЛЕНО";
            }
            else
            {
                _buttonBuyTuning.SetActive(true);
                _lockIcon.SetActive(true);
                _buttonBackToPreviousUI.SetActive(false);
                _groupOfCustomizationButtons.SetActive(false);
                _priceForBuyTuning.text = _cardata._priceForWeapons[index].ToString();
            }
        }
    }
    //Данную функцию вызывают кнопки кастомизации и дают нам знать о нажатой кастомизации
    public void ChangeCurrentCustomization(string customizationName)
    {
        _currentCustomization = customizationName;
        for(int i = 0; i < _buttonsChooseTuning.Length; i++)
        {
            _buttonsChooseTuning[i].SetActive(false);
        }
        _buttonBuyTuning.SetActive(false);
    }
    
    public void BackToPreviosStyleUI()
    {
        if (_currentUIstyle == "CustomizationOfCar")
        {
            //Убираем текущий UI
            _groupOfCustomizationButtons.SetActive(false);
            _buttonBuyTuning.SetActive(false);
            _lockIcon.SetActive(false);
            _infoBar.SetActive(false);
            //Активируем предыдущий
            UpdatePriceForCar();
            _groupArrowButtons.SetActive(true);

            FindFirstObjectByType<MenuCameraController>().ToSelectView();
            FindFirstObjectByType<CustomizationScript>().HideOrSpawnCarHood(true);
            _currentUIstyle = "SelectingCar";



        }
        else if (_currentUIstyle == "SelectingCar")
        {
            _groupArrowButtons.SetActive(false);
            _buttonBuy.SetActive(false);
            _buttonSelect.SetActive(false);
            _buttonBackToPreviousUI.SetActive(false);
            _lockIcon.SetActive(false);

            _buttonToGarage.SetActive(true);

            FindFirstObjectByType<MenuCameraController>().ToMainMenuView();

            _currentUIstyle = "MainMenu";

        } else if(_currentUIstyle == "MainMenu")
        {
            _buttonBackToPreviousUI.SetActive(false);
        }
    }
    public void ChangeCurrentUIstyleInfo(string uiStyle)
    {
        _currentUIstyle = uiStyle;

    }
    public void HideOrSpawnBackToPreviosUIButton(bool flag)
    {
        _buttonBackToPreviousUI.SetActive(flag);
        
    }
    public void HideOrSpawnCustomizationButtons(bool flag)
    {
        _groupOfCustomizationButtons.SetActive(flag);
    }

    public void UpdateTuningConfig(CarDataScript _config, string _typeOfTuning, int index)
    {
        if (_typeOfTuning == "Body")
        {
            _hpConfigNumber.text = _config._hpConfig[index].ToString();
        }
        else if (_typeOfTuning == "Engine")
        {
            _nitroTimeNumber.text = _config._nitroConfig[index].ToString();
        }
        else if (_typeOfTuning == "Wheels")
        {
            _controllabilityNumber.text = _config._tiltSpeedConfig[index].ToString();
        }
        else if (_typeOfTuning == "Weapon")
        {
            _firePowerNumber.text = _config._damageConfig[index].ToString();
        }
        
        

    }

    public void ChangeEducationText(string _text)
    {
        _educationText.text = _text;
    }
    
    

}
