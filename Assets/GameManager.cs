using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject gameWonUI;
    public Collision winCollider;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameOver() 
    { 
        gameOverUI.SetActive(true);
    }

    public void restart()
    {
        Debug.Log(" its happening");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void gameWon()
    {
        gameWonUI.SetActive(true);
    }
}
