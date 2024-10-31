using Assets.Platform.Scripts.Login;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Platform.Scripts.PlayerDataModule
{
    [Serializable]
    public class PlayerDataModule
    {
        public UserProfile userProfile;
        private PlatformManager m_PlatformManager;
    
        public void OnEnable()
        {
            FacebookLogin.OnSuccess += LoginCompleted;
            PlayServicesLogin.OnSuccess += LoginCompleted;
            GameCenterLogin.OnSuccess += LoginCompleted;
        }

        public void Init(PlatformManager pManager) => m_PlatformManager = pManager;

        private void LoginCompleted()
        {
            List<string> dataKeys = new List<string>();

            if (userProfile != null)
                dataKeys.Add(UserProfile.PLAYER_USERSDATA);

            GetUserDataRequest request = new GetUserDataRequest
            {
                PlayFabId =  LoginModule.LocalPlayfabId,
                Keys = dataKeys
            };

            PlayFabClientAPI.GetUserData(request, OnSuccess, OnFail);

            void OnSuccess(GetUserDataResult result)
            {
                if (dataKeys.Contains(UserProfile.PLAYER_USERSDATA))
                {
                    userProfile.FromJson(result.Data[UserProfile.PLAYER_USERSDATA].Value);
                }
            }

            void OnFail(PlayFabError error)
            {
                Debug.Log($"{GetType().Name} : Data Restoration Failed {error.Error}");
            }

        }

        public void OnDisable()
        {
            FacebookLogin.OnSuccess -= LoginCompleted;
            PlayServicesLogin.OnSuccess -= LoginCompleted;
            GameCenterLogin.OnSuccess -= LoginCompleted;
        }
    }
}
