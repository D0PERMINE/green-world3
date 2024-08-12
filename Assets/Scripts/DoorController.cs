using System;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private float moveDistance; // Die Entfernung, die die T�r nach oben bewegt werden soll
    [SerializeField] private float moveSpeed; // Die Geschwindigkeit, mit der die T�r sich bewegt

    private bool isOpening = false; // Zustand, ob die T�r sich �ffnet oder nicht
    private Vector3 initialPosition; // Die urspr�ngliche Position der T�r
    private Vector3 targetPosition; // Die Zielposition der T�r
    public TrashBinManager trashBinManager;

    void Start()
    {
        // Speichern Sie die urspr�ngliche Position der T�r
        initialPosition = transform.position;
        // Berechnen Sie die Zielposition
        targetPosition = new Vector3(initialPosition.x, initialPosition.y + moveDistance, initialPosition.z);
    }

    public void OpenDoor()
    {
        // �berpr�fen Sie, ob die Leertaste gedr�ckt wurde
        if (trashBinManager.GetAllTrashCollected())
        {
            isOpening = true; // Setzen Sie den Zustand auf �ffnen
        }

        // Bewegen Sie die T�r nach oben, wenn isOpening true ist
        if (isOpening)
        {
            // Bewegen Sie die T�r schrittweise zur Zielposition
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // �berpr�fen Sie, ob die T�r die Zielposition erreicht hat
            if (transform.position == targetPosition)
            {
                isOpening = false; // Beenden Sie die Bewegung
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.gameObject.tag == "Player")
        {
            FirstLevelGameManager.Instance.EndFirstLevel();
        }
    }
}