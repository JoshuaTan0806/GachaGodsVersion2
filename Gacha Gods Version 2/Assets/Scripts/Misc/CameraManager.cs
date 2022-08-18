using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] float speed;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            transform.position = Vector3.zero;
        else
        {
            if (Input.GetKey(KeyCode.W))
                transform.Translate(transform.up * speed * Time.deltaTime);
            else if (Input.GetKey(KeyCode.S))
                transform.Translate(-transform.up * speed * Time.deltaTime);

            if (Input.GetKey(KeyCode.A))
                transform.Translate(-transform.right * speed * Time.deltaTime);
            else if (Input.GetKey(KeyCode.D))
                transform.Translate(transform.right * speed * Time.deltaTime);
        }
    }
}
