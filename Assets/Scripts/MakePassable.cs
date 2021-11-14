using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePassable : MonoBehaviour
{

    public float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Junk");
            /*if (GetComponent<Rigidbody2D>().velocity.magnitude < 3)
            {
                Destroy(this.gameObject.GetComponent<Rigidbody2D>());
               
            }*/
            Destroy(this);
        }

        
    }
}
