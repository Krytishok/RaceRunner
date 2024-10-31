using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomizationScript : MonoBehaviour
{
    //Data for visual
    public int _id;
    public int _currentTuningId;
    public bool _isPurchased = false;
    public int _costForBuyCar;

    [SerializeField] GameObject[] _listOfBodies;
    [SerializeField] int[] _priceForBodies;

    [SerializeField] CarDataScript _carData;






    private void Start()
    {
    }
   

    private void LoadTypesOfTuningToScriptableObject()
    {
        
    }


}
