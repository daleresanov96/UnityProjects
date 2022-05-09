using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{

  public float movementSpeed = 100f;
  public float resetSpeed = 100f;
  public float shiftSpeed = 150f;
  public float controlSpeed = 50f;



  public float horizSensitivity = 2f;
  // public float resetHorizSensitivity = 2;
  public float verSensitivity =2f;
  // public float resetVerSensitivity = 2f;

  private float yaw = 0f;
  private float pitch = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
      yaw += horizSensitivity * Input.GetAxis("Mouse X");
      pitch -= verSensitivity * Input.GetAxis("Mouse Y");

       transform.eulerAngles =new  Vector3(0.0f,0.0f,yaw);

      if(Input.GetKey(KeyCode.LeftShift))
      {
        movementSpeed = shiftSpeed;
      }
      else
      {
          movementSpeed = resetSpeed;
      }
      if(Input.GetKey(KeyCode.W))
      {
        transform.localPosition += transform.forward * Time.deltaTime * movementSpeed;
      }
      if(Input.GetKey(KeyCode.A))
      {
        transform.localPosition += -transform.right * Time.deltaTime * controlSpeed;
      }
      if(Input.GetKey(KeyCode.S))
      {
        transform.localPosition += -transform.forward * Time.deltaTime * controlSpeed;
      }
      if(Input.GetKey(KeyCode.D))
      {
        transform.localPosition += transform.right * Time.deltaTime * controlSpeed;
      }
    }
}
