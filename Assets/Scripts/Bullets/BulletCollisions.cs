using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisions : MonoBehaviour
{
    BulletManager bulletManager;
    NuggetManager nuggetManager;

    void Start()
    {
        GameObject bulletManagerObj = GameObject.FindGameObjectWithTag("Bullet Manager");
        bulletManager =  bulletManagerObj.GetComponent<BulletManager>();

        GameObject nuggetManagerObj = GameObject.FindGameObjectWithTag("Nugget Manager");
        nuggetManager =  nuggetManagerObj.GetComponent<NuggetManager>();
    }
    void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Wall") {
            bulletManager.DestroyBullet(gameObject);
        } if (collisionInfo.gameObject.tag == "Body Segment") {
            bulletManager.DestroyBullet(gameObject);

            // get snake head growth manager
            SnakeGrowthManager snakeGrowthManager = collisionInfo.gameObject.transform.parent.GetChild(0).GetComponent<SnakeGrowthManager>();
            snakeGrowthManager.DescreaseLength();
        }
    }
}
