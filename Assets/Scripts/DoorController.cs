using System;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private float moveDistance; // Die Entfernung, die die Tür nach oben bewegt werden soll
    [SerializeField] private float moveSpeed; // Die Geschwindigkeit, mit der die Tür sich bewegt

    [SerializeField] bool isOpening = false; // Zustand, ob die Tür sich öffnet oder nicht
    private Vector3 initialPosition; // Die ursprüngliche Position der Tür
    [SerializeField] Transform targetPosition; // Die Zielposition der Tür

    void Start()
    {
        // Speichern Sie die ursprüngliche Position der Tür
        //  initialPosition = transform.position;
        // Berechnen Sie die Zielposition
        //  targetPosition = new Vector3(initialPosition.x, initialPosition.y + moveDistance, initialPosition.z);
    }

    private void Update()
    {
        if (FirstLevelGameManager.Instance.wasFirstQuestSolved)
        {
            OpenDoor();
        }
    }
    public void OpenDoor()
    {
        if (transform.position.y < targetPosition.transform.position.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.transform.position, moveSpeed * Time.deltaTime);

        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FirstLevelGameManager.Instance.EndFirstLevel();
        }
    }
}