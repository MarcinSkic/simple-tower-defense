using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;

    [SerializeField] private Animator animator;
    [SerializeField] private CircleCollider2D circleCollider2D;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer bulletSprite;

    private float speed;

    private const float FORCE_TO_SPEED_RATIO = 50;
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

        FindPlanesInExplosionRadius();
    }

    private void FindPlanesInExplosionRadius()
    {
        List<Collider2D> results = new List<Collider2D>();
        circleCollider2D.OverlapCollider(new ContactFilter2D().NoFilter(), results);

        results.ForEach(collider =>
        {
            if (collider.TryGetComponent<PlaneController>(out var controller))
            {
                controller.DealDamage(damage);
            }
        });

        Debug.Log("--------------- BULLET ----------------");
        results.ForEach(collider =>
        {
            Debug.Log(collider);
        });
    }
}
