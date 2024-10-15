using UnityEngine;

public class CarCollectionController : MonoBehaviour
{

    public GameObject[] cars; // ������ ���� �����
    private int currentCarIndex = 0; // ������ ������� ������

    void Start()
    {
        if (PlayerPrefs.HasKey("Vehicle"))
        {
            ShowCar(PlayerPrefs.GetInt("Vehicle"));
        }
        else
        {
            ShowCar(0);
        }
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
        PlayerPrefs.SetInt("Vehicle", newIndex);
    }

    public void PreviousCar()
    {
        // ������� � ���������� ������
        int newIndex = (currentCarIndex - 1 + cars.Length) % cars.Length; // ����������� �������
        ShowCar(newIndex);
        PlayerPrefs.SetInt("Vehicle", newIndex);
    }

}
