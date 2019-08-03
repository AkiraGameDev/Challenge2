using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class deathbox : MonoBehaviour
{
    GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        SceneManager.LoadScene(1);
    }
}
