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
    public class FacebookLogin : LoginSystem
    {
        #region EVENT CALLBACKS

        public static event Action OnSuccessEvent;
        public static event Action<PlayFabError> OnFailEvent;

        #endregion

        private const string ACCESSTOKEN_PREF_KEY = "AccessToken";
        public string AccessToken
        {
            get
            {
                return PlayerPrefs.GetString(ACCESSTOKEN_PREF_KEY, string.Empty);
            }
            private set
            {
                PlayerPrefs.SetString(ACCESSTOKEN_PREF_KEY, value);
            }
        }

        public void Perform(GameObject obj)
        {
            LoginWithFacebookRequest request = new LoginWithFacebookRequest
            {
                AccessToken = AccessToken,
                AuthenticationToken = "",
                CreateAccount = createAccount,
                TitleId = PlayFabSettings.TitleId,
            };

            PlayFabClientAPI.LoginWithFacebook(request, OnSuccess, OnFail);
        }

        private void OnSuccess(LoginResult result)
        {
            this.SessionTicket = result.SessionTicket;
            this.PlayFabId = result.PlayFabId;

            print($"{GetType()} : Successful");
            OnSuccessEvent?.Invoke();
        }

        private void OnFail(PlayFabError error)
        {
            print($"{GetType()} : Failed");
            OnFailEvent?.Invoke(error);
        }

        #region TESTING AND DEBUG

        public override void PerformeSetup()
        {
            Button btn = GetComponent<Button>();

            int count = btn.onClick.GetPersistentEventCount();
            for (int i = count - 1; i >= 0; i--)
                UnityEventTools.RemovePersistentListener(btn.onClick, i);

            UnityAction<GameObject> action = Perform;
            UnityEventTools.AddObjectPersistentListener(btn.onClick, Perform, gameObject);
        }

        #endregion
    }
}
