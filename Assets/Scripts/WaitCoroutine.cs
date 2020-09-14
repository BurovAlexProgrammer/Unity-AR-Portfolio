using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitCoroutine : MonoBehaviour
{
    [SerializeField]
    UnityEvent actions;

    void Start()
    {
        ExecuteEvents();
    }


    public void ExecuteEvents()
    {
        var onEndOfFrame = OnEndOfFrame();
        StartCoroutine(onEndOfFrame);
    }
    
    private IEnumerator OnEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        actions?.Invoke();
    }
}
