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
    public class GuestLogin : LoginSystem
    {
        private const string SILENT_PREF_KEY = "SilentPlayerKey";
        private string SilentPlayerKey
        {
            get
            {
                string key = PlayerPrefs.GetString(SILENT_PREF_KEY, string.Empty);
                if (string.IsNullOrEmpty(key))
                {
                    key = System.Guid.NewGuid().ToString();
                }
                return key;
            }
        }

        #region EVENT CALLBACKS

        public static event Action OnSuccessEvent;
        public static event Action<PlayFabError> OnFailEvent;

        #endregion

        /// <summary>
        /// Perform the Login Operation
        /// </summary>
        public void Perform(GameObject obj)
        {
            print($"{GetType()} : SilentKey {SilentPlayerKey}");

            LoginWithCustomIDRequest request = new LoginWithCustomIDRequest
            {
                CustomId = SilentPlayerKey,
                CreateAccount = createAccount,
                TitleId = PlayFabSettings.TitleId
            };

            PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnFail);
        }

        private void OnSuccess(LoginResult result)
        {
            this.SessionTicket = result.SessionTicket;
            this.PlayFabId = result.PlayFabId;

            LoginModule.LoginType = LoginTypeEnum.Guest;

            print($"{GetType()} : Login Successful");
            OnSuccessEvent?.Invoke();
        }

        private void OnFail(PlayFabError error)
        {
            print($"{GetType()} : Login Failed");
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
