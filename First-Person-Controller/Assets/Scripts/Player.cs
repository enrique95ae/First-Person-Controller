using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController cc;
    public float speed = 10f;

    //GRAVITY
    float ySpeed = 0;
    float gravity = -15f;
    public Transform fpsCamera;
    float pitch = 0f;

    [Range(5, 15)]
    float mouseSensitivity = 10f;

    [Range(45, 85)]
    float pitchRange = 45f;

    //member input values
    float xInput = 0f;
    float zInput = 0f;
    float xMouse = 0f;
    float yMouse = 0f;


    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        UpdateMovement();
    }


    //------------------------functions------------------------------

    void GetInput()
    {
        xInput = Input.GetAxis("Horizontal") * speed;
        zInput = Input.GetAxis("Vertical") * speed;
        xMouse = Input.GetAxis("Mouse X") * mouseSensitivity;
        yMouse = Input.GetAxis("Mouse Y") * mouseSensitivity;
    }

    void UpdateMovement()
    {
        Vector3 move = new Vector3(xInput, 0, zInput);
        move = Vector3.ClampMagnitude(move, speed);
        move = transform.TransformVector(move);

        if (cc.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = 15f;
            }
            else
            {
                ySpeed = gravity * Time.deltaTime;
            }
        }
        else
        {
            ySpeed += gravity * Time.deltaTime;
        }

        //applying movement

        cc.Move((move + new Vector3(0, ySpeed, 0)) * Time.deltaTime);


        transform.Rotate(0, xMouse, 0);


        pitch -= yMouse;
        pitch = Mathf.Clamp(pitch, -pitchRange, pitchRange);
        Quaternion camRotation = Quaternion.Euler(pitch, 0, 0);
        fpsCamera.localRotation = camRotation;
    }
}
