using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Facebook.Unity;
using System;

namespace Assets.Platform.Scripts.Login
{
    public enum LoginTypeEnum { Guest, Facebook, Google, GameCenter }

    [Serializable]
    public class LoginModule
    {
        public bool guest, facebook, google, gameCenter;

        public const string LOGIN_TYPE_KEY = "LoginTypeKey";
        public static LoginTypeEnum LoginType
        {
            get
            {
                return (LoginTypeEnum)PlayerPrefs.GetInt(LOGIN_TYPE_KEY);
            }

            set
            {
                PlayerPrefs.GetInt(LOGIN_TYPE_KEY, (int)value);
            }
        }

        public LoginModule()
        {
            guest = true;
        }

        public void Init()
        {
            if (guest)
            {
                // perform Guest operation
            }

            if (facebook)
                FB.Init();

        }

        public bool IsInitialized()
        {
            bool state = true;

            if (facebook)
            {
                state &= FB.IsInitialized;
            }

            return state;
        }

    }
}
