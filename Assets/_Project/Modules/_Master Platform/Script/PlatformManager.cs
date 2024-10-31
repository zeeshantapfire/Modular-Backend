using Assets.Platform.Scripts.Login;
using Assets.Platform.Scripts.PlayerDataModule;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformManager : MonoBehaviour
{
    // Login Module
    public bool isLoginModuleActive = false;
    public LoginModule loginModule = null;

    // PlayerData Module
    public bool isPlayerModuleActive = false;
    public PlayerDataModule playerDataModule = null;

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

        if (isLoginModuleActive)
        {
            loginModule.Init();
        }
    }

    private void OnEnable()
    {
        loginModule?.OnEnable();
        playerDataModule?.OnEnable();
    }

    private void Start()
    {
        StartCoroutine(LoadDependencies());
    }

    public void ToggleLoginModule(bool state = true)
    {
        loginModule = new LoginModule();
        isLoginModuleActive = state;
    }

    public void TogglePlayerDataModule(bool state = true)
    {
        playerDataModule = new PlayerDataModule();
        isPlayerModuleActive = state;
    }

    IEnumerator LoadDependencies()
    {
        if (isLoginModuleActive)
            yield return new WaitUntil(() => loginModule.IsExecutionCompleted());


        SceneManager.LoadScene(1);
        yield return null;
    }

    private void OnDisable()
    {
        loginModule?.OnDisable();
        playerDataModule?.OnDisable();
    }

}
