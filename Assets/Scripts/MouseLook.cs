using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {

    public float xRotation, yRotation;
    public float currentXRotation, currentYRotation;
    public float xRotationV, yRotationv;

    public Transform orthoView;
    public Transform playerView;

    private float lookSensitivity = 5f;
    private float lookSmoothDamp = 0.1f;

    private Camera c;


    void Start()
    {
        c = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.X))
            return;

        xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;
        yRotation += Input.GetAxis("Mouse X") * lookSensitivity;

        if (Input.GetKey(KeyCode.C) /*|| true*/)
        {
            c.orthographic = true;
            transform.rotation = orthoView.rotation;
            transform.position = orthoView.position;
        }
        else
        {
            c.orthographic = false;
            transform.rotation = playerView.rotation;
            transform.position = playerView.position;

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        }

        

    }
}




