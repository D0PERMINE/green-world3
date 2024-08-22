using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private GameObject heldTrash = null;
    [SerializeField] private bool isDroppingTrash = false;
    [SerializeField] private TrashBin selectedTrashBin = null;

    [SerializeField] private Transform holdPosition;
    [SerializeField] private float holdDistance = 1f; // Distance in front of the character to hold the trash
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Vector2 lastMovementDirection = Vector2.up; // Default to up
    [SerializeField] private bool correctTrashType = false;
    [SerializeField] private bool onCollisionWithTashBin = false;
    //public TrashBin[] trashBins;
    [SerializeField] private int trashBinType;

    [SerializeField] private AudioSource pickUpAndDropItemAudio;

    // getter und setter
    public GameObject HeldTrash { get => heldTrash; set => heldTrash = value; }
    public TrashBin SelectedTrashBin { get => selectedTrashBin; set => selectedTrashBin = value; }
    public bool CorrectTrashType { get => correctTrashType; set => correctTrashType = value; }
    public bool OnCollisionWithTashBin { get => onCollisionWithTashBin; set => onCollisionWithTashBin = value; }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

    }

    void Update()
    {
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
        Vector2 movement = playerMovement.Movement;
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

}
