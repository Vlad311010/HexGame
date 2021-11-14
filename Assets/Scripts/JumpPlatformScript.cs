using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatformScript : MonoBehaviour
{
    public float jumpForce;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(jumpForce * transform.up, ForceMode2D.Impulse);
        }
    }
}
