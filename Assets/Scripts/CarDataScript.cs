using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "CarDataScript", menuName = "Scriptable Objects/CarDataScript")]
[System.Serializable]
public class CarDataScript : ScriptableObject
{
    public string _nameOfCar;

    public GameObject _carPrefab;


    public int _costForBuyCar;
    //If price = 0, => object was bought
    public int[] _priceForBodies;

    public int[] _priceForWheels;

    public int[] _priceForEngines;

    public int[] _priceForWeapons;

    public int _bodyId;
    public int _engineId;
    public int _wheelsId;
    public int _weaponId;

    //Характеристики машины
    [SerializeField] public int[] _hpConfig;
    [SerializeField] public float[] _nitroConfig;
    [SerializeField] public int[] _tiltSpeedConfig;
    [SerializeField] public int[] _damageConfig;

    public void ChangeBodyID(int Id)
    {
        if (_priceForBodies[Id] == 0)
        {
            _bodyId = Id;
        }
    }
    public void ChangeEngineID(int Id)
    {
        if (_priceForBodies[Id] == 0)
        {
            _engineId = Id;
        }
    }
    public void ChangeWheelsID(int Id)
    {
        if (_priceForBodies[Id] == 0)
        {
            _wheelsId = Id;
        }
    }
    public void ChangeWeaponID(int Id)
    {
        if (_priceForBodies[Id] == 0)
        {
            _weaponId = Id;
        }
    }
   


}
