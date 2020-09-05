using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GlobalExtension;

public class SceneGroundPlane : MonoBehaviour
{
    GameObject euffelTower;
    //[SerializeField]
    //Не видно Метод в делегате инспектора с параметром enum - почему? Есть выход?
    //public  enum RotateType { xLeft, xRigth, yLeft, yRight, zLeft, zRight}
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void RotateY(float direction)
    {
        // transform.Rotate()
        Log("rotY");
        euffelTower.transform.AddEulerY(direction);
    }


}
