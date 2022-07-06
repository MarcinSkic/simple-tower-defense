using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    [Tooltip("Shoots per minute")]
    [SerializeField] float fireRate;
    [SerializeField] float bulletSpeed;

    [Tooltip("Maximum angles beetwen cannon and plane to shoot")]
    [SerializeField] float minimumAimingPrecision = 5;

    [SerializeField] Bullet bullet;
    [SerializeField] Transform _dynamic;

    [Header("temp")]
    [SerializeField] private Transform plane;

    [SerializeField] private TextMeshProUGUI distanceMeter;
    [SerializeField] private TextMeshProUGUI rotationMeter;


    private float secondsSinceLastShot;

    void Update()
    {
        var distance = (plane.position - transform.position).magnitude;
        distanceMeter.text = $"Distance {distance}";

        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, plane.position - transform.position);

        var anglesDifference = Quaternion.Angle(transform.rotation, toRotation);
        rotationMeter.text = $"Angles Difference {anglesDifference}";

        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        secondsSinceLastShot += Time.deltaTime;
        if (secondsSinceLastShot >= 60 / fireRate)
        {
            if(anglesDifference < minimumAimingPrecision)
            {
                secondsSinceLastShot = 0;
                var obj = Instantiate(bullet, transform.transform.position, transform.rotation, _dynamic);
                obj.Shoot(bulletSpeed,distance/bulletSpeed);
            }
            
        }
    }
}
