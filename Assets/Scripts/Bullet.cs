using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float FORCE_TO_SPEED_RATIO = 50;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer bulletSprite;
    private float speed;
    public void Shoot(float bulletSpeed,float fuse)
    {
        rb.AddRelativeForce(Vector2.up*bulletSpeed * FORCE_TO_SPEED_RATIO);
        StartCoroutine(TimeToExplosion(fuse));
    }

    private void Update()
    {
        speed = rb.velocity.magnitude;
    }

    IEnumerator TimeToExplosion(float fuse)
    {
        yield return new WaitForSeconds(fuse);
        Explode();
    }

    private void Explode()
    {
        rb.velocity = Vector2.zero;
        bulletSprite.enabled = false;
        animator.SetTrigger("Explode");
    }
}
