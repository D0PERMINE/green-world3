using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public WaterBarController waterBarController;

    // Optional: Referenz zum GameManager oder anderen benötigten Skripten
    // public GameManager gameManager;

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    // Überprüfen, ob der Spieler mit der Wasserflasche kollidiert ist
    //    if (other.gameObject.CompareTag("WaterBottle"))
    //    {
    //        Debug.Log("YOOOOOOOOOOO");
    //        Destroy(other.gameObject);
    //        waterBarController.AddWater(30f);

    //        //// Wasserflasche einsammeln
    //        //CollectibleItem collectible = other.GetComponent<CollectibleItem>();
    //        //if (collectible != null)
    //        //{
    //        //    collectible.Collect(); // Diese Methode ruft die Collect-Methode im CollectibleItem-Skript auf
    //        //}
    //        //else
    //        //{
    //        //    Debug.LogError("CollectibleItem-Skript nicht gefunden auf " + other.name);
    //        //}
    //    }
    //}
}
