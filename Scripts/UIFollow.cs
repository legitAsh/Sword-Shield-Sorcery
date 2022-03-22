using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour
{
    public Camera _camera;
    public Vector3 screenPoint;
    public float rotationSpeed = 1f;
  
    void Start()
    {

    }

    void Update() {
        screenPoint = _camera.transform.position;
        Vector3 rotaion = (Quaternion.LookRotation(screenPoint).eulerAngles * rotationSpeed);
        rotaion.x = 0f;
        transform.rotation = Quaternion.Euler(rotaion);
    }
}
