using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HUD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateUILives(int lives)
    {

    }

    public void UpdateUICooldown(PlayerAction action, float percentage)
    {
        switch (action)
        {
            case PlayerAction.Shoot:
                ///
                break;
        }
    }

    public void DisableUI()
    {

    }
}
