using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Triangle") || collision.gameObject.CompareTag("Square") || collision.gameObject.CompareTag("Circle"))
        {
            PlayerController player = collision.GetComponentInParent<PlayerController>();
            player.Death();
        }
    }
}
