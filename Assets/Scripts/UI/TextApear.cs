using UnityEngine;
using TMPro;

public class TextApear : MonoBehaviour
{

    float aChanel = 0.75f;
    bool appearing = false;
    TMP_Text text;
    

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    }

    void Update()
    {
        if (appearing)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + 0.01f);
            if (text.color.a >= aChanel)
                Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            appearing = true;
        }
    }
}
