﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Control: 
///  - scene changing
/// </summary>
public class SystemController : MonoBehaviour
{
    public CameraController cameraController;
    private bool sceneChanging = false;
    private string nextSceneName = "";
    private bool isPaused = false;
    private bool lockMouse = false;

    void Start()
    {
        cameraController = GetComponent<CameraController>();
        //Check requered objects
        if (cameraController.NotExist())
            throw new Exception("Requered object os NULL");
        LockMouse(false);
    }

    private void FixedUpdate()
    {
        if (lockMouse)
        {

        }
        if (sceneChanging)
            ChangeScene(nextSceneName);
    }

    /// <summary>
    /// Change scene with fade out effect optional
    /// </summary>
    /// <param name="sceneName">Target scane name</param>
    /// <param name="withFadeOut">Turn on fade out animation</param>
    public void ChangeScene(string sceneName, bool withFadeOut = true)
    {
        nextSceneName = sceneName;
        if (!withFadeOut)
        {
            SceneManager.LoadScene(nextSceneName);
            return;
        }
        if (!sceneChanging)
        {
            sceneChanging = true;
            cameraController.FadeOut();
        }
        if (!cameraController.IsFadeOutPlaying)
            SceneManager.LoadScene(nextSceneName);
    }

    /// <summary>
    /// Switch pause in game
    /// </summary>
    public void Pause()
    {
        Pause(!isPaused);
    }
    /// <summary>
    /// Set game to pause
    /// </summary>
    /// <param name="enable"></param>
    public void Pause(bool enable)
    {
        isPaused = enable;
        //LockMouse(!enable);  //Пока без лока мыши, так будет интереснее
        if (enable)
        {
            Time.timeScale = 0;
            cameraController.ShowPausePanel();
        } else
        {
            Time.timeScale = 1f;
            cameraController.HidePausePanel();
        }
    }

    /// <summary>
    /// Switch lock mouse
    /// </summary>
    public void LockMouse()
    {
        LockMouse(!lockMouse);
    }
    public void LockMouse(bool enable)
    {
        lockMouse = enable;
        if (enable)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.Confined;
    }
}
