using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour
{
    public Vector2 delayRange = new Vector2(1,2);
    public bool startState = false;

    private bool _Enabled = false;
    private bool firstUpdate = true;
    Light light;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
        light.enabled = !startState;
        StartCoroutine(LightFlash());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (firstUpdate)
        {
            _Enabled = true;
            light.enabled = startState;
            firstUpdate = false;
        }
    }

    IEnumerator LightFlash()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(delayRange.x, delayRange.y));

            if (_Enabled)
            {
                light.enabled = !light.enabled;
            }
        }
    }
}
