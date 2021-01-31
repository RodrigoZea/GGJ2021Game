using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Audio : MonoBehaviour
{
    public AudioSource gameOver;
    public AudioSource gameTheme;
    public AudioSource doorsOpen;
    public AudioSource gameWin;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void PlayGameWin()
    {
        gameWin.Play();
    }
    public void PlayGameOver()
    {
        gameOver.Play();
    }

    public void PlayDoorsOpen()
    {
        Debug.Log("Ejecutando audio");
        doorsOpen.Play();
    }

    public void PlayGameTheme()
    {
        gameTheme.Play();
    }


}
