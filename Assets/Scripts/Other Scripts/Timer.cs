using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public TextMeshProUGUI timerTXT;
    public float timeRemaining;
    public GameManager gameManagerUI;
    public Collision winCollider;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else if (timeRemaining < 0)
        {
            timeRemaining = 0;
           
        }
       
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerTXT.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (timeRemaining > 0)
        {

        }
        
        if (timeRemaining <= 0)
        {
            gameManagerUI.gameOver();
        }
    }

}
