using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class CarCollectionController : MonoBehaviour
{

    public GameObject[] cars; // Массив всех машин
    public int currentCarIndex = 0; // Индекс текущей машины


    public CarDataScript[] _carsData;


    private string _whichTuningToShow;
    private int _currentIndexOfTuning;

    public MainMenuScript _mainMenuScript;






    void Start()
    {
        currentCarIndex = PlayerPrefs.GetInt("currentCarIndex");
        ShowCar(currentCarIndex);
        InizializeCustomizationOfCar();


    }
    //Если машина не куплена, то убираем кнопку "Вернуться"
    private void UpdateInfoAboutBackToPreviousUIButton(int index)
    {
        if (_carsData[index]._costForBuyCar == 0)
        {
            FindFirstObjectByType<MainMenuScript>().HideOrSpawnBackToPreviosUIButton(true);
        }
        else
        {
            FindFirstObjectByType<MainMenuScript>().HideOrSpawnBackToPreviosUIButton(false);
        }

    }


    public void ShowCar(int index)
    {
        // Отключаем текущую машину
        if (currentCarIndex >= 0 && currentCarIndex < cars.Length)
        {
            cars[currentCarIndex].SetActive(false);
        }

        // Обновляем индекс текущей машины
        currentCarIndex = index;
        PlayerPrefs.SetInt("currentCarIndex", index);



        // Включаем новую машину
        if (currentCarIndex >= 0 && currentCarIndex < cars.Length)
        {
            cars[currentCarIndex].SetActive(true);
        }

    }

    public void NextCar()
    {
        // Переход к следующей машине
        int newIndex = (currentCarIndex + 1) % cars.Length; // Циклический переход
        ShowCar(newIndex);
        UpdateInfoAboutBackToPreviousUIButton(newIndex);

        

    }

    public void PreviousCar()
    {
        // Переход к предыдущей машине
        int newIndex = (currentCarIndex - 1 + cars.Length) % cars.Length; // Циклический переход
        ShowCar(newIndex);
        UpdateInfoAboutBackToPreviousUIButton(newIndex);

    }
    public void BuyCar()
    {
        if(DataManager.Instance._numberOfCoins >= _carsData[currentCarIndex]._costForBuyCar)
        {
            DataManager.Instance._numberOfCoins -= _carsData[currentCarIndex]._costForBuyCar;

            _carsData[currentCarIndex]._costForBuyCar = 0;
            //Устанавливаем цену машины в 0, т.к. она куплена
            FindFirstObjectByType<MainMenuScript>().UpdatePriceForCar();
            DataManager.Instance._currentCarData = _carsData[currentCarIndex];



        }
    }

    //Вызывается тремя кнопками для отображения уровня запчасти
    public void ShowCurrentTuning(int index)
    {

        _whichTuningToShow = FindFirstObjectByType<MainMenuScript>()._currentCustomization;
        _currentIndexOfTuning = index;

        if (_whichTuningToShow == "Body")
        {

            _carsData[currentCarIndex].ChangeBodyID(index);
            FindFirstObjectByType<CustomizationScript>().ShowBodyAtIndex(index);
            FindFirstObjectByType<MainMenuScript>().UpdatePriceForTuning(index);


        } else if(_whichTuningToShow == "Engine")
        {
            _carsData[currentCarIndex].ChangeEngineID(index);
            FindFirstObjectByType<CustomizationScript>().ShowEngineAtIndex(index);
            FindFirstObjectByType<MainMenuScript>().UpdatePriceForTuning(index);

        } else if (_whichTuningToShow == "Wheels")
        {
            _carsData[currentCarIndex].ChangeWheelsID(index);
            FindFirstObjectByType<CustomizationScript>().ShowWheelsAtIndex(index);
            FindFirstObjectByType<MainMenuScript>().UpdatePriceForTuning(index);

        } else if(_whichTuningToShow == "Weapon")
        {
            _carsData[currentCarIndex].ChangeWeaponID(index);
            FindFirstObjectByType<CustomizationScript>().ShowWeaponyAtIndex(index);
            FindFirstObjectByType<MainMenuScript>().UpdatePriceForTuning(index);

        }
        //Показываем характеристики автомобиля
        _mainMenuScript.UpdateTuningConfig(_carsData[currentCarIndex], _whichTuningToShow, index);
    }


    //Данную функцию активирует кнопка BuyTuningButton
    public void BuyTuning()
    {
        //Строка с текущим выбранной вкладкой тюнинга
        _whichTuningToShow = FindFirstObjectByType<MainMenuScript>()._currentCustomization;
        if (_whichTuningToShow == "Body")
        {
            if (DataManager.Instance._numberOfCoins >= _carsData[currentCarIndex]._priceForBodies[_currentIndexOfTuning])
            {
                DataManager.Instance._numberOfCoins -= _carsData[currentCarIndex]._priceForBodies[_currentIndexOfTuning];
                //Инициализируем покупку в SO
                _carsData[currentCarIndex]._priceForBodies[_currentIndexOfTuning] = 0;
                FindFirstObjectByType<MainMenuScript>().HideOrSpawnCustomizationButtons(true);
            } 
        }
        else if (_whichTuningToShow == "Engine")
        {
            if (DataManager.Instance._numberOfCoins >= _carsData[currentCarIndex]._priceForEngines[_currentIndexOfTuning])
            {
                DataManager.Instance._numberOfCoins -= _carsData[currentCarIndex]._priceForEngines[_currentIndexOfTuning];
                //Инициализируем покупку в SO
                _carsData[currentCarIndex]._priceForEngines[_currentIndexOfTuning] = 0;
                FindFirstObjectByType<MainMenuScript>().HideOrSpawnCustomizationButtons(true);

            }
        }
        else if (_whichTuningToShow == "Wheels")
        {
            if (DataManager.Instance._numberOfCoins >= _carsData[currentCarIndex]._priceForWheels[_currentIndexOfTuning])
            {
                DataManager.Instance._numberOfCoins -= _carsData[currentCarIndex]._priceForWheels[_currentIndexOfTuning];
                //Инициализируем покупку в SO
                _carsData[currentCarIndex]._priceForWheels[_currentIndexOfTuning] = 0;
                FindFirstObjectByType<MainMenuScript>().HideOrSpawnCustomizationButtons(true);
            }
        }
        else if (_whichTuningToShow == "Weapon")
        {
            if (DataManager.Instance._numberOfCoins >= _carsData[currentCarIndex]._priceForWeapons[_currentIndexOfTuning])
            {
                DataManager.Instance._numberOfCoins -= _carsData[currentCarIndex]._priceForWeapons[_currentIndexOfTuning];
                //Инициализируем покупку в SO
                _carsData[currentCarIndex]._priceForWeapons[_currentIndexOfTuning] = 0;
                FindFirstObjectByType<MainMenuScript>().HideOrSpawnCustomizationButtons(true);
            }
        }
        //Обновляем визуальные данные
        FindFirstObjectByType<MainMenuScript>().UpdateVisualCoins();
        FindFirstObjectByType<MainMenuScript>().UpdatePriceForTuning(_currentIndexOfTuning);
    }

    public CarDataScript GetCurrentCarData()
    {
        return _carsData[currentCarIndex];
    }

    public void SelectCar()
    {
        DataManager.Instance._currentCarData = _carsData[currentCarIndex];
        PlayerPrefs.SetInt("currentCarInex", currentCarIndex);

        //Обновляем текст с характеристиками машины
        _mainMenuScript.UpdateTuningConfig(_carsData[currentCarIndex], "Body", _carsData[currentCarIndex]._bodyId);
        _mainMenuScript.UpdateTuningConfig(_carsData[currentCarIndex], "Engine", _carsData[currentCarIndex]._engineId);
        _mainMenuScript.UpdateTuningConfig(_carsData[currentCarIndex], "Wheels", _carsData[currentCarIndex]._wheelsId);
        _mainMenuScript.UpdateTuningConfig(_carsData[currentCarIndex], "Weapon", _carsData[currentCarIndex]._weaponId);

    }

    public void InizializeCustomizationOfCar()
    {
        CarDataScript car = _carsData[currentCarIndex];
        FindFirstObjectByType<CustomizationScript>().Initialize(car._bodyId, car._engineId, car._wheelsId, car._weaponId);
    }
    public void HideOrSpawnCarHood(bool flag)
    {
        FindFirstObjectByType<CustomizationScript>().HideOrSpawnCarHood(flag);
    }



}
