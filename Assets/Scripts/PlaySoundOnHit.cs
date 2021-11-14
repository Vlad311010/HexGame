using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnHit : MonoBehaviour
{
    AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!audio.isPlaying && collision.relativeVelocity.magnitude > 30f)
        {
            audio.Play();
        }
    }

}
