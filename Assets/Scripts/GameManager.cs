using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void StopGame()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Stop");
    }
}
