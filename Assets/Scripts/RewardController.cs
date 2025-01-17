using UnityEngine;
using YG;

public class RewardController : MonoBehaviour
{

    [SerializeField] GameObject _rewardImage;
    [SerializeField] GameObject _rewardButton;

    private bool _rewardIsShow;
    public void ShowRewardAd()
    {
        _rewardImage.SetActive(true);
        _rewardButton.SetActive(false);
        YandexGame.RewVideoShow(0);
        _rewardImage.SetActive(false);
        _rewardIsShow = true;
    }

    public void GetReward()
    {
        DataManager.Instance._numberOfCoins += 500;
        PlayerPrefs.SetInt("Coins", DataManager.Instance._numberOfCoins);
        FindFirstObjectByType<MainMenuScript>().UpdateVisualCoins();
    }

    public void SetActiveRewardButton(bool active)
    {
        if (!_rewardIsShow)
        {
            _rewardButton.SetActive(active);
        }
        else
        {
            _rewardButton.SetActive(false);
        }
    }

    private void Start()
    {
        _rewardIsShow = false;
        _rewardButton.SetActive(true);
    }


}
