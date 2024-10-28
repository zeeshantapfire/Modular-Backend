using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

namespace Assets.Platform.Scripts.Login
{
    public class GameCenterLogin
    {
        public static event Action OnSuccess;
        public static event Action<PlayFabError> OnFail;

        public void Login()
        {
            LoginWithGameCenterRequest request = new LoginWithGameCenterRequest
            {
                PlayerId = LoginModule.LocalPlayfabId,
                CreateAccount = false,
                TitleId = PlayFabSettings.TitleId,
            };

            PlayFabClientAPI.LoginWithGameCenter(request, LoginSuccess, LoginFail);

            void LoginSuccess(LoginResult result)
            {
                LoginModule.LocalPlayfabId = result.PlayFabId;
                LoginModule.LoginType = LoginTypeEnum.GameCenter;
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
