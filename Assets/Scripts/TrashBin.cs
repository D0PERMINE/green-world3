using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public string binType;
    public PlayerController playerController;

    private void Start()
    {
         playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerController.heldTrash != null)
        {
            Debug.Log("1");
            if (other.CompareTag("Trash"))
            {
                Debug.Log("2");
                var trashType = other.GetComponent<Trash>().trashType;
                var binType = this.gameObject.GetComponent<TrashBin>().binType;

                if (binType == trashType)
                {
                    Debug.Log("true");
                    playerController.correctTrashType = true;
                }
                else
                {
                    Debug.Log("false");
                    playerController.correctTrashType = false;
                }

                playerController.onCollisionWithTashBin = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerController.onCollisionWithTashBin = false;
    }
}