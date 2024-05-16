using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class FacebookManager : MonoBehaviour
{
    [SerializeField] PlayerStatistics m_playerStats;
    [SerializeField] GameObject m_collectionScreen;
    [SerializeField] private TextMeshProUGUI m_collectionText;


    void Awake()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    public void FBLogin()
    {
        var permissions = new List<string>() { "public_profile" };

        FB.LogInWithReadPermissions(permissions, AuthCallResult);
    }

    private void AuthCallResult(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            var aToken = AccessToken.CurrentAccessToken;

            Debug.Log(aToken.UserId);

            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }

            RewardClaimed();
        }
        else
        {
            Debug.Log("User cancelled login");
        }

    }

    public async Task SignInWithFacebookAsync(string accessToken)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithFacebookAsync(accessToken);
            Debug.Log("SignIn is successful.");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

    public void RewardClaimed()
    {
        m_collectionScreen.SetActive(true);

        m_playerStats.m_money += 10000;

        m_collectionText.text = "You've connected to Facebook! Heres 10000 money!";
    }
}
