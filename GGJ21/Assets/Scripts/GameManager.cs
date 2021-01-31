﻿using System.Collections;
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

    private bool dashReady;
    private bool shieldReady;
    private bool bombReady;

    private float dashCooldown = 5.0f;
    private float shieldCooldown = 15.0f;
    private float bombCooldown = 20.0f;

    private float dashTimer;
    private float shieldTimer;
    private float bombTimer;

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

            dashReady = false;
            shieldReady = false;
            bombReady = false;

            dashTimer = dashCooldown;
            shieldTimer = shieldCooldown;
            bombTimer = bombCooldown;

            NextLevel();

            initializedGame = true;
        }
    }

    private void Update()
    {
        // Cooldowns
        if (!dashReady)
        {
            dashTimer -= Time.deltaTime;
            if(dashTimer <= 0)
            {
                dashReady = true;
                dashTimer = 0;
            }
        }

        if (!shieldReady)
        {
            shieldTimer -= Time.deltaTime;
            if (shieldTimer <= 0)
            {
                shieldReady = true;
                shieldTimer = 0;
            }
        }

        if (!bombReady)
        {
            bombTimer -= Time.deltaTime;
            if (bombTimer <= 0)
            {
                bombReady = true;
                bombTimer = 0;
            }
        }
    }

    public void DamagePlayer(int damage)
    {
        lives-=damage;

        if(lives <= 0)
        {
            GameOver();
        }

        Debug.Log("TODO: Damage Player Event");
    }

    public void GameOver()
    {
        Debug.Log("TODO: Game Over Event");
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

    // USE PLAYER ACTION

    public void UseDash()
    {
        if (!hasDash || !dashReady)
        {
            return;
        }

        dashTimer = dashCooldown;
        dashReady = false;
    }

    public void UseShield()
    {
        if (!hasShield || !shieldReady)
        {
            return;
        }

        shieldTimer = shieldCooldown;
        shieldReady = false;
    }

    public void UseBomb()
    {
        if (!hasBomb || !bombReady)
        {
            return;
        }

        bombTimer = bombCooldown;
        bombReady = false;
    }

    // GETS UI

    public int GetLives()
    {
        return lives;
    }

    public float GetDashCooldown()
    {
        return dashTimer;
    }

    public float GetShieldCooldown()
    {
        return shieldTimer;
    }

    public float GetBombCooldown()
    {
        return bombTimer;
    }

    // GETS FOR PLAYER

    public bool CanDash()
    {
        if(hasDash && dashReady)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanShield()
    {
        if (hasShield && shieldReady)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanBomb()
    {
        if (hasBomb && bombReady)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
