using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logger : MonoBehaviour
{
    [SerializeField]
    private Text text;
    void Start()
    {
        if (text == null)
        {
            Debug.LogError("В компоненте Logger не связан компонент Text для вывода логов");
            Destroy(this);
        }
    }

    public void WriteLine(string message)
    {
        text.text = message + Environment.NewLine + text.text;
    }

    public void Clear()
    {
        text.text = "";
    }
}
