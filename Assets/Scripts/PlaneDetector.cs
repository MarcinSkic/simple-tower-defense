using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDetector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private float turretRange;


    public List<Collider2D> planes = new List<Collider2D>();
    void Start()
    {
        circleCollider.radius = turretRange;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<PlaneController>(out var controller))
        {
            if (!planes.Contains(other))
            {
                planes.Add(other);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<PlaneController>(out var controller))
        {
            if (planes.Contains(other))
            {
                planes.Remove(other);
            }
        }
    }
}
