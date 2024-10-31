using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarDataScript", menuName = "Scriptable Objects/CarDataScript")]
public class CarDataScript : ScriptableObject
{
    [SerializeField] string _nameOfCar;
    //If price = 0, => object was bought
    
    Dictionary<string, int> _bodies = new Dictionary<string, int>();



    public Dictionary<string, int> Bodies
    {
        get
        {
            return _bodies;
        }
    }


}
