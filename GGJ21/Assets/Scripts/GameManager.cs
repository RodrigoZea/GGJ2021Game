using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager GetInstance()
    {
        return instance;
    }


    private bool initializedGame = false;

    private int currentLevel;

    private int lives;

    private int score;

    // ATTACKS
    private bool hasDash;
    private bool hasShield;
    private bool hasBomb;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("Destroying instance of GameManager");
            Destroy(this.gameObject);
        }
        else if (instance == null)
        {
            Debug.Log("Creating new instance of GameManager");
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (!initializedGame)
        {
            currentLevel = 0;
            score = 0;
            lives = 3;
            hasDash = true;
            hasShield = true;
            hasBomb = true;

            NextLevel();

            initializedGame = true;
        }
    }

    private void Update()
    {

    }

    public void DamagePlayer()
    {
        lives--;


        Debug.Log("TODO: Damage Player Event");
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        // UI FADE
        // GENERATE MAP

        Debug.Log("TODO: Advance Level");
    }
}
