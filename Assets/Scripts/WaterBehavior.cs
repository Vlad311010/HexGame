
using UnityEngine;

public class WaterBehavior : MonoBehaviour
{

    BuoyancyEffector2D effector;


    float defaultDensity = 3;
    float lowerDensity = 1.7f;

    void Start()
    {
        effector = GetComponent<BuoyancyEffector2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Circle"))
        {
            effector.density = lowerDensity;
        }
        else { effector.density = defaultDensity; }
    }
}
