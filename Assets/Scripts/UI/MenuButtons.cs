using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelSelect;
    [SerializeField] GameObject options;
    [SerializeField] GameObject levels;
    [SerializeField] Sprite collectedCoin;
    [SerializeField] Sprite uncollectedCoin;
    int collectedAmount;
    [SerializeField] GameObject amountText;

    public void Start()
    {
        for (int i = 0; i < levels.transform.childCount; i++)
        {
            Sprite sprite;
            if (CoinsCollected.collectedCoins[i])
            {
                sprite = collectedCoin;
                collectedAmount++;
            }
            else
                sprite = uncollectedCoin;
            levels.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        }
        amountText.GetComponent<TMP_Text>().text = collectedAmount.ToString();
        
    }

    public void LevelSelect(int levelId)
    {
        SceneManager.LoadScene(levelId);  
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Play()
    {
        mainMenu.active = false;
        levelSelect.active = true;
    }

    public void Options()
    {
        mainMenu.active = false;
        options.active = true;
    }

    public void ShowMenu()
    {
        mainMenu.active = true;
        options.active = false;
        levelSelect.active = false;
    }
}
