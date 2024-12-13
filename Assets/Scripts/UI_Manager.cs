
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using YG;


public class UI_Manager : MonoBehaviour
{
    [SerializeField] GameObject ButtonBackToMenu;
    [SerializeField] GameObject PauseBar;
    [SerializeField] GameObject PauseButton;
    [SerializeField] GameObject GameOverMenu;
    [SerializeField] UnityEngine.UI.Image _healthbar;
    [SerializeField] TextMeshProUGUI _distanceTraveledNumber;
    [SerializeField] TextMeshProUGUI _distanceTraveledMenuText;

    private bool _isPause = false;
    public float _health;

    private Transform _playerPosition;
    private float _distanceTraveled = 0;


    private void Start()
    {
        _playerPosition = FindFirstObjectByType<CarController>().transform;

        StartCoroutine(UpdateDistance(0.8f));
    }
    
    private IEnumerator UpdateDistance(float delay)
    {
        while (true)
        {
            _distanceTraveled = Mathf.Abs(MathF.Round(_playerPosition.position.z / 1000, 1));
            _distanceTraveledNumber.text = _distanceTraveled.ToString() + " КМ";

            Debug.Log("Distance Updated");

            yield return new WaitForSeconds(delay);
        }
    }
   


    public void SpawnButtonBackToMenu()
    {
        ButtonBackToMenu.SetActive(true);
    }
    public void GameOverUI()
    {
        PauseButton.SetActive(false);
        GameOverMenu.SetActive(true);
        StopAllCoroutines();
        _distanceTraveledMenuText.text = _distanceTraveledNumber.text;
        _distanceTraveledNumber.text = "";
        UpdateBestScore();

    }
    public void PauseButtonLogic()
    {
        if (!_isPause)
        {
            Time.timeScale = 0f;
            _isPause = true;
            PauseBar.SetActive(true);
            FindFirstObjectByType<AudioManagerController>().StopMusic();
            FindFirstObjectByType<CarAudioScript>().StopEngine();
        }
        else
        {
            PauseBar.SetActive(false);
            FindFirstObjectByType<AudioManagerController>().StartMusic();
            FindFirstObjectByType<CarAudioScript>().PlayEngine();
            StartCoroutine(ResumeWithDelay(0.5f)); // Задержка в 0.5 секунд
        }
    }

    private IEnumerator ResumeWithDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Ждём реальное время
        Time.timeScale = 1f;
        _isPause = false;
    }

    public void UpdateHealthBar(float currentHealth)
    {
        _healthbar.fillAmount = currentHealth/_health;
        Debug.Log("fillAmount " + (currentHealth / _health).ToString());
        Debug.Log("Current Health" + currentHealth.ToString() + " _health " + _health.ToString());
    }

    public void UpdateBestScore()
    {
        long distance = (long)Math.Round(_distanceTraveled*1000);
        Debug.Log(distance);
        if (PlayerPrefs.HasKey(("score")))
        {
            if (PlayerPrefs.GetFloat("score") < _distanceTraveled)
            {
                PlayerPrefs.SetFloat("score", _distanceTraveled);
                YandexGame.NewLeaderboardScores("score", distance);
            }
        }
        else
        {
            PlayerPrefs.SetFloat("score", _distanceTraveled);
            YandexGame.NewLeaderboardScores("score", distance);
        }
    }
}
