using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameWon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.GetComponent<E_Audio>().PlayGameWin();
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
