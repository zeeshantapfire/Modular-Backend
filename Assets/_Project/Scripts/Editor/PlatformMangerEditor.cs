using Assets.Platform.Scripts.Login;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlatformManager), true)]
public class PlatformMangerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlatformManager platformManager = (PlatformManager)target;

        #region LOGIN MODULE

        if (platformManager.isLoginSystemActive)
        {
            // Draw Login Module
            DrawTitle("Login Module", Color.white);

            EditorGUILayout.LabelField("Guest Key", LoginModule.GuestKey);
            EditorGUILayout.LabelField("Playfab ID", LoginModule.LocalPlayfabId);
            EditorGUILayout.LabelField("Access Token", LoginModule.LocalAccessToken);

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            platformManager.loginModule.activeFacebook = EditorGUILayout.Toggle("Facebook", platformManager.loginModule.activeFacebook);
            platformManager.loginModule.activeGoogle = EditorGUILayout.Toggle("Google", platformManager.loginModule.activeGoogle);
            platformManager.loginModule.activeGameCenter = EditorGUILayout.Toggle("Game Center", platformManager.loginModule.activeGameCenter);

            EditorGUILayout.Space(10);

            if (GUILayout.Button("Remove Login Module"))
            {
                platformManager.ToggleLoginModule(false);
            }
        }
        else
        {
            if (GUILayout.Button("Add Login Module"))
            {
                platformManager.ToggleLoginModule(true);
            }
        }

        #endregion

        if (GUI.changed)
            EditorUtility.SetDirty(platformManager);
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

        EditorGUILayout.Space(10); 
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
    }

}
