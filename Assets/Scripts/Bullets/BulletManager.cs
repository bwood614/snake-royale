using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    
    public GameObject bulletPrefab;

    // state
    List<GameObject> bullets;
    List<Rigidbody2D> bulletRBs;

    void Start()
    {
        bullets = new List<GameObject>();
        bulletRBs = new List<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        bulletRBs.ForEach(bulletRB => {
            bulletRB.AddRelativeForce(new Vector2(0, 400));
        });
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FireBullet(Vector3 position, Quaternion rotation) {
        GameObject newBullet = Instantiate(bulletPrefab, position, rotation, transform);
        bullets.Add(newBullet);
        bulletRBs.Add(newBullet.GetComponent<Rigidbody2D>());
    }

    public void DestroyBullet(GameObject bulletToDestroy) {
        Destroy(bulletToDestroy);
        bullets.Remove(bulletToDestroy);
        bulletRBs.Remove(bulletToDestroy.GetComponent<Rigidbody2D>());
    }
}
