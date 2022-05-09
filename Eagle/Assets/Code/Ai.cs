using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Ai : MonoBehaviour
{
    Animator anim;

    private NavMeshAgent agent;

    public Transform player;

    public float radius;

    private void Start ()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

    }

    private void Update ()
    {
        Vector3 direction = player.position - this.transform.position;

        direction.y = 0f;

        if (Vector3.Distance(player.position, this.transform.position) < 20f)
        {
            
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                        Quaternion.LookRotation(direction), 0.1f);

            if (direction.magnitude > (float)2.5f)
            { 
                this.transform.Translate(0f, 0f, 0.05f);
                anim.SetBool("Walk", true);
                anim.SetBool("Attack", false);
                agent.enabled = true;
            }
            else
            {
                anim.SetBool("Walk", false);
                if((int)player.position.y <= this.transform.position.y + 2f)
                {
                    anim.SetBool("Attack", true);
                    anim.SetBool("Walk", false);
                    agent.enabled = false;
                }
                else
                {
                    anim.SetBool("Attack", false);
                    anim.SetBool("Walk", false);
                }
            }
        }
        else if(Vector3.Distance(player.position, this.transform.position) >15f)
        {
            anim.SetBool("Attack", false);
            anim.SetBool("Walk", true);
            agent.enabled = true;
            if (!agent.hasPath)
            {
                agent.SetDestination(GetPoint.Instance.GetRandomPoint(transform, radius));
            }

        }
    }

  // #if UNITY_EDITOR

  //     private void OnDrawGizmos ()
  //     {
  //         Gizmos.DrawWireSphere (transform.position, radius);
  //     }

  // #endif
}
