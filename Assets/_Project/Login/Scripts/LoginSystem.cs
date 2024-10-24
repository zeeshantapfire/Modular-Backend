using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Platform.Scripts.Login
{
    public enum LoginTypeEnum { Silent, Facebook, Google, Email, GameCenter }

    public class LoginSystem : MonoBehaviour
    {
        private const string AUTH_PREF_KEY = "AuthKey";
        public string AuthKey
        {
            get
            {
                return PlayerPrefs.GetString(AUTH_PREF_KEY);
            }
            set
            {
                PlayerPrefs.SetString(AUTH_PREF_KEY, value);
            }
        }

        private const string LOGIN_PREF_KEY = "LoginType";
        public LoginTypeEnum LoginType
        {
            get
            {
                return (LoginTypeEnum)PlayerPrefs.GetInt(LOGIN_PREF_KEY);
            }
            set
            {
                PlayerPrefs.SetInt(LOGIN_PREF_KEY, (int)value);
            }
        }
    }
}
