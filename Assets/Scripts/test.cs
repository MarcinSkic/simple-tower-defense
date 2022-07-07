using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public void OnExplosionFinished()
    {
        Destroy(transform.parent.gameObject);
    }
}
