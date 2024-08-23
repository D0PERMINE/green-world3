using System;
using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private float moveDistance; // Die Entfernung, die die T�r nach oben bewegt werden soll
    [SerializeField] private float moveSpeed; // Die Geschwindigkeit, mit der die T�r sich bewegt

    [SerializeField] bool isOpening = false; // Zustand, ob die T�r sich �ffnet oder nicht
    private Vector3 initialPosition; // Die urspr�ngliche Position der T�r
    [SerializeField] Transform targetPosition; // Die Zielposition der T�r
    //[SerializeField] private Vector3 targetPosition;
    [SerializeField] private bool doorIsOpen;
    [SerializeField] private CameraController cameraController;

    //getter und setter
    public bool DoorIsOpen { get => doorIsOpen; set => doorIsOpen = value; }

    void Start()
    {
        // Speichern Sie die urspr�ngliche Position der T�r
        initialPosition = transform.position;
        // Berechnen Sie die Zielposition
        //targetPosition = new Vector3(initialPosition.x, initialPosition.y + moveDistance, initialPosition.z);
    }

    void Update()
    {
        if (FirstLevelGameManager.Instance.wasFirstQuestSolved)
        {
            
            GameStateHandler.Instance.GameState = GameState.cameraTransition;
            StartCoroutine(OpenDoorSequence());
        }
    }
    IEnumerator OpenDoorSequence()
    {
        
        cameraController.TriggerDoorZoomIn();
        yield return new WaitForSeconds(3f);

        // Bewege die T�r von der aktuellen Position zur Zielposition
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);
        FirstLevelGameManager.Instance.wasFirstQuestSolved = false;

        yield return new WaitForSeconds(3f);
        cameraController.TriggerDoorZoomOut();
        Debug.Log("game state: " + GameStateHandler.Instance.GameState);
        yield return new WaitForSeconds(2f);
        GameStateHandler.Instance.GameState = GameState.game;
        Debug.Log("game state2: " + GameStateHandler.Instance.GameState);
        // �berpr�fe, ob die T�r die Zielposition erreicht hat
        //if (transform.position == targetPosition.position)
        //{
        //    doorIsOpen = true;
        //    isOpening = false; // Bewegung stoppen
        //    Debug.Log("Door has fully opened.");
        //}
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FirstLevelGameManager.Instance.EndFirstLevel();
        }
    }
}