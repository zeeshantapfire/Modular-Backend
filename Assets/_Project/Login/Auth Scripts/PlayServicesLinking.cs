using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

namespace Assets.Platform.Scripts.Login
{
    public class PlayServicesLinking
    {
        public static event Action OnSuccess;
        public static event Action OnAlreadyLinked;
        public static event Action<PlayFabError> OnFail;

        public void Authenticate(bool forceLink = false)
        {
            if (!forceLink)
            {
                PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
            }
            else
            {
                LinkAccountWithPlayfab(true);
            }

            void ProcessAuthentication(SignInStatus status)
            {
                if (status == SignInStatus.Success)
                {
                    PlayGamesPlatform.Instance.RequestServerSideAccess(false, (string token) =>
                    {
                        LoginModule.LocalAccessToken = token;
                        LinkAccountWithPlayfab(false);
                    });
                }
                else
                {
                    Debug.Log($"{GetType().Name} : Play Service Authentication Failed");
                }
            }
        }

        private void LinkAccountWithPlayfab(bool forceLinking)
        {
            if (string.IsNullOrEmpty(LoginModule.LocalAccessToken))
            {
                Debug.Log($"{GetType().Name} : Failed To fetch token.");
                return;
            }

            PlayFabClientAPI.LinkGooglePlayGamesServicesAccount(new LinkGooglePlayGamesServicesAccountRequest
            {
                ServerAuthCode = LoginModule.LocalAccessToken,
            }, 

            (LinkGooglePlayGamesServicesAccountResult result) => {

                LoginModule.LoginType = LoginTypeEnum.PlayServices;
                OnSuccess?.Invoke();
            }, 

            (PlayFabError error) => {

                if (error.Error == PlayFabErrorCode.AccountAlreadyLinked ||
                    error.Error == PlayFabErrorCode.LinkedAccountAlreadyClaimed)
                {
                    OnAlreadyLinked?.Invoke();
                }
                else
                {
                    OnFail?.Invoke(error);
                }
            });
        }

    }
}
