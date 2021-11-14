using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoinsCollected
{
    public static bool[] collectedCoins = new bool[10];//length = number of levels
    public static int amount = 0;
    
    static void Start()
    {
        for (int i = 0; i < collectedCoins.Length; i++)
        {
            collectedCoins[i] = false;
        }
    }

    public static void SetAsCollected(int levelId)
    {
        if (amount == 0)
            Start();

        if (!collectedCoins[levelId - 1])
        { collectedCoins[levelId - 1] = true; amount++; }
    }
    //on loading level select menu check collectedCoins for every level -> change some visual 
    //
}

