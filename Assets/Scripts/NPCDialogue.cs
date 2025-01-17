using System.Collections;
using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] GameObject questionSymbol;
    [SerializeField] private TMP_Text npcNameText; // TextMeshPro Text-Objekt f�r den NPC-Namen
    [SerializeField] private TMP_Text dialogueText; // TextMeshPro Text-Objekt f�r die Anzeige des Dialogtextes
    [SerializeField] private GameObject dialogueBox; // Das UI-Element, das den Dialogtext enth�lt
    [SerializeField] private TrashSpawner trashSpawner; // Referenz auf den TrashSpawner

    [SerializeField] private string npcName; // Der Name des NPCs
    [SerializeField] private string[] initialDialogueLines; // Zeilen des ersten Dialogs
    [SerializeField] private string[] followUpDialogueLines; // Zeilen des Folge-Dialogs

    private int currentLineIndex = 0;
    private bool playerInRange = false;
    private bool isDialogueActive = false;
    private bool isTyping = false; // Um zu �berpr�fen, ob derzeit getippt wird
    private Coroutine typingCoroutine; // Zum Verwalten der Coroutine
    private bool hasCompletedFirstDialogue = false; // Status, ob der erste Dialog abgeschlossen wurde

    [SerializeField] private PlayerMovement playerMovement; // Referenz zum playerMovement

    [SerializeField] WaterTankHandler waterTankHandler;
    [SerializeField] private CameraController cameraController;

    // getter und setter
    public bool IsDialogueActive { get => isDialogueActive; set => isDialogueActive = value; }

    void Start()
    {
        dialogueText.text = "";
        npcNameText.text = "";
        dialogueBox.SetActive(false);
        questionSymbol.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            if (!isDialogueActive)
            {
                StartDialogue();
            }
            else if (isTyping)
            {
                // Vervollst�ndige die aktuelle Zeile sofort
                CompleteLine();
            }
            else
            {
                DisplayNextLine();
            }
            cameraController.TriggerNPCDialigZoom();
        }

        NoMovingDuringDialog();
    }

    private void NoMovingDuringDialog()
    {
        if (isDialogueActive)
        {
            playerMovement.MovementEnabled(false);
        }
        else
        {
            playerMovement.MovementEnabled(true);
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueBox.SetActive(true);
        playerMovement.SetMovementInputToZero();

        // Setze den Namen des NPCs sofort
        npcNameText.text = npcName;

        if (hasCompletedFirstDialogue)
        {
            // Zeige den Folge-Dialog
            currentLineIndex = 0;
            typingCoroutine = StartCoroutine(TypeLine(followUpDialogueLines));
        }
        else
        {
            // Zeige den ersten Dialog
            currentLineIndex = 0;
            typingCoroutine = StartCoroutine(TypeLine(initialDialogueLines));
        }
    }

    IEnumerator TypeLine(string[] lines)
    {
        isTyping = true;
        string currentLine = lines[currentLineIndex];
        dialogueText.text = "";

        foreach (char letter in currentLine.ToCharArray())
        {
            Debug.Log("lol: " + letter);
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // Wartezeit zwischen den Buchstaben
        }

        isTyping = false;
    }

    void CompleteLine()
    {
        if (typingCoroutine != null && !hasCompletedFirstDialogue)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = initialDialogueLines[currentLineIndex];
            isTyping = false;
        } else if (typingCoroutine != null && hasCompletedFirstDialogue)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = followUpDialogueLines[currentLineIndex];
            isTyping = false;
        }
    }

    void DisplayNextLine()
    {
        if (hasCompletedFirstDialogue && currentLineIndex < followUpDialogueLines.Length - 1)
        {
            currentLineIndex++;
            typingCoroutine = StartCoroutine(TypeLine(followUpDialogueLines));
        }
        else if (!hasCompletedFirstDialogue && currentLineIndex < initialDialogueLines.Length - 1)
        {
            currentLineIndex++;
            typingCoroutine = StartCoroutine(TypeLine(initialDialogueLines));
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialogueBox.SetActive(false);
        CompleteLine();

        if (!hasCompletedFirstDialogue)
        {
            hasCompletedFirstDialogue = true;
            if (trashSpawner != null)
            {
                trashSpawner.EnableSpawning(); 
                waterTankHandler.StartWaterTimer();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            questionSymbol.SetActive(true);
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            questionSymbol.SetActive(false);
            playerInRange = false;
        }
    }
}