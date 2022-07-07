using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    [Header("STATS")]
    [SerializeField] private float rotationSpeed;
    [Tooltip("Maximum angles beetwen cannon and plane to shoot")]
    [SerializeField] private float minimumAimingPrecision = 5;

    [Tooltip("Shoots per minute")]
    [SerializeField] private float fireRate;

    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletAngleBias = 20f;
    [SerializeField] private float bulletFuseBias = 0.25f;

    [Header("GAME OBJECTS")]
    [SerializeField] private PlaneDetector planeDetector;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform _dynamic;

    [Header("temp")]
    [SerializeField] private TextMeshProUGUI distanceMeter;
    [SerializeField] private TextMeshProUGUI rotationMeter;

    private Transform currentTarget;
    private float secondsSinceLastShot;

    private const float SECONDS_IN_MINUTE = 60;

    
    void Update()
    {
        if (currentTarget == null)
        {
            if (!FindTarget())
            {
                return;
            }
        }

        var targetDistance = GetDistanceFromTarget();

        var aimInaccuracy = AimTurret();

        UpdateReloadTimer();

        if (CanShoot(aimInaccuracy))
        {
            Shoot(targetDistance);
        }
    }

    private bool FindTarget()
    {
        if (planeDetector.planes.Count != 0)
        {
            currentTarget = planeDetector.planes[0].transform;
            return true;
        }
        else
        {
            return false;
        }
    }

    private float GetDistanceFromTarget()
    {
        var distance = (currentTarget.position - transform.position).magnitude;
        distanceMeter.text = $"Distance {distance}";    //DEBUG TEMP
        return distance;
    }

    private float AimTurret()
    {
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, currentTarget.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        var anglesDifference = Quaternion.Angle(transform.rotation, toRotation);
        rotationMeter.text = $"Angles Difference {anglesDifference}"; //DEBUG TEMP

        return anglesDifference;
    }

    private void UpdateReloadTimer()
    {
        secondsSinceLastShot += Time.deltaTime;
    }

    private bool CanShoot(float aimInaccuracy)
    {
        return secondsSinceLastShot >= SECONDS_IN_MINUTE / fireRate && aimInaccuracy < minimumAimingPrecision;
    }

    private void Shoot(float targetDistance)
    {
        secondsSinceLastShot = 0;
        Quaternion biasedRotation = new Quaternion();
        biasedRotation.eulerAngles = transform.rotation.eulerAngles + Vector3.forward * Random.Range(-bulletAngleBias, bulletAngleBias);

        var distanceToSpawnFromCannon = transform.up.normalized * 0.1f;
        var obj = Instantiate(bullet, transform.position + distanceToSpawnFromCannon , biasedRotation , _dynamic);
        var biasedFuse = (targetDistance / bulletSpeed) * Random.Range(1f - bulletFuseBias / 2f, 1f + bulletFuseBias);
        obj.Shoot(bulletSpeed, biasedFuse);
    }
}
