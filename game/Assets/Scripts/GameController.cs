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
    public GameObject deadText;
    public GameObject winText;
    public GameObject endTurnButton;

    void PlayerDeath() 
    {
        deadText.SetActive(true);
    }

    void PlayerWin()
    {
        winText.SetActive(true);
    }

    void enemyAttack()
    {
        float damage;
        if ((damage = (float)(enemy.enemyTurn())) < 0)
        {

        }
        else
        {
            player.health = player.health - damage;
            IncrementCurrentPlayer();
            enemy.Reset();
        }

    }

    //player attack
    void playerAttack()
    {
        float damage;
        if ((damage = (float)player.playerTurn()) < 0)
        {

        }
        else
        {
            Debug.Log("Damage: " + damage);
            enemy.health = enemy.health - damage;
            player.ResetDamage();
        }
    }


    //increments the current player
    void IncrementCurrentPlayer() {
        if (currentPlayer == 0) {
            currentPlayer = 1;
            endTurnButton.SetActive(false);
        }
        else {
            currentPlayer = 0;
            endTurnButton.SetActive(true);
        }
    }


    void Start()
    {
        endTurnButton.GetComponent<Button>().onClick.RemoveAllListeners();
        endTurnButton.GetComponent<Button>().onClick.AddListener(EndPlayerTurn);
    }


    void EndPlayerTurn()
    {
        IncrementCurrentPlayer();
        player.Reset();
    }



    // Update is called once per frame
    void Update()
    {
        if (player.health <= 0)
        {
            PlayerDeath();
        }
        else if(enemy.health <= 0)
        {
            PlayerWin();
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