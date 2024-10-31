using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // ����������� ���������� ��� �������� ������������� ����������
    private static DataManager _instance;

    //Data
    public int _vehicleId;
    public int _currentIndexOfCar;
    public int _numberOfCoins = 100;
    public GameObject _currentCar;

   public List<List<string>> _playerData = new List<List<string>>();

    
   

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

    // ������ ������ ��� ���������� ���������� ����
    
}