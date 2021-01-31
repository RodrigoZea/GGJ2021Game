using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HUD : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    public UI_Cooldown dashIcon;
    public UI_Cooldown shieldIcon;
    public UI_Cooldown bombIcon;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDashCooldown(gameManager.GetDashCooldown());
        UpdateShieldCooldown(gameManager.GetShieldCooldown());
        UpdateBombCooldown(gameManager.GetBombCooldown());
        UpdateUILives(gameManager.GetLives());
    }

    public void UpdateUILives(int lives)
    {
        if (heart1.activeSelf)
        {
            if (lives < 1)
            {
                heart1.SetActive(false);
            }
        }
        else
        {
            if (lives >= 1)
            {
                heart1.SetActive(true);
            }
        }

        if (heart2.activeSelf)
        {
            if (lives < 2)
            {
                heart2.SetActive(false);
            }
        }
        else
        {
            if (lives >= 2)
            {
                heart2.SetActive(true);
            }
        }

        if (heart3.activeSelf)
        {
            if (lives < 3)
            {
                heart3.SetActive(false);
            }
        }
        else
        {
            if (lives >= 3)
            {
                heart3.SetActive(true);
            }
        }
    }

    private void UpdateDashCooldown(float cooldown)
    {
        if (cooldown < 0.1f)
        {
            dashIcon.SetEnabled();
        }
        else
        {
            dashIcon.SetCooldown(cooldown);
        }
    }

    private void UpdateShieldCooldown(float cooldown)
    {
        if (cooldown < 0.1f)
        {
            shieldIcon.SetEnabled();
        }
        else
        {
            shieldIcon.SetCooldown(cooldown);
        }
    }

    private void UpdateBombCooldown(float cooldown)
    {
        if (cooldown < 0.1f)
        {
            bombIcon.SetEnabled();
        }
        else
        {
            bombIcon.SetCooldown(cooldown);
        }
    }

    public void DisableUI()
    {

    }
}
