using System.Collections;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Door[] doors;
    public bool oneTimeButtuon;
    SpriteRenderer sprite;

    public Color deactiveColor;
    public Color activeColor;
    public Sprite triangleSprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = deactiveColor;
        if (oneTimeButtuon)
        {
            sprite.sprite = triangleSprite;
            transform.position += new Vector3(0, -1.45f);
            GetComponent<BoxCollider2D>().size = new Vector2(1.8f, 1.6f);
            GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.31f);
        }
    }

    public void OnActivation()
    {
        sprite.color = activeColor;
        foreach (Door i in doors)
        {
            i.active = true;
        }
    }

    public void OnDeactivation()
    {
        sprite.color = deactiveColor;
        foreach (Door i in doors)
        {
            i.active = false;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")) || collision.gameObject.layer.Equals(LayerMask.NameToLayer("Moveable")))
        {

            OnActivation();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!oneTimeButtuon && collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")) || collision.gameObject.layer.Equals(LayerMask.NameToLayer("Moveable")))
            OnDeactivation();
    }

}
