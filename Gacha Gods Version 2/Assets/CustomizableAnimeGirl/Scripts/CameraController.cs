/*
Camera controller
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CustomizableAnimeGirl
{
    public class CameraController : MonoBehaviour
    {
        private Vector3 targetPos;
        public float cameraMoveSpeed = 0.05f;
        public float cameraRotateSpeed = 200f;
        public float cameraZoomSpeed = 2.0f;
        public bool autoRotate = false;
        public float autoRotateSpeed = 0.1f;

        // Start is called before the first frame update

        void Start()
        {
            targetPos = Vector3.zero;
        }

        // Update is called once per frame
        void Update()
        {
            if (autoRotate){
                transform.RotateAround(targetPos, Vector3.up, autoRotateSpeed);
            }

            // Skip if mouse cursor is on GUI
            if (EventSystem.current.IsPointerOverGameObject()) return;

            // Move camera with WASD
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += transform.up * cameraMoveSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += transform.up * -cameraMoveSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += transform.right * cameraMoveSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += transform.right * -cameraMoveSpeed;
            }

            // Rotate camera with Right Drag
            if (Input.GetMouseButton(1))
            {
                float mouseInputX = Input.GetAxis("Mouse X");
                float mouseInputY = Input.GetAxis("Mouse Y");
                transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * cameraRotateSpeed);
                transform.RotateAround(targetPos, transform.right, mouseInputY * Time.deltaTime * -cameraRotateSpeed);
            }

            // Zoom in and out of the camera with the mouse wheel
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            transform.position += transform.forward * scroll * cameraZoomSpeed;

            // Move the camera by holding the wheel
            if (Input.GetMouseButton(2))
            {
                float mouseInputX = Input.GetAxis("Mouse X");
                float mouseInputY = Input.GetAxis("Mouse Y");
                transform.position -= transform.right * mouseInputX * cameraMoveSpeed;
                transform.position -= transform.up * mouseInputY * cameraMoveSpeed;
            }
        }

        public void toggleAutoRotate(){
            autoRotate = !autoRotate;
        }
    }
}