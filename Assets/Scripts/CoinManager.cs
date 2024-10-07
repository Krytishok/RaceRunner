using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [SerializeField] public int _numberOfCoinsInLevel;
    [SerializeField] TextMeshProUGUI _text;


   
    public void AddOne()
    {
        _numberOfCoinsInLevel++;
        _text.text = _numberOfCoinsInLevel.ToString();
    }
}
