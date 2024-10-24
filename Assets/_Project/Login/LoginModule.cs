using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Facebook.Unity;

public class LoginModule
{
    public bool guest, facebook, google, gameCenter;

    public LoginModule() 
    {
        guest = true;
    }

    public void Init()
    {
        if (guest)
        {
            // perform Guest operation
        }

        if (facebook)
        {
            FB.Init();
        }
    }

}
