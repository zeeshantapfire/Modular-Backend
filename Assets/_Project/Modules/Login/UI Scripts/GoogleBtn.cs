using Assets.Platform.Scripts.Login;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoogleBtn : MonoBehaviour
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
        bool deviceCheck = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor;
        gameObject.SetActive(m_LoginModule.activatePlayService && deviceCheck);
    }

    private void OnEnable()
    {
        m_Button.onClick.AddListener(() => m_LoginModule.PlayServiceLinking.Authenticate());
    }

    private void OnDisable()
    {
        m_Button.onClick.RemoveListener(() => m_LoginModule.PlayServiceLinking.Authenticate());
    }
}
