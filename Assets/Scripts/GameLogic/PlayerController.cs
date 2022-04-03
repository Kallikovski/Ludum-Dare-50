using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private float mouseSensetivity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float killHeight;

    [SerializeField] private float gravity;
    [SerializeField] private float projectileSpeed;
    private GameObject projectile;

    //References
    private CharacterController controller;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawn;
    [SerializeField] private Transform projectileDirection;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip shoot;
    [SerializeField] private AudioClip teleport;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(GameManager.Instance.State == GameState.Running)
        {
            Move();
            Rotate();
            Action();
            CheckIsPlayerAlive();
        }
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);

        if (isGrounded)
        {
            if (velocity.y <= 0)
            {
                velocity.y = -2f;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }

        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        moveDirection = transform.TransformDirection(moveDirection);


        moveDirection *= moveSpeed;

        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensetivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }

    private void Action()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Teleport();
        }
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }

    private void Shoot()
    {
        if (projectile == null)
        {
            source.PlayOneShot(shoot);
            projectile = Instantiate(projectilePrefab, projectileSpawn.transform.position, Quaternion.identity);

            Projectile script = projectile.GetComponent<Projectile>();

            Vector3 projectileDirectionVec = (projectileDirection.position - projectileSpawn.position).normalized;
            script.Setup(projectileDirectionVec);
        }
    }
    private void Teleport()
    {
        if (projectile != null)
        {
            source.PlayOneShot(teleport);
            controller.enabled = false;
            transform.position = projectile.transform.position;
            controller.enabled = true;
            Destroy(projectile);
        }
    }
    
    private void CheckIsPlayerAlive()
    {
        if(gameObject.transform.position.y <= killHeight)
        {
            GameManager.Instance.UpdateGameState(GameState.GameOver);
            Destroy(gameObject);
        }
    }
}
