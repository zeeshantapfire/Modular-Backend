using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace Assets.Platform.Scripts.Login
{
    public enum LoginTypeEnum { Silent, Facebook, Google, Email, GameCenter }

    public class LoginSystem : MonoBehaviour
    {
        public bool createAccount = true;

        public string SessionTicket { get; protected set; }
        public string PlayFabId { get; protected set; }   

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

        public virtual void PerformeSetup()
        {
            
        }
    }
}
