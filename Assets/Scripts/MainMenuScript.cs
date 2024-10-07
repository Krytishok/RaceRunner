using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _NumberOfCoinsText;
    private int _NumberOfCoins = 0;



    private void Start()
    {
        Time.timeScale = 1.0f;
        _NumberOfCoinsText.text = PlayerPrefs.GetInt("Coins").ToString();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
