using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.SocialPlatforms;

namespace Assets.Platform.Scripts.Login
{
    public class GameCenterLinking
    {
        public static event Action OnSuccess;
        public static event Action OnAlreadyLinked;
        public static event Action<PlayFabError> OnFail;

        public void Authenticate(bool forceLink = false)
        {
            if (!forceLink)
            {
                if (!Social.localUser.authenticated)
                {
                    Social.localUser.Authenticate(success =>
                    {
                        if (success)
                        {
                            Debug.Log($"{GetType().Name} : Login Successfull");
                            LoginModule.LocalAccessToken = Social.localUser.id;
                            AccountLinking(false);
                        }
                        else
                        {
                            Debug.Log($"{GetType().Name} : Login Failed");
                        }
                    });
                }
            }
            else
            {
                AccountLinking(true);
            }
        }

        private void AccountLinking(bool forceLink)
        {
            PlayFabClientAPI.LinkGameCenterAccount(new LinkGameCenterAccountRequest
            {
                GameCenterId = LoginModule.LocalAccessToken,
                ForceLink = forceLink
            }, 

            (LinkGameCenterAccountResult result) => {

                LoginModule.LoginType = LoginTypeEnum.GameCenter;
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
