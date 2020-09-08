using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalExtension;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RotateY(float direction)
    {
        // transform.Rotate()
        Log("rotY");
        gameObject.transform.AddEulerY(direction);
    }
}
