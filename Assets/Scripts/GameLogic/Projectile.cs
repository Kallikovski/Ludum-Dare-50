using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 moveDirection;
    private float lifeTime = 4f;
    private float projectileSpeed = 35f;
    private int maxBounce = 5;
    private int currentBounce;
    private Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = moveDirection * projectileSpeed;
    }
    public void Setup(Vector3 moveDirection)
    {
        currentBounce = 0;
        this.moveDirection = moveDirection;
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentBounce++;
        if (currentBounce == maxBounce)
        {
            Destroy(gameObject);
        }
    }
}
