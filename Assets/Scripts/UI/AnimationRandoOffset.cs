using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRandoOffset : MonoBehaviour   
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();    
    }

    public void RandomOffset()
    {
        anim.speed = 0;
        StartCoroutine(RandowWait());
    }

    IEnumerator RandowWait()
    {
        yield return new WaitForSecondsRealtime(Random.Range(0.8f, 3f));
        anim.speed = 1;
    }
}
