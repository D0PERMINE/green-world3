using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //public float moveSpeed = 5f;
    //private Rigidbody2D rb;
    //private Vector2 movement;
    public GameObject heldTrash = null;

    public Transform holdPosition;
    public float holdDistance = 1f; // Distance in front of the character to hold the trash
    private PlayerMovement playerMovement;
    private Vector2 lastMovementDirection = Vector2.up; // Default to up
    public bool correctTrashType = false;
    public bool onCollisionWithTashBin = false;

    public AudioSource footstepAudio;
    public AudioSource pickUpAndDropItemAudio;

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();

        if (footstepAudio == null)
        {
            footstepAudio = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        //movement.x = Input.GetAxis("Horizontal");
        //movement.y = Input.GetAxis("Vertical");

        HoldDistance();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (heldTrash == null)
            {
                PickupTrash();
            }
            else
            {
                DropTrash();
            }
        }

        FoodStepAudioOn();
    }

    void FixedUpdate()
    {
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void PickupTrash()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Trash"))
            {
                heldTrash = collider.gameObject;
                var trashRb = heldTrash.GetComponent<Rigidbody2D>();
                if (trashRb != null)
                {
                    trashRb.isKinematic = true;
                }
                pickUpAndDropItemAudio.Play();
                break;
            }
        }
    }

    void DropTrash()
    {
        if (heldTrash != null)
        {
            var trashRb = heldTrash.GetComponent<Rigidbody2D>();
            if (trashRb != null)
            {
                trashRb.isKinematic = false;
            }
            if (correctTrashType && onCollisionWithTashBin)
            {
                // Punkte hinzuf�gen
                ScoreManager.Instance.AddScore(10);
                Destroy(heldTrash);
            } else if (!correctTrashType && onCollisionWithTashBin)
            {
                // Minuspunkte hinzuf�gen
                ScoreManager.Instance.AddScore(-5);
                Destroy(heldTrash);
            }
            pickUpAndDropItemAudio.Play();
            heldTrash = null;
        }
    }

    private void CalculateHoldDistance()
    {
        Vector2 movement = playerMovement.movement;
        if (movement != Vector2.zero)
        {
            lastMovementDirection = movement.normalized;

            if (lastMovementDirection.x < 0)
            {
                holdDistance = 1.0f;
                holdDistance = 0.5f;
            }
            else if (lastMovementDirection.x > 0)
            {
                holdDistance = 1.0f;
                holdDistance = 0.5f;
            }
            else if (lastMovementDirection.y < 0)
            {
                holdDistance = 1.7f;
                holdDistance = -0.2f;
            }
            else if (lastMovementDirection.y > 0)
            {
                holdDistance = 1.2f;
                holdDistance = 0.7f;
            }

        }
    }

    private void HoldDistance()
    {
        CalculateHoldDistance();

        if (heldTrash != null)
        {
            Vector2 holdPositionOffset = lastMovementDirection * holdDistance;
            heldTrash.transform.position = (Vector2)transform.position + holdPositionOffset;
        }
    }

    private bool IsCharacterMoving()
    {
        return Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
    }

    private void FoodStepAudioOn()
    {
        // �berpr�fe, ob der Charakter sich bewegt
        if (IsCharacterMoving())
        {
            footstepAudio.enabled = true;
        }
        else
        {
            footstepAudio.enabled = false;
        }
    }

}
