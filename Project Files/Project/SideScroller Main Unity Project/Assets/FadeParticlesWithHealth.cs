using UnityEngine;

public class FadeParticlesWithHealth : MonoBehaviour
{
    Health parentHealth;
    float startHP;
    Material material;
    // Use this for initialization
    void Start()
    {
        parentHealth = transform.parent.GetComponent<Health>();
        startHP = parentHealth.currentHealth;
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Color temp = material.color;
        temp.a = parentHealth.currentHealth / startHP;
        material.color = temp;
    }
}
