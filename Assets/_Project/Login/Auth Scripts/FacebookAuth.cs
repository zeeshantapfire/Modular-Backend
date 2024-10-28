using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.UI;
using UnityEditor.Events;
using UnityEngine.Events;
using System.Net.NetworkInformation;
using Facebook.Unity;

namespace Assets.Platform.Scripts.Login
{
    // Perform Silent/Guest Login
    // Login method to perform Guest Login
    // OnSuccess and OnFail Methods to Respond
    public class FacebookAuth
    {
        public static event Action OnSuccess;
        public static event Action<PlayFabError> OnFail;

        private const string AUTH_PREF_KEY = "AuthKey";
        public string AuthToken
        {
            get
            {
                return PlayerPrefs.GetString(AUTH_PREF_KEY);
            }
            set
            {
                PlayerPrefs.SetString(AUTH_PREF_KEY, value);
            }
        }

        public void Login()
        {
            Debug.Log($"Facebook Access Token {LoginModule.LocalAccessToken}");
            LoginWithFacebookRequest request = new LoginWithFacebookRequest
            {
                AccessToken = LoginModule.LocalAccessToken,
                CreateAccount = false,
                TitleId = PlayFabSettings.TitleId,
            };

            PlayFabClientAPI.LoginWithFacebook(request, LoginSuccess, LoginFail);

            void LoginSuccess(PlayFab.ClientModels.LoginResult result)
            {
                LoginModule.LocalPlayfabId = result.PlayFabId;
                LoginModule.LoginType = LoginTypeEnum.Facebook;
             
                Debug.Log($"{GetType()} : Login Complete");

                OnSuccess?.Invoke();
            }

            void LoginFail(PlayFabError error)
            {
                Debug.Log($"{GetType()} : Login Failed {error.Error}");
                OnFail?.Invoke(error);
            }
        }

    }
}
