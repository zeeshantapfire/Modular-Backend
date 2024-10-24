using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public LoginModule loginModule = null;

    public void InitLoginModule(bool state = true)
    {
        loginModule = state? new LoginModule() : null;
    }
}
