using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController player;
    public EnemyController enemy;
    public int currentPlayer;
    public int totalPlayers;
    public GameObject deadText;
    public GameObject winText;

    void youded() 
    {
        deadText.SetActive(true);
    }
    void youween()
    {
        winText.SetActive(true);
    }

    void enemyAttack()
    {
        float damage = (float)(enemy.enemyTurn());
        player.health = player.health - damage;
        IncrementCurrentPlayer();
    }

    void playerAttack()
    {
        float damage;
        if ((damage = (float)player.playerTurn())< 0)
        {

        }
        else
        {
            Debug.Log("Damage: " + damage);
            enemy.health = enemy.health - damage;
            IncrementCurrentPlayer();
            player.Reset();
        }
    }

    void IncrementCurrentPlayer()
    {
        if (currentPlayer == 0)
        {
            currentPlayer = 1;
        }
        else
        {
            currentPlayer = 0;
        }
    }


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (player.health <= 0)
        {
            youded();
        }
        else if(enemy.health <= 0)
        {
            youween();
        }
        else
        {
            if (currentPlayer == 1)
            {
                enemyAttack();
            }
            else
            {
                playerAttack();
            }
        }

    }
}


/*
 * Instantiate - this is how you take a unity object thing and put it into the scene
 * Unity UI - button
 * something involving instantiating a unity UI button...
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */