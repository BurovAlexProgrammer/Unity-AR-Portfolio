using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_Scene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Quit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
    #else
        Application.Quit();
    #endif
    }

    //Переход в гл. меню - AR Примеры 
    public void GoToExamples()
    {

    }

}
