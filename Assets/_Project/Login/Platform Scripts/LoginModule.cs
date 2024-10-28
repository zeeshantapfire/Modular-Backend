using UnityEngine;
using Facebook.Unity;
using System;
using GooglePlayGames;


namespace Assets.Platform.Scripts.Login
{
    public enum LoginTypeEnum { Guest = 0, Facebook, PlayServices, GameCenter }

    [Serializable]
    public class LoginModule
    {
        #region FIELD DECLERATION

        public bool activeFacebook;
        public bool activatePlayService;
        public bool activeGameCenter;

        public GuestLogin GuestLogin { get; set; }

        public FacebookLogin FacebookLogin { get; set; }
        public FacebookLinking FacebookLinking { get; set; }

        public PlayServicesLogin PlayServiceLogin { get; set; }
        public PlayServicesLinking PlayServiceLinking { get; set; }

        public GameCenterLogin GameCenterLogin { get; set; }
        public GameCenterLinking GameCenterLinking { get; set; }

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
            set
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
            PlayGamesPlatform.Activate();

            GuestLogin = new GuestLogin();

            if (activeFacebook)
            {
                FacebookLogin = new FacebookLogin();
                FacebookLinking = new FacebookLinking();
            }

            if (activatePlayService)
            {
                PlayServiceLogin = new PlayServicesLogin();
                PlayServiceLinking = new PlayServicesLinking();
            }

            if (activeGameCenter)
            {
                GameCenterLogin = new GameCenterLogin();
                GameCenterLinking = new GameCenterLinking();
            }

            PerformInitialLogin();
        }

        private void PerformInitialLogin()
        {
            switch (LoginType)
            {
                case LoginTypeEnum.Facebook:
                    FacebookLogin.Authenticate();
                    break;

                case LoginTypeEnum.PlayServices:
                    break;

                case LoginTypeEnum.GameCenter:
                    break;

                default:
                    GuestLogin.Login();
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
