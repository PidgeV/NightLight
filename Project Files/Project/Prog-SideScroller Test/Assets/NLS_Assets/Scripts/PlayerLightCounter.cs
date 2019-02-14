using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightCounter : MonoBehaviour {

    public bool mustBeInLight = false;
    public bool isInLight = false;
    private int startHP;

    public float curTime = 0.0f;
    public float damageDelay = 1;
    public int healthToSubract = 1;

    public float healingDelay = 10;
    public int healthToAdd = 3;

    private DealDamage dealDamage;

    // Use this for initialization
    void Start () {
		
	}

    private void Awake()
    {
        dealDamage = GetComponent<DealDamage>();
        startHP = GetComponent<Health>().currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (mustBeInLight != isInLight)
        {
            curTime += Time.deltaTime;
            
            if (curTime >= damageDelay)
            {
                curTime = 0.0f;
                GetComponent<Health>().currentHealth -= healthToSubract;
            }
        }
        else
        {
            curTime += Time.deltaTime;

            if (curTime >= healingDelay)
            {
                curTime = 0.0f;

                if (GetComponent<Health>().currentHealth <= startHP)
                {
                    GetComponent<Health>().currentHealth += healthToAdd;
                    if (GetComponent<Health>().currentHealth >= startHP)
                        GetComponent<Health>().currentHealth = startHP;
                }
            }
        }
    }
}
