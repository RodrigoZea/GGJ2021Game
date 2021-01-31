using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

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
    private bool hasMelee;
    private bool hasShield;
    private bool hasBomb;

    private bool dashReady;
    private bool meleeReady;
    private bool shieldReady;
    private bool bombReady;

    private float dashCooldown = 3.0f;
    private float meleeCooldown = 5.0f;
    private float shieldCooldown = 15.0f;
    private float bombCooldown = 20.0f;

    private float dashTimer;
    private float meleeTimer;
    private float shieldTimer;
    private float bombTimer;


    // POWERUPS
    private bool doubleShootActive;

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
            hasMelee = true;

            dashReady = false;
            meleeReady = false;
            shieldReady = false;
            bombReady = false;

            dashTimer = dashCooldown;
            meleeTimer = meleeCooldown;
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

        if (!meleeReady)
        {
            meleeTimer -= Time.deltaTime;
            if (meleeTimer <= 0)
            {
                meleeReady = true;
                meleeTimer = 0;
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

    public int DamagePlayer(int damage)
    {
        lives-=damage;

        if(lives <= 0)
        {
            StartCoroutine(GameOver());
        }

        return lives;
    }

    private IEnumerator GameOver()
    {
        Debug.Log("TODO: Game Over Event");
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GameOver");
        Destroy(this.gameObject);
    }

    public void Win()
    {
        Debug.Log("TODO: Win Event");
        SceneManager.LoadScene("Win");
        RG_CameraController.instance.GetComponent<E_Audio>().PlayGameWin();
        Destroy(this.gameObject);
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

    public void UseMelee()
    {
        if (!hasMelee || !meleeReady)
        {
            return;
        }

        meleeTimer = meleeCooldown;
        meleeReady = false;
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

    public float GetMeleeCooldown()
    {
        return meleeTimer;
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

    public bool CanMelee()
    {
        if (hasMelee && meleeReady)
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
