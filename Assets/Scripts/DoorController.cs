using System;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private float moveDistance; // Die Entfernung, die die Tür nach oben bewegt werden soll
    [SerializeField] private float moveSpeed; // Die Geschwindigkeit, mit der die Tür sich bewegt

    private bool isOpening = false; // Zustand, ob die Tür sich öffnet oder nicht
    private Vector3 initialPosition; // Die ursprüngliche Position der Tür
    private Vector3 targetPosition; // Die Zielposition der Tür
    public TrashBinManager trashBinManager;

    void Start()
    {
        // Speichern Sie die ursprüngliche Position der Tür
        initialPosition = transform.position;
        // Berechnen Sie die Zielposition
        targetPosition = new Vector3(initialPosition.x, initialPosition.y + moveDistance, initialPosition.z);
    }

    void Update()
    {
        // Überprüfen Sie, ob die Leertaste gedrückt wurde
        if (trashBinManager.GetAllTrashCollected())
        {
            isOpening = true; // Setzen Sie den Zustand auf Öffnen
        }

        // Bewegen Sie die Tür nach oben, wenn isOpening true ist
        if (isOpening)
        {
            // Bewegen Sie die Tür schrittweise zur Zielposition
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Überprüfen Sie, ob die Tür die Zielposition erreicht hat
            if (transform.position == targetPosition)
            {
                isOpening = false; // Beenden Sie die Bewegung
            }
        }
    }
}