using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Cooldown : MonoBehaviour
{
    public Text cooldownTime;
    public GameObject darkBG;
    public GameObject disabledIcon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEnabled()
    {
        darkBG.SetActive(false);
        disabledIcon.SetActive(false);
        cooldownTime.gameObject.SetActive(false);
    }

    public void SetDisabled()
    {
        darkBG.SetActive(true);
        disabledIcon.SetActive(true);
    }

    public void SetCooldown(float time)
    {
        darkBG.SetActive(true);
        cooldownTime.gameObject.SetActive(true);
        cooldownTime.text = time.ToString("0.0");
    }
}
