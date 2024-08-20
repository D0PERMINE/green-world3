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
            WaterTankHandler waterBarController = FindObjectOfType<WaterTankHandler>();
            if (waterBarController != null)
            {
                waterBarController.AddWater();
            }
            else
            {
                Debug.LogError("WaterTankHandler nicht gefunden!");
            }

            // Zerstöre das WaterBottle-Objekt nach dem Einsammeln
            Destroy(gameObject);
        }
    }
}
