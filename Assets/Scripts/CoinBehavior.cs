using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class CoinBehavior : MonoBehaviour
{
    Light2D light;
    SpriteRenderer sprite;
    AudioSource audio;


    void Start()
    {
        light = GetComponent<Light2D>();
        sprite = GetComponent<SpriteRenderer>();
        light.lightType = Light2D.LightType.Sprite;
        audio = GetComponent<AudioSource>();
    }

    public void OnPickUp()
    { 
       //logic
        int sceneId = SceneManager.GetActiveScene().buildIndex;
        CoinsCollected.SetAsCollected(sceneId);
        //sound
        sprite.enabled = false;
        light.enabled = false;
        //visual

        StartCoroutine(DelayDestroy());
    }

    //OnTriggerEnter -> Collect(), PLaySound(), LevelSetCoinCollected();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            OnPickUp();
        }
    }

    IEnumerator DelayDestroy()
    {
        if (!audio.isPlaying)
            audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        Destroy(gameObject);
    }


}
