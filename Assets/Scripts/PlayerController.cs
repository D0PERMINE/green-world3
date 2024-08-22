using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public GameObject heldTrash = null;
    public bool isDroppingTrash = false;
    public TrashBin selectedTrashBin = null;

    public Transform holdPosition;
    public float holdDistance = 1f; // Distance in front of the character to hold the trash
    private PlayerMovement playerMovement;
    private Vector2 lastMovementDirection = Vector2.up; // Default to up
    public bool correctTrashType = false;
    public bool onCollisionWithTashBin = false;
    //public TrashBin[] trashBins;
    private int trashBinType;

    public AudioSource pickUpAndDropItemAudio;


    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

    }

    void Update()
    {
        //movement.x = Input.GetAxis("Horizontal");
        //movement.y = Input.GetAxis("Vertical");
        if(GameStateHandler.Instance.GameState != GameState.game) { return; }

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
            if (selectedTrashBin != null && heldTrash != null) { selectedTrashBin.glowEffect.TriggerGlow(selectedTrashBin.glowEffect.maxGlowStrength); }
            var trashRb = heldTrash.GetComponent<Rigidbody2D>();
            if (trashRb != null)
            {
                trashRb.isKinematic = false;
            }
            if (correctTrashType && onCollisionWithTashBin)
            {
                // Punkte hinzufügen
                FirstLevelGameManager.Instance.UpdateScrore(true);
                Debug.Log("Add points");
                Destroy(heldTrash);

            } else if (!correctTrashType && onCollisionWithTashBin)
            {
                // Minuspunkte hinzufügen
                FirstLevelGameManager.Instance.UpdateScrore(false);
                Debug.Log("Subtract points");
                Destroy(heldTrash);
            }
            pickUpAndDropItemAudio.Play();
            heldTrash = null;
            FirstLevelGameManager.Instance.UpdateCollectedTrash();
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

    //public void SetTrashBinType(string trashBinType)
    //{
    //    if (trashBinType == "Rest")
    //    {
    //        this.trashBinType = 0;
    //    } 
    //    else if (trashBinType == "Plastik")
    //    {
    //        this.trashBinType = 1;
    //    }
    //    else if (trashBinType == "Bio")
    //    {
    //        this.trashBinType = 2;
    //    }
    //}

}
