using UnityEngine;


public enum TrashType
{
    residual,
    plastic,
    bio,
    paper
}
public class TrashBin : MonoBehaviour
{
   // public string binType;
    public TrashType trashType;
    public PlayerController playerController;
    public GlowEffect glowEffect;
    private bool trashDropped;
    public TrashBinManager trashBinManager;
    

    private void Start()
    {
       // playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        glowEffect.TriggerGlow(glowEffect.glowStrengthOnSelection);
    }
    public void SetTrashDropped(bool trashDropped)
    {
        this.trashDropped = trashDropped;
        trashBinManager.SetCollectedTrash();
        Debug.Log("collected trash: " + trashBinManager.GetCollectedTrash());
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController.HeldTrash != null)
            {
                TrashType heldTrashType = playerController.HeldTrash.GetComponent<Trash>().trash;

                if (heldTrashType == trashType)
                {
                    Debug.Log("true");
                    playerController.CorrectTrashType = true;
                }
                else
                {
                    Debug.Log("false");
                    playerController.CorrectTrashType = false;
                }

               
                playerController.OnCollisionWithTashBin = true;
                playerController.SelectedTrashBin = this;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            playerController.OnCollisionWithTashBin = false;
            playerController.SelectedTrashBin = null;
   
        }
    }

     
        
        //if (playerController.heldTrash != null)
        //{ }
        //Debug.Log("1");
        //if (other.CompareTag("Trash"))
        //{
        //    Debug.Log("2");
        //    var trashType = other.GetComponent<Trash>().trashType;
        //    var binType = this.gameObject.GetComponent<TrashBin>().binType;

        //    if (binType == trashType)
        //    {
        //        Debug.Log("true");
        //        playerController.correctTrashType = true;
        //    }
        //    else
        //    {
        //        Debug.Log("false");
        //        playerController.correctTrashType = false;
        //    }
        //    playerController.SetTrashBinType(binType);

        //    playerController.onCollisionWithTashBin = true;
        //}
        // }
        // }


}