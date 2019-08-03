using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<AudioSource>().Play();
        Destroy(gameObject);
        gameController.incrementScore();
    }
}
