using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using PlasticGui.WorkspaceWindow.PendingChanges;

namespace Assets.Platform.Scripts.Login
{
    [CustomEditor(typeof(LoginSystem), true)]
    public class LoginSystemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            LoginSystem loginSystem = (LoginSystem)target;

            if (GUILayout.Button("Perform Setup"))
            {
                loginSystem.PerformeSetup();
            }
        }
    }
}
