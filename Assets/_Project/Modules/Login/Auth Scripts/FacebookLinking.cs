using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using Facebook.Unity;

namespace Assets.Platform.Scripts.Login
{
    // Perform Silent/Guest Login
    // Login method to perform Guest Login
    // OnSuccess and OnFail Methods to Respond
    public class FacebookLinking
    {
        public static event Action OnSuccess;
        public static event Action OnAlreadyLinked;
        public static event Action<PlayFabError> OnFail;

        public void Authenticate(bool forceLink = false)
        {
            if (!forceLink)
            {
                FB.LogInWithReadPermissions(new List<string>() { "public_profile" }, (ILoginResult result) =>
                {
                    if (!string.IsNullOrEmpty(result.Error))
                    {
                        Debug.LogError("Facebook Login Failed: " + result.Error);
                        return;
                    }
                    LoginModule.LocalAccessToken = result.AccessToken.TokenString;
                    AccountLinking(false);
                });
            }
            else
            {
                AccountLinking(true);
            }
        }

        private void AccountLinking(bool forceLink)
        {
            PlayFabClientAPI.LinkFacebookAccount(new LinkFacebookAccountRequest
            {
                AccessToken = LoginModule.LocalAccessToken,
                ForceLink = forceLink
            }, 

            (LinkFacebookAccountResult result) => {

                LoginModule.LoginType = LoginTypeEnum.Facebook;
                Debug.Log(LoginModule.LoginType);
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
