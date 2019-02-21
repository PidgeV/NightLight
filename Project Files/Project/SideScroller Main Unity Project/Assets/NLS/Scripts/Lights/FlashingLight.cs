using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour {
    public Vector2 delayRange = new Vector2(1,2);
    Light light;
	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
        StartCoroutine(LightFlash());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator LightFlash()
    {
        while(true)
        {
            light.enabled = !light.enabled;
            yield return new WaitForSeconds(Random.Range(delayRange.x, delayRange.y));
        }
    }
}
