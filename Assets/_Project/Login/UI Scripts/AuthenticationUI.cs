using Assets.Platform.Scripts.Login;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthenticationUI : MonoBehaviour
{
    public AlreadyLinkWindow alreadyLinkWindow;

    private LoginModule m_LoginModule;


    private void Awake()
    {
        m_LoginModule = PlatformManager.Instance.loginModule;
    }


    private void OnEnable()
    {
        FacebookAuthLink.OnSuccess += FacebookLinkSuccess;
        FacebookAuthLink.OnAlreadyLinked += FacebookAlreadyExit;
        FacebookAuthLink.OnFail += FacebookLinkFail;
    }

    #region FACEBOOK CALLBACKS

    private void FacebookLinkSuccess()
    {
        Debug.Log(LoginModule.LoginType);
        Debug.Log($"{GetType()} : Facebook Linking Sucessful");
    }

    private void FacebookAlreadyExit()
    {
        Debug.Log($"{GetType()} : Facebook Linking Already Linked");
        alreadyLinkWindow.gameObject.SetActive(true);
    }

    private void FacebookLinkFail(PlayFabError error)
    {
        Debug.Log($"{GetType()} : Facebook Linking Failed {error.Error}");
    }

    #endregion

    private void OnDisable()
    {
        FacebookAuthLink.OnSuccess -= FacebookLinkSuccess;
        FacebookAuthLink.OnAlreadyLinked -= FacebookAlreadyExit;
        FacebookAuthLink.OnFail -= FacebookLinkFail;
    }

}
