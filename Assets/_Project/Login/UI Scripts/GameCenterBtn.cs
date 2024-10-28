using Assets.Platform.Scripts.Login;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCenterBtn : MonoBehaviour
{
    private Button m_Button;
    private LoginModule m_LoginModule;

    private void Awake()
    {
        m_Button = GetComponent<Button>();
        m_LoginModule = PlatformManager.Instance.loginModule;
    }

    private void Start()
    {
        bool deviceCheck = Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.WindowsEditor;
        gameObject.SetActive(m_LoginModule.activeGameCenter && deviceCheck);
    }

    private void OnEnable()
    {
        m_Button.onClick.AddListener(() => m_LoginModule.FacebookLinking.Authenticate());
    }

    private void OnDisable()
    {
        m_Button.onClick.RemoveListener(() => m_LoginModule.FacebookLinking.Authenticate());
    }
}
