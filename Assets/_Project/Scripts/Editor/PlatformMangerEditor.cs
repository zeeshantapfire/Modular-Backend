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

        if (ReferenceEquals(platformManager.loginModule, null))
        {
            if (GUILayout.Button("Add Login Module"))
            {
                platformManager.InitLoginModule();
            }
        }
        else
        {
            // Draw Login Module
            DrawTitle("Login Module",Color.white);
            platformManager.loginModule.guest = EditorGUILayout.Toggle("Guest Login", platformManager.loginModule.guest);
            platformManager.loginModule.facebook = EditorGUILayout.Toggle("Facebook Login", platformManager.loginModule.facebook);
            platformManager.loginModule.google = EditorGUILayout.Toggle("Google Login", platformManager.loginModule.google);
            platformManager.loginModule.gameCenter = EditorGUILayout.Toggle("Game Center Login", platformManager.loginModule.gameCenter);

            if (GUILayout.Button("Remove Login Module"))
            {
                platformManager.InitLoginModule(false);
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
