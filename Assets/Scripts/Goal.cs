using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private int requiredCoins;
    private Game game;

    void Start()
    {
        requiredCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        game = FindObjectOfType<Game>();
    }

    public void CheckForCompletion(int coinCount)
    {
        if(coinCount >= requiredCoins)
        {
            game.LoadNextLevel();
        }
        else
        {
            Debug.Log("Not enough carrots");
        }
    }
}
