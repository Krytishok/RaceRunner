using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] GameObject ButtonBackToMenu;

    private void Start()
    {
        ButtonBackToMenu.SetActive(false);
    }

    public void SpawnButtonBackToMenu()
    {
        ButtonBackToMenu.SetActive(true);
    }
}
