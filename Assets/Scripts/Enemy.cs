using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameController gameController;
    Transform enemyTransform;
    Vector2 bottomY;
    Vector2 topY;
    bool moveDown;
    float moveTimer;

    public float moveLength;
    public int yDelta;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        enemyTransform = GetComponent<Transform>();
        bottomY = new Vector2(enemyTransform.position.x, enemyTransform.position.y);
        topY = new Vector2(enemyTransform.position.x, enemyTransform.position.y + yDelta);
        moveTimer = 0.0f;
    }

    void Update()
    {
        if (moveDown)
        {
            //move Down
            transform.position = Vector2.Lerp(topY, bottomY, (moveTimer) / moveLength);
            moveTimer += Time.deltaTime;
        }
        else
        {
            //move Up
            transform.position = Vector2.Lerp(bottomY, topY, (moveTimer) / moveLength);
            moveTimer += Time.deltaTime;
        }

        if(enemyTransform.position.y >= topY.y && !moveDown)
        {
            moveDown = true;
            moveTimer = 0.0f;
        }
        else if(enemyTransform.position.y <= bottomY.y && moveDown)
        {
            moveDown = false;
            moveTimer = 0.0f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        FindObjectOfType<PlayerController>().ResetJump();
        gameController.DecrementLives();
    }
}
