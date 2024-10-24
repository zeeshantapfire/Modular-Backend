using Assets.Platform.Scripts.Login;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformManager : MonoBehaviour
{
    public LoginModule loginModule = null;

    private void Awake()
    {
        loginModule.Init();

        StartCoroutine(LoadDependencies());
    }

    IEnumerator LoadDependencies()
    {
        yield return new WaitUntil(()=> loginModule.IsInitialized());

        SceneManager.LoadScene(1);
    }


    public void InitLoginModule(bool state = true)
    {
        loginModule = state? new LoginModule() : null;
    }
}
