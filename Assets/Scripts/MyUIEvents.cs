using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GlobalExtension;

public class MyUIEvents : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    protected enum UpdateModes { InUpdate, InFixedUpdate }
    [Tooltip("В каком режыме выполнять")]
    [SerializeField]
    UpdateModes updateMode;
    //Расширенные события мыши
    [SerializeField]
    UnityEvent OnMouseDownEvent;
    [SerializeField]
    UnityEvent WhileMouseDownEvent;
    [SerializeField]
    UnityEvent OnMouseUpEvent;
    private bool isMouseDown = false;
    void Update()
    {
        if (updateMode == UpdateModes.InUpdate)
            Process();
    }

    private void FixedUpdate()
    {
        if (updateMode == UpdateModes.InFixedUpdate)
            Process();
    }

    void Process()
    {
        if (isMouseDown)
        {
            WhileMouseDown();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isMouseDown = true;
        OnMouseDownEvent?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isMouseDown = false;
        OnMouseUpEvent?.Invoke();
    }

    void WhileMouseDown()
    {
        WhileMouseDownEvent?.Invoke();
    }
}
