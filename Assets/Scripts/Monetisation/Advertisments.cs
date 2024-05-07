using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Advertisments : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string m_androidGameID;
    [SerializeField] string m_iosGameID;
    [SerializeField] bool m_testMode = true;
    private string m_gameID;

    public bool m_initialised;
    RewardAds m_rewardAds;
    [SerializeField] GameManager m_gameManager;

    //a21b61b6-015f-48ec-b2ac-3cd6bd93c117

    private void Awake()
    {
        m_rewardAds = GetComponent<RewardAds>();

        InitialiseAds();
    }

    public void InitialiseAds()
    {
        #if UNITY_IOS

            m_gameID = m_iosGameID;

        #elif UNITY_ANDROID

            m_gameID = m_androidGameID;

        #elif UNITY_EDITOR

            m_gameID = m_androidGameID;

        #endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(m_gameID, m_testMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        m_initialised = true;
        m_rewardAds.LoadAd();
        StartCoroutine(m_gameManager.AdvertTimer(m_gameManager.m_adTimer));
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
