﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static GlobalExtension;

public class SceneMainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject panelMainMenu;
    [SerializeField]
    GameObject panelSureQuit;
    [SerializeField]
    GameObject panelExamples;

    void Start()
    {
        panelMainMenu.CheckExist();
        panelSureQuit.CheckExist();
        panelExamples.CheckExist();
    }

    void Update()
    {
        
    }

    public void Quit()
    {
        QuitGame();
    }

    public void GoToSureQuit()
    {
        panelSureQuit.SetActive(true);
    }


    public void GoToExamples()
    {
        panelExamples.SetActive(true);
    }

    public void GoToMainMenu()
    {
        panelSureQuit.SetActive(false);
        panelExamples.SetActive(false);
        panelMainMenu.SetActive(true);
    }
}