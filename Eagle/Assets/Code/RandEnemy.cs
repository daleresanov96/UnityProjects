using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandEnemy : MonoBehaviour
{
    public Terrain terrain;
    float xPos;
    float yPos;
    float zPos;
    float xsPos;
    float zsPos;
    float xfPos;
    float zfPos;
    float y0ffset = 0.5f;
    public int animalCount;
    public GameObject[] animal;
    UnityEngine.AI.NavMeshAgent agent;
    int counts;

    void Start ()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
        StartCoroutine(EnemyDrop ());
    }

    IEnumerator EnemyDrop ()
    {
        while (counts < animalCount)
        {
            for (int i = 0; i < animal.Length; i++)
            {
                xsPos = terrain.transform.position.x;
                zsPos = terrain.transform.position.z;
                xfPos = terrain.terrainData.size.x;
                zfPos = terrain.terrainData.size.x;

                xPos = UnityEngine.Random.Range(xsPos, xsPos + xfPos);
                zPos = UnityEngine.Random.Range(zsPos, zsPos + zfPos);
                yPos = Terrain.activeTerrain.SampleHeight(new Vector3 (xPos, 0f , zPos)) + y0ffset;
               Vector3 navAgent = new Vector3 (xPos, yPos, zPos);
               UnityEngine.AI.NavMeshHit hit;
                if (UnityEngine.AI.NavMesh.SamplePosition(navAgent, out hit, 100f, UnityEngine.AI.NavMesh.AllAreas))
                {
                    Instantiate(animal[i], hit.position, Quaternion.identity);
                }
                yield return new WaitForSeconds(0.00001f);
            }
            counts += 1;
        }
    }

}
