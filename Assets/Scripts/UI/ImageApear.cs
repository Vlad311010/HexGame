using UnityEngine;
using UnityEngine.UI;

public class ImageApear : MonoBehaviour
{

    float aChanel = 0.50f;
    bool appering = false;
    Image[] childImages;

   

    void Start()
    {
        childImages = gameObject.GetComponentsInChildren<Image>();
        foreach (Image i in childImages)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        }
    }

    void Update()
    {
        if (appering)
        {
            foreach (Image i in childImages)
            {
                if (i.color.a <= aChanel)
                    i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + 0.01f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            appering = true;
        }
    }
}
