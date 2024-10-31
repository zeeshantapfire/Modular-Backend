using Assets.Platform.Scripts.Login;
using Assets.Platform.Scripts.PlayerDataModule;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlatformManager), true)]
public class PlatformMangerEditor : Editor
{
    private Editor userProfileEditor;

    private GUIStyle DeActivateBtn
    {
        get
        {
            GUIStyle style = new GUIStyle(GUI.skin.button)
            {
                fixedHeight = 25,
                fontSize = 13,
                fontStyle = FontStyle.Normal,
                normal = { textColor = new Color(242f / 255f, 155f/255f, 155f/255f), },
                hover = { textColor = new Color(242f / 255f, 155f / 255f, 155f / 255f), }
            };

            return style;
        }
    }

    private GUIStyle ActiveBtn
    {
        get
        {
            GUIStyle style = new GUIStyle(GUI.skin.button)
            {
                fixedHeight = 25,
                fontSize = 13,
                fontStyle = FontStyle.Normal,
                normal = { textColor = new Color(63f / 255f, 173f / 255f, 78f / 255f) },
                hover = { textColor = new Color(63f / 255f, 173f / 255f, 78f / 255f) }
            };

            return style;
        }
    }

    public override void OnInspectorGUI()
    {
        PlatformManager platformManager = (PlatformManager)target;

        LoginModuleEditor(platformManager); // Login Module

        PlayerDataModuleEditor(platformManager); // PlayerData Module

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
            EditorUtility.SetDirty(platformManager);
    }

    private void PlayerDataModuleEditor(PlatformManager platformManager)
    {
        if (platformManager.isPlayerModuleActive)
        {
            DrawTitle("Player Data Module", Color.white);

            platformManager.playerDataModule.userProfile = (UserProfile)EditorGUILayout.ObjectField("User Data:", platformManager.playerDataModule.userProfile, typeof(UserProfile),false);
            if (platformManager.playerDataModule.userProfile != null) 
            {
                if (userProfileEditor == null)
                {
                    userProfileEditor = CreateEditor(platformManager.playerDataModule.userProfile);
                }

                userProfileEditor.OnInspectorGUI();
            }

            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.Space(10);


            if (GUILayout.Button("Remove Player Data", DeActivateBtn))
            {
                platformManager.TogglePlayerDataModule(false);
            }
        }
        else
        {
            if (GUILayout.Button("Add Player Data", ActiveBtn))
            {
                platformManager.TogglePlayerDataModule(true);
            }
        }
    }

    private void LoginModuleEditor(PlatformManager platformManager)
    {
        if (platformManager.isLoginModuleActive)
        {
            // Draw Login Module
            DrawTitle("Login Module", Color.white);

            EditorGUILayout.LabelField("Login Type", LoginModule.LoginType.ToString());
            EditorGUILayout.LabelField("Guest Key", LoginModule.GuestKey);
            EditorGUILayout.LabelField("Playfab ID", LoginModule.LocalPlayfabId);
            EditorGUILayout.LabelField("Access Token", LoginModule.LocalAccessToken);

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            platformManager.loginModule.activeFacebook = EditorGUILayout.Toggle("Facebook", platformManager.loginModule.activeFacebook);
            platformManager.loginModule.activatePlayService = EditorGUILayout.Toggle("Google", platformManager.loginModule.activatePlayService);
            platformManager.loginModule.activeGameCenter = EditorGUILayout.Toggle("Game Center", platformManager.loginModule.activeGameCenter);

            EditorGUILayout.Space(10);

            if (GUILayout.Button("Remove Login Module", DeActivateBtn))
            {
                platformManager.ToggleLoginModule(false);
            }
        }
        else
        {
            if (GUILayout.Button("Add Login Module", ActiveBtn))
            {
                platformManager.ToggleLoginModule(true);
            }
        }
    }

    private void DrawTitle(string title, Color color)
    {
        GUIStyle style = new GUIStyle(GUI.skin.label)
        {
            fontSize = 20,
            fontStyle = FontStyle.Bold,
            normal = { textColor = color },
            alignment = TextAnchor.MiddleLeft,
            fixedHeight = 40
        };

        EditorGUILayout.LabelField(title, style);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
    }

}
