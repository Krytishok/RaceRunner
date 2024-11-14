using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomizationScript : MonoBehaviour
{


    [SerializeField] GameObject[] _listOfBodies;
    [SerializeField] GameObject[] _listOfWheels;
    [SerializeField] GameObject[] _listOfEngines;
    [SerializeField] GameObject[] _listOfWeapons;

    [SerializeField] GameObject _carHood;



    private void Start()
    {
       
    }

    public void ShowBodyAtIndex(int index)
    {
        for (int i = 0; i < _listOfBodies.Length; i++)
        {
            _listOfBodies[i].SetActive(false);
        }
        _listOfBodies[index].SetActive(true);

    }
    public void ShowEngineAtIndex(int index)
    {
        for (int i = 0; i < _listOfEngines.Length; i++)
        {
            _listOfEngines[i].SetActive(false);
        }
        _listOfEngines[index].SetActive(true);

    }
    public void ShowWheelsAtIndex(int index)
    {
        for (int i = 0; i < _listOfWheels.Length; i++)
        {
            _listOfWheels[i].SetActive(false);
        }
        _listOfWheels[index].SetActive(true);

    }
    public void ShowWeaponyAtIndex(int index)
    {
        for (int i = 0; i < _listOfWeapons.Length; i++)
        {
            _listOfWeapons[i].SetActive(false);
        }
        _listOfWeapons[index].SetActive(true);

    }
    public void HideOrSpawnCarHood(bool flag)
    {
        _carHood.SetActive(flag);
    }
    public void Initialize(int bodyId, int engineId, int wheelsId, int weaponId)
    {
        ShowBodyAtIndex(bodyId);
        ShowEngineAtIndex(engineId);
        ShowWheelsAtIndex(wheelsId);
        ShowWeaponyAtIndex(weaponId);
    }



}
