using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalExtension;

public class AR_ImageTarget_Scene : MonoBehaviour
{
    private Logger logger;
    private MyGameManager gameManager;
    [SerializeField]
    private GameObject landscapeMenu;
    [SerializeField]
    private GameObject portraitMenu;
    [Tooltip("Car")]
    [SerializeField]
    private GameObject car;
    private Animator carAnim;


    private void Awake()
    {
        logger = GameObject.FindObjectOfType<Logger>();
        gameManager = GameObject.FindObjectOfType<MyGameManager>();
        gameManager.DeviceOrientationChanged += OnDeviceOrientationChanged;
        car = GameObject.Find("Tocus");
        carAnim = car.GetComponent<Animator>();
    }

    private void Start()
    {
        if (logger == null)
            throw new NullReferenceException();
        if (gameManager == null)
            throw new NullReferenceException();
        if (landscapeMenu == null)
            throw new NullReferenceException();
        if (portraitMenu == null)
            throw new NullReferenceException();
        if (car == null)
            throw new NullReferenceException();
        if (carAnim == null)
            throw new NullReferenceException();
    }

    private void Update()
    {
        
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Run()
    {
        //TODO запустить анимацию
        Log("Run!");
        carAnim.Play("Run");
    }


    void OnDeviceOrientationChanged(object sender, EventArgs e)
    {
        Log("New device orientation: " + gameManager.CurrentDeviceOrientation);

        //Адаптация меню (отключение одних и подключение других элементов)
        if (gameManager.CurrentDeviceOrientation == DeviceOrientation.LandscapeLeft |
            gameManager.CurrentDeviceOrientation == DeviceOrientation.LandscapeRight)
        {
            landscapeMenu.SetActive(true);
            portraitMenu.SetActive(false);
        }
        if (gameManager.CurrentDeviceOrientation == DeviceOrientation.Portrait)
        {
            landscapeMenu.SetActive(false);
            portraitMenu.SetActive(true);
        }
    }

    public void OnImageTargetFound()
    {
        Log("Image target found");
        carAnim.Play("StartEngine");
    }

    public void OnImageTargetLost()
    {
        Log("Image target lost");
    }

}
