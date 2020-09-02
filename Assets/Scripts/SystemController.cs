using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GlobalExtension;

/// <summary>
/// Контролирует: 
///  - смену сцен
/// </summary>
public class SystemController : MonoBehaviour
{
    private bool sceneChanging = false;
    private string nextSceneName = "";
    private bool isPaused = false;
    private bool lockMouse = false;

    //Камера
    [SerializeField]
    bool fadeInEffect = true;
    [SerializeField]
    GameObject fadeFrame = null;
    private Animation fadeAnimation;
    public bool IsFadeOutPlaying { get { return fadeAnimation.IsPlaying("FadeOut"); } }
    public bool IsFadeInPlaying { get { return fadeAnimation.IsPlaying("FadeIn"); } }
    [SerializeField]
    GameObject pausePanel = null;

    //Ориентация
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
        LockMouse(false);
        //Проверки
        if (fadeFrame.NotExist())
        {
            Error("SystemController -> fadeFrame is null");
        } else
        {
            fadeFrame.SetActive(true);
            fadeAnimation = fadeFrame.GetComponent<Animation>();
            if (fadeAnimation.NotExist())
                Error("SystemController -> fadeAnimation  is NULL");
        }

        //Эффект появления (если включен)
        if (fadeInEffect)
            FadeIn();
        else
            fadeFrame.SetActive(false);

        //Определение ориентации экрана
        if (Screen.width > Screen.height)
            CurrentDeviceOrientation = DeviceOrientation.LandscapeLeft;
        else
            CurrentDeviceOrientation = DeviceOrientation.Portrait;
    }

    private void FixedUpdate()
    {
        //Отключение FadeFrame после анимации
        if (fadeInEffect)
        {
            if (!fadeAnimation.isPlaying)
            {
                fadeInEffect = false;
                fadeFrame.SetActive(false);
            }
        }

        if (lockMouse)
        {

        }
        if (sceneChanging)
            ChangeScene(nextSceneName);

        //Ориентация устройства
        if (Input.deviceOrientation != DeviceOrientation.Unknown)
            CurrentDeviceOrientation = Input.deviceOrientation;
    }

    /// <summary>
    /// Смена сцены с эффектом затухания
    /// </summary>
    /// <param name="sceneName">Название сцены</param>
    /// <param name="withFadeOut">Использование эффекта затухания</param>
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
            FadeOut();
        }
        if (!IsFadeOutPlaying)
            SceneManager.LoadScene(nextSceneName);
    }

    /// <summary>
    /// Переключатель паузы в игре
    /// </summary>
    public void Pause()
    {
        Pause(!isPaused);
    }
    /// <summary>
    /// Установка паузы в игре
    /// </summary>
    /// <param name="enable"></param>
    public void Pause(bool enable)
    {
        isPaused = enable;
        //LockMouse(!enable);  //Пока без лока мыши, так будет интереснее
        if (enable)
        {
            Time.timeScale = 0;
            ShowPausePanel();
        } else
        {
            Time.timeScale = 1f;
            HidePausePanel();
        }
    }

    /// <summary>
    /// Переключатель блокировки мыши
    /// </summary>
    public void LockMouse()
    {
        LockMouse(!lockMouse);
    }
    /// <summary>
    /// Установка блокировки мыши
    /// </summary>
    /// <param name="enable"></param>
    public void LockMouse(bool enable)
    {
        lockMouse = enable;
        if (enable)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.Confined;
    }

    /// <summary>
    /// Эффект появления
    /// </summary>
    void FadeIn()
    {
        fadeFrame.SetActive(true);
        fadeAnimation.Play("FadeIn");
    }

    /// <summary>
    /// Эффект затухания
    /// </summary>
    public void FadeOut()
    {
        fadeFrame.SetActive(true);
        fadeAnimation.Play("FadeOut");
    }

    /// <summary>
    /// Показать панель паузы
    /// </summary>
    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }

    /// <summary>
    /// Скрыть панель паузы
    /// </summary>
    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }

}
