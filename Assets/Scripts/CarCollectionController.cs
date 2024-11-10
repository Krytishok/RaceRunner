using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CarCollectionController : MonoBehaviour
{

    public GameObject[] cars; // ������ ���� �����
    public int currentCarIndex = 0; // ������ ������� ������


    public CarDataScript[] _carsData;


    private string _whichTuningToShow;
    private int _currentIndexOfTuning;






void Start()
    {
        currentCarIndex = PlayerPrefs.GetInt("currentCarIndex");
        ShowCar(currentCarIndex);
        InizializeCustomizationOfCar();


    }

    public void ShowCar(int index)
    {
        // ��������� ������� ������
        if (currentCarIndex >= 0 && currentCarIndex < cars.Length)
        {
            cars[currentCarIndex].SetActive(false);
        }

        // ��������� ������ ������� ������
        currentCarIndex = index;
        PlayerPrefs.SetInt("currentCarIndex", index);


        // �������� ����� ������
        if (currentCarIndex >= 0 && currentCarIndex < cars.Length)
        {
            cars[currentCarIndex].SetActive(true);
        }

    }

    public void NextCar()
    {
        // ������� � ��������� ������
        int newIndex = (currentCarIndex + 1) % cars.Length; // ����������� �������
        ShowCar(newIndex);

    }

    public void PreviousCar()
    {
        // ������� � ���������� ������
        int newIndex = (currentCarIndex - 1 + cars.Length) % cars.Length; // ����������� �������
        ShowCar(newIndex);
    }
    public void BuyCar()
    {
        if(DataManager.Instance._numberOfCoins >= _carsData[currentCarIndex]._costForBuyCar)
        {
            DataManager.Instance._numberOfCoins -= _carsData[currentCarIndex]._costForBuyCar;

            _carsData[currentCarIndex]._costForBuyCar = 0;
            //������������� ���� ������ � 0, �.�. ��� �������
            FindFirstObjectByType<MainMenuScript>().UpdatePriceForCar();
            DataManager.Instance._currentCarData = _carsData[currentCarIndex];



        }
    }

    //���������� ����� �������� ��� ����������� ������ ��������
    public void ShowCurrentTuning(int index)
    {

        _whichTuningToShow = FindFirstObjectByType<MainMenuScript>()._currentCustomization;
        _currentIndexOfTuning = index;
        if (_whichTuningToShow == "Body")
        {
            FindFirstObjectByType<CustomizationScript>().ShowBodyAtIndex(index);
            FindFirstObjectByType<MainMenuScript>().UpdatePriceForTuning(index);

            _carsData[currentCarIndex].ChangeBodyID(index);
        } else if(_whichTuningToShow == "Engine")
        {
            FindFirstObjectByType<CustomizationScript>().ShowEngineAtIndex(index);
            FindFirstObjectByType<MainMenuScript>().UpdatePriceForTuning(index);

            _carsData[currentCarIndex].ChangeEngineID(index);
        } else if (_whichTuningToShow == "Wheels")
        {
            FindFirstObjectByType<CustomizationScript>().ShowWheelsAtIndex(index);
            FindFirstObjectByType<MainMenuScript>().UpdatePriceForTuning(index);

            _carsData[currentCarIndex].ChangeWheelsID(index);
        } else if(_whichTuningToShow == "Weapon")
        {
            FindFirstObjectByType<CustomizationScript>().ShowWeaponyAtIndex(index);
            FindFirstObjectByType<MainMenuScript>().UpdatePriceForTuning(index);

            _carsData[currentCarIndex].ChangeWeaponID(index);
        }
    }


    //������ ������� ���������� ������ BuyTuningButton
    public void BuyTuning()
    {
        //������ � ������� ��������� �������� �������
        _whichTuningToShow = FindFirstObjectByType<MainMenuScript>()._currentCustomization;
        if (_whichTuningToShow == "Body")
        {
            if (DataManager.Instance._numberOfCoins >= _carsData[currentCarIndex]._priceForBodies[_currentIndexOfTuning])
            {
                DataManager.Instance._numberOfCoins -= _carsData[currentCarIndex]._priceForBodies[_currentIndexOfTuning];
                //�������������� ������� � SO
                _carsData[currentCarIndex]._priceForBodies[_currentIndexOfTuning] = 0;
            } 
        }
        else if (_whichTuningToShow == "Engine")
        {
            if (DataManager.Instance._numberOfCoins >= _carsData[currentCarIndex]._priceForEngines[_currentIndexOfTuning])
            {
                DataManager.Instance._numberOfCoins -= _carsData[currentCarIndex]._priceForEngines[_currentIndexOfTuning];
                //�������������� ������� � SO
                _carsData[currentCarIndex]._priceForEngines[_currentIndexOfTuning] = 0;

            }
        }
        else if (_whichTuningToShow == "Wheels")
        {
            if (DataManager.Instance._numberOfCoins >= _carsData[currentCarIndex]._priceForWheels[_currentIndexOfTuning])
            {
                DataManager.Instance._numberOfCoins -= _carsData[currentCarIndex]._priceForWheels[_currentIndexOfTuning];
                //�������������� ������� � SO
                _carsData[currentCarIndex]._priceForWheels[_currentIndexOfTuning] = 0;
            }
        }
        else if (_whichTuningToShow == "Weapon")
        {
            if (DataManager.Instance._numberOfCoins >= _carsData[currentCarIndex]._priceForWeapons[_currentIndexOfTuning])
            {
                DataManager.Instance._numberOfCoins -= _carsData[currentCarIndex]._priceForWeapons[_currentIndexOfTuning];
                //�������������� ������� � SO
                _carsData[currentCarIndex]._priceForWeapons[_currentIndexOfTuning] = 0;
            }
        }
        //��������� ���������� ������
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
    }

    public void InizializeCustomizationOfCar()
    {
        CarDataScript car = _carsData[currentCarIndex];
        FindFirstObjectByType<CustomizationScript>().Initialize(car._bodyId, car._engineId, car._wheelsId, car._weaponId);
    }

}
