using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Platform.Scripts.UI
{
    public abstract class Window : MonoBehaviour
    {
        public virtual void Show()
        {
            gameObject.SetActive(false);
        }

        public virtual void Init()
        {

        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }
    }
}