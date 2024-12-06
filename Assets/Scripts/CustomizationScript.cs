using System.Collections.Generic;
using UnityEngine;

public class CustomizationScript : MonoBehaviour
{


    [SerializeField] GameObject[] _listOfBodies;
    [SerializeField] GameObject[] _listOfWheels;
    [SerializeField] GameObject[] _listOfEngines;
    [SerializeField] GameObject[] _listOfWeapons;

    [SerializeField] GameObject _carHood;

    [Header("reference materials")]
    [SerializeField] string[] _textsBody;
    [SerializeField] string[] _textsEngine;
    [SerializeField] string[] _textsWheels;
    [SerializeField] string[] _textsWeapons;



    //Для отображения колес в игровой сцене
    [Header("Settings for Playable Scene")]


     [SerializeField] GameObject[] _wheels1Level;
     [SerializeField] GameObject[] _wheels2Level;
     [SerializeField] GameObject[] _wheels3Level;

    private MainMenuScript _mainMenu;



    private void Start()
    {
       _mainMenu = FindFirstObjectByType<MainMenuScript>();
    }

    public void ShowBodyAtIndex(int index)
    {
        for (int i = 0; i < _listOfBodies.Length; i++)
        {
            _listOfBodies[i].SetActive(false);
        }
        _listOfBodies[index].SetActive(true);

        if (_mainMenu != null)
        {
            _mainMenu.ChangeEducationText(_textsBody[index]);
        }

    }
    public void ShowEngineAtIndex(int index)
    {
        for (int i = 0; i < _listOfEngines.Length; i++)
        {
            _listOfEngines[i].SetActive(false);
        }
        _listOfEngines[index].SetActive(true);

        if (_mainMenu != null)
        {
            _mainMenu.ChangeEducationText(_textsEngine[index]);
        }

    }
    public void ShowWheelsAtIndex(int index)
    {
        for (int i = 0; i < _listOfWheels.Length; i++)
        {
            _listOfWheels[i].SetActive(false);
        }
        _listOfWheels[index].SetActive(true);
        if (_mainMenu != null)
        {
            _mainMenu.ChangeEducationText(_textsWheels[index]);
        }
    }
    public void ShowWeaponyAtIndex(int index)
    {
        for (int i = 0; i < _listOfWeapons.Length; i++)
        {
            _listOfWeapons[i].SetActive(false);
        }
        _listOfWeapons[index].SetActive(true);
        if (_mainMenu != null)
        {
            _mainMenu.ChangeEducationText(_textsWeapons[index]);
        }

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

    public void InitializeWheels(int index)
    {
        Debug.Log("InitializeFunction is started");
        Debug.Log(_wheels1Level);
        List<GameObject[]> _listOfPlayableWheels = new List<GameObject[]>() {_wheels1Level, _wheels2Level, _wheels3Level};
        Debug.Log(_listOfPlayableWheels.Count);
        for(int i = 0; i < _listOfPlayableWheels.Count; i++)
        {
            HideOrSpawnAllWheels(_listOfPlayableWheels[i], false);
        }
        HideOrSpawnAllWheels(_listOfPlayableWheels[index], true);

    }
    private void HideOrSpawnAllWheels(GameObject[] _wheelsArray, bool flag)
    {
        for(int i=0; i < _wheelsArray.Length; i++)
        {
            _wheelsArray[i].SetActive(flag);
        }
    }




}
