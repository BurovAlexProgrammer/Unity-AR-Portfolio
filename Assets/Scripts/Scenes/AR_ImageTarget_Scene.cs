using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalExtension;

public class AR_ImageTarget_Scene : MonoBehaviour
{
    private Logger logger;
    private SystemController systemController;
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
        systemController = GameObject.FindObjectOfType<SystemController>();
        systemController.DeviceOrientationChanged += OnDeviceOrientationChanged;
        car = GameObject.Find("Tocus");
        carAnim = car.GetComponent<Animator>();
    }

    private void Start()
    {
        if (logger == null)
            throw new NullReferenceException();
        if (systemController == null)
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

    public void GoToMainMenu()
    {
        systemController.ChangeScene(Scenes.MAIN_MENU_SCENE);
    }

    void OnDeviceOrientationChanged(object sender, EventArgs e)
    {
        Log("New device orientation: " + systemController.CurrentDeviceOrientation);

        //Адаптация меню (отключение одних и подключение других элементов)
        if (systemController.CurrentDeviceOrientation == DeviceOrientation.LandscapeLeft |
            systemController.CurrentDeviceOrientation == DeviceOrientation.LandscapeRight)
        {
            landscapeMenu.SetActive(true);
            portraitMenu.SetActive(false);
        }
        if (systemController.CurrentDeviceOrientation == DeviceOrientation.Portrait)
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
