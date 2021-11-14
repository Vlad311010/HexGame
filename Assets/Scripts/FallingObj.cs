using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FallingObj : MonoBehaviour
{

    public float fallingSpeed;
    public GameObject destroyParticleSys;
    //SerializedObject particle;

    private void Start()
    {
       /* particle = new SerializedObject(destroyParticleSys);
        particle.FindProperty("Start Color").colorValue = Color.cyan;*/
    }

    void Update()
    {

        
        transform.position += Vector3.down * fallingSpeed * Time.deltaTime;     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject particles = Instantiate(destroyParticleSys, transform.position + new Vector3(-1, 4), Quaternion.identity);
        particles.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
        particles.GetComponent<ParticleSystem>().startLifetime = 5f;

        Destroy(gameObject);
    }

}
