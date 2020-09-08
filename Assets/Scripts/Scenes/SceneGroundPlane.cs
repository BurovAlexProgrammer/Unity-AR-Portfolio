using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GlobalExtension;

public class SceneGroundPlane : MonoBehaviour
{
    [SerializeField]
    GameObject euffelTower;
    [SerializeField]
    GameObject cameraAR;

    //[SerializeField]
    //Не видно Метод в делегате инспектора с параметром enum - почему? Есть выход?
    //public  enum RotateType { xLeft, xRigth, yLeft, yRight, zLeft, zRight}
    void Start()
    {
        euffelTower.CheckExist();
        cameraAR.CheckExist();
    }

    void Update()
    {
        
    }

    public void RotateY(float direction)
    {
        Log("rotY");
        euffelTower.transform.AddEulerY(direction);
    }

    public void MoveSide(float direction)
    {
        Log("moveSide");
        var vector = new Vector3(0, cameraAR.transform.rotation.y, 0);
        euffelTower.transform.Translate(new Vector3(direction*Time.fixedDeltaTime,0,0));
    }

    public void Scale(float direction)
    {
        Log("moveSide");
        var s = euffelTower.transform.localScale;
        euffelTower.transform.localScale = s * (1 + direction*Time.fixedDeltaTime);
    }
}
