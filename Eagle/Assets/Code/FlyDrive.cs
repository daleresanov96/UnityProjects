using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDrive : MonoBehaviour
{
    public Animator anim;
    FlyDrive scriptF;
    MoveDrive scriptM;
    Terrain terrain;
    CharacterController cC;
    UnityEngine.AI.NavMeshAgent agent;
    public float speed = 10.0f;
    public float shiftSpeed = 15.5f;
    public float rotationSpeed = 100.0f;
    public float down = -0.08f;
    public float up = 0.1f;
    public float cloud = 60;
    private float translation;
    private float shiftTranslation;
    private float rotation;
    private float yPos;
    private float y0ffset = 0.5f;
    void Start ()
    {
        anim = GetComponent< Animator > ();
        agent = GetComponent< UnityEngine.AI.NavMeshAgent >();
        scriptF = GetComponent< FlyDrive >();
        scriptM = GetComponent<MoveDrive>();
        cC = GetComponent<CharacterController>();
    }
    void Update ()
    {
        yPos = Terrain.activeTerrain.SampleHeight(new Vector3 (this.transform.position.x, 0 , this.transform.position.z)) + y0ffset;
        Fly ();
        Anima ();
    }

    public void Fly ()
    {
        translation = Input.GetAxis("VerticalNotS") * speed;
        shiftTranslation = Input.GetAxis("VerticalNotS") * shiftSpeed;
        rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        shiftTranslation *= Time.deltaTime;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        if (Input.GetKey("left ctrl") == false
            &&transform.position.z <= 1000f
                && transform.position.x <= 1000f
                    && transform.position.z >= 0f
                        && transform.position.x >= 0f)
            transform.Translate(0f, 0f, translation);
        if (transform.position.z >= 1000f
                && transform.position.x >= 1000f
                    && transform.position.z <= 0f
                        && transform.position.x <= 0f)
            transform.Translate(0f, 0f, translation -1f);
        transform.Rotate(0f, rotation, 0f);
        if (transform.position.y <= (yPos + 0.5f))
        {
            // scriptF.enabled = false;
            // scriptM.enabled = true;
            // cC.enabled = true;
            //anim.SetBool("lanfloor", true);
            //agent .enabled = true;

            transform.Translate( 0f, 0.1f, 0f);
            //GetComponent<Rigidbody>().enabled = false;
        }
    }

    public void Anima ()
    {
        if (rotation != 0)
        {
            anim.SetBool("fly", false);
            anim.SetBool("glide", false);
            anim.SetBool("fall", false);
            anim.SetBool("flyB", false);
            this.transform.Rotate(0f, rotation, 0f);
                                //Left<
            if (rotation < 0f)
            {
                if (Input.GetKey("w"))
                {
                    anim.SetBool("fly", false); 
                    anim.SetBool("flyB", false);
                    anim.SetBool("fall", false);
                    anim.SetBool("flyR", false);  
                    anim.SetBool("flyL", true);
                }
                else
                {
                    anim.SetBool("glide", false);
                    anim.SetBool("flyB", false);
                    anim.SetBool("fall", false);
                    anim.SetBool("glideR", false);
                    anim.SetBool("glideL", true);
                }
            } 
                                //Ringht>               
            if (rotation > 0f)
            {
                if (Input.GetKey("w"))
                {
                    anim.SetBool("fly", false);
                    anim.SetBool("flyB", false);
                    anim.SetBool("fall", false);
                    anim.SetBool("flyL", false); 
                    anim.SetBool("flyR", true);
                }
                else
                {
                    anim.SetBool("glide", false);
                    anim.SetBool("glideL", false);
                    anim.SetBool("glideR", true);
                }
            }
        }
                            //Legt<>Ringht(End)
        else 
        {
            if (Input.GetKey("w") || Input.GetKey("space") && Input.GetKey("left ctrl") == false)  
            {
                anim.SetBool("flyB", false);
                anim.SetBool("glide", false);
                anim.SetBool("fall", false);
                anim.SetBool("fly", true);
            }
            else
            {
                anim.SetBool("flyR", false);
                anim.SetBool("flyL", false);
                anim.SetBool("glideR", false);
                anim.SetBool("glideL", false);
                anim.SetBool("fly", false);
                anim.SetBool("glide", false);
                anim.SetBool("fall", false);
                anim.SetBool("flyB", true);
            }
        }
        if (Input.GetKey("space") && transform.position.y <= (yPos + cloud))
        {
                transform.Translate( 0f, up, 0f);
        }
        else if (Input.GetKey("w") && Input.GetKey("left shift") && Input.GetKey("left ctrl") == false)
        {
            transform.Translate(0f, 0f, shiftTranslation);
        }
        if (Input.GetKey("left ctrl") && transform.position.y >= (yPos + 1f))
        {
            anim.SetBool("flyR", false);
            anim.SetBool("flyL", false);
            anim.SetBool("glideR", false);
            anim.SetBool("glideL", false);
            anim.SetBool("fly", false);
            anim.SetBool("glide", false);
            anim.SetBool("flyB", false);
            anim.SetBool("fall", true);
            transform.Translate(0f, down, 0f);
        }
    }

}
