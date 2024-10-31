using Assets.Platform.Scripts.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Platform.Scripts.UI
{
    public class WindowManager : MonoBehaviour
    {
        private Dictionary<Type, Window> windowDictionary = new Dictionary<Type, Window>();

        private void Start()
        {
            windowDictionary.Clear();

            foreach (Window item in Resources.FindObjectsOfTypeAll<Window>())
            {
                if (windowDictionary.ContainsKey(item.GetType()))
                {
                    print($"ToFix: Duplicate window {item.GetType().Name}");
                    continue;
                }
                item.Init();
                windowDictionary.Add(item.GetType(), item);
                item.Close();
            }
        }

        public void OpenWindow<T>() where T : Window
        {
            CloseWindows();

            T window = GetWindow<T>();

            if (!ReferenceEquals(window,null))
            {
                window.Show();
            }
        }

        public T GetWindow<T>() where T : Window
        {
            Type windowType = typeof(T);
            if (windowDictionary.ContainsKey(windowType))
            {
                return windowDictionary[windowType] as T;
            }
            else
            {
                print($"{windowType} : Doesn't Exist.");
                return null;
            }
        }

        private void CloseWindows()
        {
            foreach (var window in windowDictionary)
                window.Value.Close();
        }

    }
}