using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{


    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    [SerializeField] private HealthSlider healthSlider;

    private void Start()
    {
        health = maxHealth;
        healthSlider.Init(maxHealth, maxHealth);
    }

    void Update()
    {
        transform.Translate(new Vector2(0, 1) * speed * Time.deltaTime);
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        healthSlider.ChangeValue(health);
    }
}
