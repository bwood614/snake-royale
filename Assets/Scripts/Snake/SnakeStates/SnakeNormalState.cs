using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random=UnityEngine.Random;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;

public class SnakeNormalState : SnakeBaseState
{
    public float moveSpeed = 200;

    IController controller;
    SnakeGrowthManager snakeGrowthManager;

    NuggetManager nuggetManager;
    BulletManager bulletManager;

    Rigidbody2D snakeRB;

    // state

    public override void EnterState(SnakeStateManager snake)
    {
        controller = snake.GetComponent<IController>();
        controller.Initialize();

        snakeGrowthManager = snake.GetComponent<SnakeGrowthManager>();
        snakeGrowthManager.UpdateMarkerFrequency(.005f);

        GameObject nuggetManagerObj = GameObject.FindGameObjectWithTag("Nugget Manager");
        nuggetManager =  nuggetManagerObj.GetComponent<NuggetManager>();

        GameObject bulletManagerObj = GameObject.FindGameObjectWithTag("Bullet Manager");
        bulletManager =  bulletManagerObj.GetComponent<BulletManager>();

        snakeRB = snake.GetComponent<Rigidbody2D>();
    }

    public override void FixedUpdate(SnakeStateManager snake)
    {
        snakeRB.AddRelativeForce(new Vector2(0, moveSpeed));
    }

    public override void OnCollisionEnter(SnakeStateManager snake, Collision2D collisionInfo)
    {
    }

    public override void OnCollisionExit(SnakeStateManager snake, Collision2D collisionInfo)
    {
    }

    public override void OnTriggerEnter(SnakeStateManager snake, Collider2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Body Segment" && collisionInfo.gameObject.transform.parent != snake.transform.parent) {
            //snake.SwitchState(snake.snakeDeadState);
            snakeGrowthManager.DescreaseLength();
        }
    }

    public override void UpdateState(SnakeStateManager snake)
    {
        controller.UpdateRotation(snake);
        controller.UpdateBoost(snake);

        if (controller.CheckForShoot()) {
            bulletManager.FireBullet(snake.transform.position, snake.transform.rotation);
        }
    }

}
