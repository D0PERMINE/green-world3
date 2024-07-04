using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Finde den WaterBarController in der Szene und rufe AddWater() auf
            WaterBarController waterBarController = FindObjectOfType<WaterBarController>();
            if (waterBarController != null)
            {
                waterBarController.AddWater(30f);
            }
            else
            {
                Debug.LogError("WaterBarController nicht gefunden!");
            }

            // Zerstöre das WaterBottle-Objekt nach dem Einsammeln
            Destroy(gameObject);
        }
    }
}
