using Assets.Platform.Scripts.Login;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Platform.Scripts.UI
{
    public class AlreadyLinkWindow : Window
    {
        private Login.LoginModule m_LoginModule;
        private LoginTypeEnum m_LoginType;

        public override void Show()
        {
            base.Show();

            m_LoginModule = PlatformManager.Instance.loginModule;
        }

        public void Init(LoginTypeEnum loginType)
        {
            m_LoginType = loginType;
        }

        public void LinkWithCurrent()
        {
            if (m_LoginType == LoginTypeEnum.Facebook)
            {
                m_LoginModule.FacebookLinking.Authenticate(true);
            }

            if (m_LoginType == LoginTypeEnum.PlayServices)
            {
                m_LoginModule.PlayServiceLinking.Authenticate(true);
            }

            if (m_LoginType == LoginTypeEnum.GameCenter)
            {
                m_LoginModule.GameCenterLinking.Authenticate(true);
            }
        }

        public void Login()
        {
            if (m_LoginType == LoginTypeEnum.Facebook)
            {
                m_LoginModule.FacebookLogin.Authenticate();
            }

            if (m_LoginType == LoginTypeEnum.PlayServices)
            {
                m_LoginModule.PlayServiceLogin.Authenticate();
            }

            if (m_LoginType == LoginTypeEnum.GameCenter)
            {
                m_LoginModule.GameCenterLogin.Authenticate();
            }
        }

    }
}