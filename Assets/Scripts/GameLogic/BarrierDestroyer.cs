using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        Destroy(other);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.gameObject);
        Destroy(collision.collider.gameObject);
        Destroy(collision.collider);
    }
}
