using System;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private float moveDistance; // Die Entfernung, die die T�r nach oben bewegt werden soll
    [SerializeField] private float moveSpeed; // Die Geschwindigkeit, mit der die T�r sich bewegt

    [SerializeField] bool isOpening = false; // Zustand, ob die T�r sich �ffnet oder nicht
    private Vector3 initialPosition; // Die urspr�ngliche Position der T�r
    [SerializeField] Transform targetPosition; // Die Zielposition der T�r

    void Start()
    {
        // Speichern Sie die urspr�ngliche Position der T�r
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