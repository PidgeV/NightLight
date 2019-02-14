using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaLight : MonoBehaviour {

    public Transform originLight = null;
    public GameObject pointTowards;
    public GameObject volume = null;
    public int numberRays = 3;
    public int lengthRays = 10;         //Get from light
    public float spaceBetween = 1.5f;
    public int layer = 8;
    public int offsetAngle = -65;
    public GameObject[] players;
    PlayerLightCounter[] plc;

    // Use this for initialization
    void Start () {
        if (originLight == null)
            originLight = transform;
        plc = new PlayerLightCounter[players.Length];
        for (int i = 0; i < players.Length; i++)
        {
            plc[i] = players[i].GetComponent<PlayerLightCounter>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << layer;
        Vector3 direction = Vector3.zero;
        bool hitBl = false;
    
        for (int i = 0; i < numberRays ; i++)
        {
            direction = pointTowards.transform.position;

            direction.z += (spaceBetween * i);
            direction = direction.normalized;
            direction = Quaternion.AngleAxis(offsetAngle, Vector3.down) * direction;


            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, lengthRays, layerMask) && volume == null)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(direction) * hit.distance, Color.red);
                
                if (hit.collider.tag == "Player")
                {
                    PlayerLightCounter lightCounter = hit.collider.gameObject.GetComponent<PlayerLightCounter>();
                    lightCounter.isInLight = true;
                    hitBl = true;
                    Debug.Log("Did Hit Player");
                }
                else
                    Debug.Log("Did Hit");
            }
            else if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, lengthRays, layerMask) && volume != null)
            {
                
                Collider m_Collider = volume.GetComponent<Collider>();
                if(m_Collider.bounds.Contains(hit.transform.position) && hit.collider.tag == "Player")
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(direction) * hit.distance, Color.red);
                    Debug.Log("Did Hit Player in a Collider");
                    hitBl = true;
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(direction) * lengthRays, Color.blue);
                    if (!hitBl)
                    {
                        for (int j = 0; j < players.Length; j++)
                        {
                            plc[j].isInLight = false;
                        }
                    }
                    Debug.Log("Did not Hit");
                }
                //Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(direction) * lengthRays, Color.blue);
                if (!hitBl)
                {
                    for (int j = 0; j < players.Length; j++)
                    {
                        plc[j].isInLight = false;
                    }
                }
                Debug.Log("Did not Hit");
            }
        }
        /*for (int i = numberRays / 2; i < numberRays; i++)
        {
            direction = pointTowards.transform.position;

            direction.z -= (spaceBetween * i);
            direction = direction.normalized;
            direction = Quaternion.AngleAxis(-90, Vector3.down) * direction;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, lengthRays, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(direction) * hit.distance, Color.red);
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(direction) * lengthRays, Color.blue);
                Debug.Log("Did not Hit");
            }
        }*/


    }
}
