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

namespace Assets.Platform.Scripts.Login
{
    // Perform Silent/Guest Login
    // Login method to perform Guest Login
    // OnSuccess and OnFail Methods to Respond
    public class GuestAuth
    {
        public static event Action OnSuccess;
        public static event Action<PlayFabError> OnFail;

        /// <summary>
        /// Perform the Login Operation
        /// </summary>
        public void Login()
        {
            Debug.Log($"Guest Key : {LoginModule.GuestKey}");
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

            Debug.Log($"{GetType().Name} : Login Complete");
            OnSuccess?.Invoke();
        }
    }
}
