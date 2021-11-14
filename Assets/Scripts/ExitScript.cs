using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{   
    static int levelId;
    float levelExitTime;
    AudioSource audio;

    void Awake()
    {
        levelId = SceneManager.GetActiveScene().buildIndex;
        audio = GetComponent<AudioSource>();
        levelExitTime = audio.clip.length + 0.15f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            StartCoroutine(ExitLevel());
        }
    }

    IEnumerator ExitLevel()
    {
        if (!audio.isPlaying)
            audio.Play();
        yield return new WaitForSeconds(levelExitTime);
        SceneManager.LoadScene(++levelId);
    }
}
