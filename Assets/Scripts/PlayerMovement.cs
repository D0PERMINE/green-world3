using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [SerializeField] private Vector2 movement;
    [SerializeField] private AudioSource footstepAudio;

    private bool canMove; // Steuerung der Bewegungsfähigkeit

    // getter und setter
    public Vector2 Movement { get => movement; set => movement = value; }

    private void Start()
    {
        if (footstepAudio == null)
        {
            footstepAudio = GetComponent<AudioSource>();
        }

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateHandler.Instance.GameState != GameState.game) { return; }
        Move();
        FoodStepAudioOn();

    }

    public void Move()
    {
        if (canMove)
        {
            // input
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            SafeLastMovementDirection();
        }
        
    }

    private void SafeLastMovementDirection()
    {
        // Letzte Bewegungsrichtung speichern für Idle Animation
        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("LastHorizontal", movement.x);
            animator.SetFloat("LastVertical", movement.y);
        }
    }

    private void FixedUpdate()
    {
        if (GameStateHandler.Instance.GameState != GameState.game) { SetMovementInputToZero(); return; }
        // movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private bool IsCharacterMoving()
    {
        return Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
    }

    private void FoodStepAudioOn()
    {
        // Überprüfe, ob der Charakter sich bewegt
        if (IsCharacterMoving() && canMove)
        {
            footstepAudio.enabled = true;
        }
        else
        {
            footstepAudio.enabled = false;
        }
    }

    public void MovementEnabled(bool enabled)
    {
        canMove = enabled;
    }


    public void SetMovementInputToZero()
    {
        movement.x = 0;
        movement.y = 0;

        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", 0);
        animator.SetFloat("Speed", 0);
    }
}
