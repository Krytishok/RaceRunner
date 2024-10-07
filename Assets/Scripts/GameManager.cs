using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int NumberOfCoinsAtAll = 0;
    private int incrementCoinToAll;
    public void StopGame()
    {
        //Add collected coins in level to total
        NumberOfCoinsAtAll += FindFirstObjectByType<CoinManager>()._numberOfCoinsInLevel;

        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + NumberOfCoinsAtAll);


        SceneManager.LoadScene("MainMenu");
        Debug.Log("Stop");
    }

    
}
