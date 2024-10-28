using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Facebook.Unity;
using System;
using UnityEngine.Analytics;

namespace Assets.Platform.Scripts.Login
{
    public enum LoginTypeEnum { Guest = 0, Facebook, Google, GameCenter }

    [Serializable]
    public class LoginModule
    {
        #region FIELD DECLERATION

        public bool activeFacebook;
        public bool activeGoogle;
        public bool activeGameCenter;

        public GuestAuth GuestAuth { get; set; }
        public FacebookAuth FacebookAuth { get; set; }
        public FacebookAuthLink FacebookAuthLink { get; set; }

        #endregion

        #region STATIC FIELDS

        public const string LOGIN_TYPE_KEY = "LoginTypeKey";
        public static LoginTypeEnum LoginType
        {
            get
            {
                return (LoginTypeEnum)PlayerPrefs.GetInt(LOGIN_TYPE_KEY);
            }

            set
            {
                PlayerPrefs.SetInt(LOGIN_TYPE_KEY, (int)value);
            }
        }

        public const string PLAYFABE_PREF_KEY = "PlayerPrefKey";
        public static string LocalPlayfabId
        {
            get
            {
                return PlayerPrefs.GetString(PLAYFABE_PREF_KEY);
            }
            set
            {
                PlayerPrefs.SetString(PLAYFABE_PREF_KEY, value);
            }
        }

        private const string ACCESSTOKEN_PREF_KEY = "AccessToken";
        public static string LocalAccessToken
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

        private const string SILENT_PREF_KEY = "SilentPlayerKey";
        public static string GuestKey
        {
            get
            {
                string key = PlayerPrefs.GetString(SILENT_PREF_KEY, string.Empty);
                if (string.IsNullOrEmpty(key))
                {
                    key = System.Guid.NewGuid().ToString();
                    PlayerPrefs.SetString(SILENT_PREF_KEY, key);
                }
                return key;
            }
        }

        #endregion

        public void Init()
        {
            FB.Init();

            GuestAuth = new GuestAuth();

            if (activeFacebook)
            {
                FacebookAuth = new FacebookAuth();
                FacebookAuthLink = new FacebookAuthLink();
            }

            
            Debug.Log($"{PlayerPrefs.GetInt(LOGIN_TYPE_KEY)}");
            Debug.Log($"{GetType().Name} : {LoginType} Mode");

            switch (LoginType)
            {
                case LoginTypeEnum.Facebook:
                    FacebookAuth.Login();
                    break;

                case LoginTypeEnum.Google:
                    break;

                case LoginTypeEnum.GameCenter:
                    break;

                default:
                    GuestAuth.Login();
                    break;
            }
        }

        public bool IsExecutionCompleted()
        {
            bool state = true;

            if (activeFacebook)
            {
                state &= FB.IsInitialized;
            }

            return state;
        }

    }
}
