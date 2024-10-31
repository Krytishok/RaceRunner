using UnityEngine;

public class CarCollectionController : MonoBehaviour
{

    public GameObject[] cars; // Массив всех машин
    private int currentCarIndex = 0; // Индекс текущей машины

    private MonoBehaviour _mainMenuScript;


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
        // Отключаем текущую машину
        if (currentCarIndex >= 0 && currentCarIndex < cars.Length)
        {
            cars[currentCarIndex].SetActive(false);
        }

        // Обновляем индекс текущей машины
        currentCarIndex = index;

        // Включаем новую машину
        if (currentCarIndex >= 0 && currentCarIndex < cars.Length)
        {
            cars[currentCarIndex].SetActive(true);
        }
        //Сохраняем индекс первой показанной машинки
        DataManager.Instance._currentIndexOfCar = currentCarIndex;
        DataManager.Instance._currentCar = cars[currentCarIndex];
    }

    public void NextCar()
    {
        // Переход к следующей машине
        int newIndex = (currentCarIndex + 1) % cars.Length; // Циклический переход
        ShowCar(newIndex);
        //PlayerPrefs.SetInt("Vehicle", newIndex);
    }

    public void PreviousCar()
    {
        // Переход к предыдущей машине
        int newIndex = (currentCarIndex - 1 + cars.Length) % cars.Length; // Циклический переход
        ShowCar(newIndex);
        //PlayerPrefs.SetInt("Vehicle", newIndex);
    }
    public void BuyCar()
    {
        if(DataManager.Instance._numberOfCoins >= FindFirstObjectByType<CustomizationScript>()._costForBuyCar)
        {
            DataManager.Instance._numberOfCoins -= FindFirstObjectByType<CustomizationScript>()._costForBuyCar;
            FindFirstObjectByType<CustomizationScript>()._isPurchased = true;
            DataManager.Instance._vehicleId = currentCarIndex;


            FindFirstObjectByType<MainMenuScript>().HideOrSpawnBuyButton(false);
            FindFirstObjectByType<MainMenuScript>().HideOrSpawnBuyButton(false);
        }
    }


}
