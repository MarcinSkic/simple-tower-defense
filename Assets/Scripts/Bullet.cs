using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float FORCE_TO_SPEED_RATIO = 50;
    [SerializeField] private Rigidbody2D rb; 
    private float speed;
    public void Shoot(float bulletSpeed,float fuse)
    {
        rb.AddRelativeForce(Vector2.up*bulletSpeed);
        StartCoroutine(TimeToExplosion(fuse * FORCE_TO_SPEED_RATIO));
    }

    private void Update()
    {
        speed = rb.velocity.magnitude;
    }

    IEnumerator TimeToExplosion(float fuse)
    {
        yield return new WaitForSeconds(fuse);
        Destroy(this.gameObject);
    }
}
