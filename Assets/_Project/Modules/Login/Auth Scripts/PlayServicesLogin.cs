using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

namespace Assets.Platform.Scripts.Login
{
    public class PlayServicesLogin
    {
        public static event Action OnSuccess;
        public static event Action<PlayFabError> OnFail;

        public void Authenticate()
        {
            LoginWithGooglePlayGamesServicesRequest request = new LoginWithGooglePlayGamesServicesRequest
            {
                CreateAccount = false,
                ServerAuthCode = LoginModule.LocalAccessToken
            };

            PlayFabClientAPI.LoginWithGooglePlayGamesServices(request, LoginSuccess, LoginFail);

            void LoginSuccess(LoginResult result)
            {
                LoginModule.LocalPlayfabId = result.PlayFabId;
                LoginModule.LoginType = LoginTypeEnum.PlayServices;
             
                Debug.Log($"{GetType()} : Login Complete");

                OnSuccess?.Invoke();
            }

            void LoginFail(PlayFabError error)
            {
                Debug.Log($"{GetType().Name} : Login Failed {error.Error}");
                OnFail?.Invoke(error);
            }
        }

    }
}
