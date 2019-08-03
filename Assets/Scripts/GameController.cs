using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text scoreText;
    public Text winText;
    public Text livesText;
    public Text loseText;

    private int score;
    private int lives;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Coins: 0 :(";
        winText.text = "";
        livesText.text = "Lives: 3";
        score = 0;
        lives = 3;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void incrementScore()
    {
        score++;
        scoreText.text = "Coins: " + score.ToString();

        if(SceneManager.GetActiveScene().buildIndex == 0 && score >= 4)
        {
            SceneManager.LoadScene(1);
        }
        else if(SceneManager.GetActiveScene().buildIndex == 1 && score >= 4)
        {
            winText.text = "You won!";
            GetComponent<AudioSource>().Play();
        }
    }

    public void DecrementLives()
    {
        lives--;
        livesText.text = "Lives: " + lives.ToString();

        if(lives <= 0)
        {
            loseText.text = "You lose :( get better soon";
            Destroy(GameObject.Find("Player"));
        }
    }
}
