using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

namespace Assets.Platform.Scripts.Login
{
    // Perform Silent/Guest Login
    // Login method to perform Guest Login
    // OnSuccess and OnFail Methods to Respond
    public class FacebookLogin
    {
        public static event Action OnSuccess;
        public static event Action<PlayFabError> OnFail;

        public void Authenticate()
        {
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
