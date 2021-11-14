using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class SecretWall : MonoBehaviour
{
    SpriteRenderer sprite;

    float aChanel = 0.40f;
    bool fading = false;
    ShadowCaster2D shadow;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        shadow = GetComponent<ShadowCaster2D>();
    }

    void Update()
    {
        if (fading)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - 0.01f);
            if (sprite.color.a < aChanel)
                Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            fading = true;
            shadow.castsShadows = false;
            shadow.selfShadows = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (fading && collision.gameObject.layer.Equals(LayerMask.NameToLayer("Hiden")))
        {
            //Debug.Log(collision.gameObject.name);
            collision.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "OverlayObjects";
        }
        
    }
}
