using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] GameObject ButtonBackToMenu;
    [SerializeField] GameObject PauseBar;

    private bool _isPause = false;

    private void Start()
    {
        
    }

    public void SpawnButtonBackToMenu()
    {
        ButtonBackToMenu.SetActive(true);
    }
    public void PauseButtonLogic()
    {
        if (!_isPause)
        {
            Time.timeScale = 0f;
            _isPause = true;
            PauseBar.SetActive(true);
        }
        else
        {
            PauseBar.SetActive(false);
            StartCoroutine(ResumeWithDelay(0.5f)); // Задержка в 0.5 секунд
        }
    }

    private IEnumerator ResumeWithDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Ждём реальное время
        Time.timeScale = 1f;
        _isPause = false;
    }
}
