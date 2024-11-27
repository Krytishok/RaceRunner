using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DataManager : MonoBehaviour
{
    // ����������� ���������� ��� �������� ������������� ����������
    private static DataManager _instance;

    //Data
    public int _vehicleId;
    public int _currentIndexOfCar;
    public int _numberOfCoins = 0;
    public GameObject _currentCar;

    public CarDataScript _currentCarData;


    
   

    // ��������� ����������� �������� ��� ������� � ����������
    public static DataManager Instance
    {
        get
        {
            // ���� ��������� �� ����������, ������� ���
            if (_instance == null)
            {
                // ������� ����� ������ � ��������� � ���� ��������� DataManager
                _instance = new GameObject("GameManager").AddComponent<DataManager>();
            }
            return _instance;
        }
    }

    // ����� Awake ���������� ��� ������������� �������
    private void Awake()
    {
        // ���������, ���������� �� ��� ���������
        if (_instance == null)
        {
            // ���� ��������� �� ����������, ��������� ������� ������ � �� ���������� ��� ��� �������� ����� �����
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // ���� ��������� ��� ����������, ���������� ������� ������, ����� ��������� ��������������
            Destroy(gameObject);
        }
    }

    public static void SaveData(ScriptableObject data, string key)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
        Debug.Log(key + " DATA IS SUCCESFULLY SAVED");
    }

    public static void LoadData(ScriptableObject data, string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            JsonUtility.FromJsonOverwrite(json, data);
            Debug.Log(key + " DATA IS SUCCESFULLY LOADED");
        }
    }

}