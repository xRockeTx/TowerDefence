using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.Video;

public class Advertising : MonoBehaviour , IUnityAdsListener
{
    [SerializeField] private BuyOpen buyOpen;
    [SerializeField] private Button addMoney;

    private void Start()
    {   
        Advertisement.Initialize("4229895");
        Advertisement.AddListener(this);
    }
    public void AddMoney()
    {
        if (Advertisement.IsReady("Rewarded_Android"))
        {
            Time.timeScale = 0;
            Advertisement.Show("Rewarded_Android");
        }
    }
    public void ShowAds()
    {
        if (Advertisement.IsReady("Interstitial_Android"))
        {
            Time.timeScale = 0;
            Advertisement.Show("Interstitial_Android");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Ads are ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Time.timeScale = 1;
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Video started");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == "Rewarded_Android" && showResult == ShowResult.Finished)
        {
            Debug.Log("Rewarded_Android");
            buyOpen.ChangeMoney(40);
            addMoney.gameObject.SetActive(false);
        }
        Time.timeScale = 1;
    }
}
