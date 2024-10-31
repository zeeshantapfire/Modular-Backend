using Assets.Platform.Scripts.Login;
using Newtonsoft.Json;
using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Assets.Platform.Scripts.PlayerDataModule
{
    [CreateAssetMenu(fileName = "UserProfile", menuName = "Board Game Platform/Create User Profile")]
    public class UserProfile : ScriptableObject
    {
        [JsonIgnore] public const string PLAYER_USERSDATA = "PlayerUserData";

        // Actual Data
        public string userName;
        public int avatarIndex;
        public string bio;
        public int wins;
        public int losses;
        public int userLevel;
        public int experiencePoints;

        #region EDITOR METHODS
        public void FillSampleData()
        {
            userName = "CoolGamerX";
            avatarIndex = 1;
            wins = 25;
            losses = 13;
            bio = "Gaming enthusiast and coffee lover ☕️🎮";
            userLevel = 25;
            experiencePoints = 4500;
        }

        public void Reset()
        {
            userName = string.Empty;
            avatarIndex = 0;
            bio = string.Empty;
            wins = 0;
            losses = 0;
            userLevel = 0;
            experiencePoints = 0;
        }

        #endregion

        public void FromJson(string data) => JsonConvert.PopulateObject(data, this);

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void UpdatePlayfab()
        {
            Dictionary<string, string> keysAndData = new Dictionary<string, string>();
            keysAndData.Add(PLAYER_USERSDATA, ToJson());

            UpdateUserDataRequest request = new UpdateUserDataRequest
            {
                Data = keysAndData
            };

            PlayFabClientAPI.UpdateUserData(request, OnSuccess, OnFail);

            void OnSuccess(UpdateUserDataResult result)
            {
                
            }

            void OnFail(PlayFabError error)
            {
                Debug.Log($"{GetType().Name} : Data Update Failed {error.Error}");
            }
        }

    }
}