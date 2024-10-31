using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Assets.Platform.Scripts.PlayerDataModule
{
    [CustomEditor(typeof(UserProfile))]
    public class UserProfileEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            UserProfile profile = target as UserProfile;

            EditorGUILayout.Space(10);
            if (GUILayout.Button("Fill Data"))
            {
                profile.FillSampleData();
            }
            if (GUILayout.Button("Reset Data"))
            {
                profile.Reset();
            }
        }

    }
}
