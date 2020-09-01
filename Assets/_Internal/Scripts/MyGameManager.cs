using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalExtension;

public class MyGameManager: MonoBehaviour
{
    [SerializeField]
    private DeviceOrientation _currentDeviceOrientation;
    public DeviceOrientation CurrentDeviceOrientation
    {
        get { return _currentDeviceOrientation; }
        private set
        {
            if (value != _currentDeviceOrientation)
            {
                _currentDeviceOrientation = value;
                DeviceOrientationChanged?.Invoke(this, new EventArgs());
            }
        }
    }
    public event EventHandler DeviceOrientationChanged;

    void Start()
    {
        //Определение ориентации экрана
        if (Screen.width > Screen.height)
            CurrentDeviceOrientation = DeviceOrientation.LandscapeLeft;
        else
            CurrentDeviceOrientation = DeviceOrientation.Portrait;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Input.deviceOrientation != DeviceOrientation.Unknown)
            CurrentDeviceOrientation = Input.deviceOrientation;
    }


}
