using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovment : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float RotationSpeed;

    Vector3 startPos = new Vector3(0, 12, 40);
    Vector3 startRot = new Vector3(15, 180, 0);


    public GameManagment gameManagment;

    // Update is called once per frame
    void Update()
    {
        if (gameManagment.inSpeciesSetUp)
        {
            //Dont run script
        }
        else
        {
            Looking();
            Movment();
        }

        // Reset on escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.position = startPos;
            transform.eulerAngles = startRot;
        }
    }

    void Movment()
    {
        float xImput = Input.GetAxis("Horizontal");
        float zImput = Input.GetAxis("Vertical");

        Vector3 dir = transform.forward * zImput + transform.right * xImput;

        transform.position += dir * (Speed / gameManagment.currentTimeSpeed) * Time.deltaTime;
    }

    void Looking()
    {
        //Freezing rotation when space is held
        if (Input.GetMouseButton(1))
        {
            float MouseY = Input.GetAxis("Mouse Y");
            float MouseX = Input.GetAxis("Mouse X");

            Vector3 NewRotation = transform.localEulerAngles;
            NewRotation.x += MouseY * RotationSpeed * -1;
            NewRotation.y += MouseX * RotationSpeed;
            transform.localEulerAngles = NewRotation;
        }
        else
        {
            //No looking
        }
    }
}
