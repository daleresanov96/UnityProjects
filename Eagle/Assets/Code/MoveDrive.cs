using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDrive : MonoBehaviour
{
  public Animator anim;
  public CharacterController controller;
  public Transform cam;
  public Transform groundCheck;
  public LayerMask groundMask;
  public float speed = 6f;
  public float jump = 3f;
  public float turnSmoothTime = -0.2f;
  public float gravity = -9.81f;
  public float groundDistance = 0.2f;

  float turnSmoothVelocity;
  private Vector3 velocity;
  private bool isGrounded;
    void Start() 
    {
        anim = GetComponent< Animator > ();
    }
    // Update is called once per frame
    void Update()
    {
      anim.SetBool("fly", false);
      isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

      if (isGrounded && velocity.y < 0)
      {
        velocity.y = -2f;
      }

      float horizontal = Input.GetAxisRaw("Horizontal");
      float vertical = Input.GetAxisRaw("Vertical");
      Vector3 direction = new Vector3(horizontal, 0f,vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
          float targetAngle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
          float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
          transform.rotation = Quaternion.Euler(0f, angle, 0f);


          Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
          controller.Move(moveDir.normalized * speed * Time.deltaTime);

          if (Input.GetButtonDown("Jump") && isGrounded)
          {
            velocity.y = Mathf.Sqrt(jump * -2.0f * gravity);
          }

          velocity.y += gravity * Time.deltaTime;
          controller.Move(velocity * Time.deltaTime);




        }
    }
}
