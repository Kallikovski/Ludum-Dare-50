using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] private float baseMovementSpeed;
    public float movementSpeed;
    private Vector3 movementDirection;

    private void Start()
    {
        movementDirection = new Vector3(0, 0, 1);
        movementSpeed = baseMovementSpeed - GameManager.Instance.GameScore / 1000;
    }

    private void Update()
    {
        
        transform.position += movementDirection *movementSpeed * Time.deltaTime;
        IncreaseSpeed();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Garbage"))
        {
            Destroy(gameObject);
        }
    }
    private void IncreaseSpeed()
    {
        if((int)GameManager.Instance.GameScore%100 == 0)
        {
            movementSpeed -= 0.2f;
        }
    }
}
