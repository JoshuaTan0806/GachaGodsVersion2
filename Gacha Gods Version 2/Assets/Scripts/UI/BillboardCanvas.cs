using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardCanvas : MonoBehaviour
{
    Camera mainCam;
    Quaternion originalRotation;

    private void Awake()
    {
        mainCam = Camera.main;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = mainCam.transform.rotation * originalRotation;
    }
}
