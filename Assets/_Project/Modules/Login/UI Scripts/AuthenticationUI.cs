using Assets.Platform.Scripts.Login;
using Assets.Platform.Scripts.PlayerDataModule;
using Assets.Platform.Scripts.UI;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthenticationUI : MonoBehaviour
{
    public UserProfile userProfile;
    public WindowManager windowManager;

    private LoginModule m_LoginModule;

    private void Awake()
    {
        m_LoginModule = PlatformManager.Instance.loginModule;
    }

    private void OnEnable()
    {
        FacebookLinking.OnSuccess += FacebookLinkSuccess;
        FacebookLinking.OnAlreadyLinked += FacebookAlreadyExit;
        FacebookLinking.OnFail += FacebookLinkFail;

        PlayServicesLinking.OnSuccess += PlayServiceLinkSuccess;
        PlayServicesLinking.OnAlreadyLinked += PlayServiceAccountAlreadyExit;
        PlayServicesLinking.OnFail += PlayServiceLinkFail;

        GameCenterLinking.OnSuccess += GameCenterLinkSuccess;
        GameCenterLinking.OnAlreadyLinked += GameCenterAccountAlreadyExit;
        GameCenterLinking.OnFail += GameCenterLinkFail;
    }

    #region FACEBOOK CALLBACKS

    private void FacebookLinkSuccess()
    {
        Debug.Log(LoginModule.LoginType);

        userProfile.UpdatePlayfab();

        Debug.Log($"{GetType()} : Facebook Linking Sucessful");
    }

    private void FacebookAlreadyExit()
    {
        Debug.Log($"{GetType()} : Facebook Linking Already Linked");
        windowManager.GetWindow<AlreadyLinkWindow>().Init(LoginTypeEnum.Facebook);
        windowManager.OpenWindow<AlreadyLinkWindow>();
    }

    private void FacebookLinkFail(PlayFabError error)
    {
        Debug.Log($"{GetType()} : Facebook Linking Failed {error.Error}");
    }

    #endregion

    #region PLAYSERVICES CALLBACKS

    private void PlayServiceLinkSuccess()
    {
        Debug.Log(LoginModule.LoginType);
        userProfile.UpdatePlayfab();
        Debug.Log($"{GetType()} : Facebook Linking Sucessful");
    }

    private void PlayServiceAccountAlreadyExit()
    {
        Debug.Log($"{GetType()} : Facebook Linking Already Linked");
        windowManager.GetWindow<AlreadyLinkWindow>().Init(LoginTypeEnum.PlayServices);
        windowManager.OpenWindow<AlreadyLinkWindow>();
    }

    private void PlayServiceLinkFail(PlayFabError error)
    {
        Debug.Log($"{GetType()} : Facebook Linking Failed {error.Error}");
    }

    #endregion

    #region GAMECENTER CALLBACKS

    private void GameCenterLinkSuccess()
    {
        Debug.Log(LoginModule.LoginType);
        userProfile.UpdatePlayfab();
        Debug.Log($"{GetType()} : Facebook Linking Sucessful");
    }

    private void GameCenterAccountAlreadyExit()
    {
        Debug.Log($"{GetType()} : Facebook Linking Already Linked");
        windowManager.GetWindow<AlreadyLinkWindow>().Init(LoginTypeEnum.PlayServices);
        windowManager.OpenWindow<AlreadyLinkWindow>();
    }

    private void GameCenterLinkFail(PlayFabError error)
    {
        Debug.Log($"{GetType()} : Facebook Linking Failed {error.Error}");
    }

    #endregion

    private void OnDisable()
    {
        FacebookLinking.OnSuccess -= FacebookLinkSuccess;
        FacebookLinking.OnAlreadyLinked -= FacebookAlreadyExit;
        FacebookLinking.OnFail -= FacebookLinkFail;

        PlayServicesLinking.OnSuccess -= PlayServiceLinkSuccess;
        PlayServicesLinking.OnAlreadyLinked -= PlayServiceAccountAlreadyExit;
        PlayServicesLinking.OnFail -= PlayServiceLinkFail;

        GameCenterLinking.OnSuccess -= GameCenterLinkSuccess;
        GameCenterLinking.OnAlreadyLinked -= GameCenterAccountAlreadyExit;
        GameCenterLinking.OnFail -= GameCenterLinkFail;
    }

}
