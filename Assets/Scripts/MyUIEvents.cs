using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static GlobalExtension;

public class MyUIEvents : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnMouseDownEvent;
    [SerializeField]
    UnityEvent WhileMouseDownEvent;
    [SerializeField]
    UnityEvent OnMouseUpEvent;
    private bool isMouseDown = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (isMouseDown)
            WhileMouseDown();
    }

    private void OnMouseDown()
    {
        Log("OnMouseDown");
        isMouseDown = true;
        OnMouseDownEvent?.Invoke();
    }

    private void OnMouseUp()
    {
        Log("OnMouseUp");
        isMouseDown = false;
        OnMouseUpEvent?.Invoke();
    }

    void WhileMouseDown()
    {
        Log("WhileMouseDown");
        WhileMouseDownEvent?.Invoke();
    }
}
