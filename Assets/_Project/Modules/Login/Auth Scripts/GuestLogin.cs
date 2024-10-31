using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

namespace Assets.Platform.Scripts.Login
{
    // Perform Silent/Guest Login
    // Login method to perform Guest Login
    // OnSuccess and OnFail Methods to Respond
    public class GuestLogin
    {
        public static event Action OnSuccess;
        public static event Action<PlayFabError> OnFail;

        /// <summary>
        /// Perform the Login Operation
        /// </summary>
        public void Authenticate()
        {
            LoginWithCustomIDRequest request = new LoginWithCustomIDRequest
            {
                CustomId = LoginModule.GuestKey,
                CreateAccount = true,
                TitleId = PlayFabSettings.TitleId
            };

            PlayFabClientAPI.LoginWithCustomID(request, LoginSuccess, (PlayFabError error) => { OnFail?.Invoke(error); });
        }

        private void LoginSuccess(LoginResult result)
        {
            LoginModule.LocalPlayfabId = result.PlayFabId;
            LoginModule.LoginType = LoginTypeEnum.Guest;

            OnSuccess?.Invoke();
        }
    }
}
