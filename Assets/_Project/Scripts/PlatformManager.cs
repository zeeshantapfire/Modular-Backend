using Assets.Platform.Scripts.Login;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformManager : MonoBehaviour
{
    public bool isLoginSystemActive;
    public LoginModule loginModule = null;

    private static PlatformManager m_PlatformManager;
    public static PlatformManager Instance
    {
        get
        {
            return m_PlatformManager;
        }
        private set
        {
            m_PlatformManager = value;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (isLoginSystemActive)
        {
            loginModule.Init();
        }
    }

    private void Start()
    {
        StartCoroutine(LoadDependencies());
    }

    public void ToggleLoginModule(bool state = true)
    {
        loginModule = new LoginModule();
        isLoginSystemActive = state;
    }


    IEnumerator LoadDependencies()
    {
        if (isLoginSystemActive)
            yield return new WaitUntil(() => loginModule.IsExecutionCompleted());


        SceneManager.LoadScene(1);
        yield return null;
    }

}
