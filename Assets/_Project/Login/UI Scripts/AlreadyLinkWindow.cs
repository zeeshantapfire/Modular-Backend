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

    LoginTypeEnum loginType;

    public void Init(LoginTypeEnum loginType)
    {
        this.loginType = loginType;
    }

    public void LinkWithCurrent()
    {
        if (loginType == LoginTypeEnum.Facebook)
        {
            m_LoginModule.FacebookLinking.Authenticate(true);
        }

        if (loginType == LoginTypeEnum.PlayServices)
        {
            m_LoginModule.PlayServiceLinking.Authenticate(true);
        }

        if (loginType == LoginTypeEnum.GameCenter)
        {
            m_LoginModule.GameCenterLinking.Authenticate(true);
        }
    }

    public void Login()
    {
        if (loginType == LoginTypeEnum.Facebook)
        {
            m_LoginModule.FacebookLogin.Authenticate();
        }

        if (loginType == LoginTypeEnum.PlayServices)
        {
            m_LoginModule.PlayServiceLogin.Authenticate();
        }

        if (loginType == LoginTypeEnum.GameCenter)
        {
            m_LoginModule.GameCenterLogin.Authenticate();
        }
    }

}
