using Assets.Platform.Scripts.Login;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlreadyLinkWindow : MonoBehaviour
{
    private LoginModule m_LoginModule;

    private void Start()
    {
        m_LoginModule = PlatformManager.Instance.loginModule;
    }

    public void LinkWithCurrent()
    {
        m_LoginModule.FacebookAuthLink.Login(true);
    }

    public void Login()
    {
        m_LoginModule.FacebookAuth.Login();
    }

}
