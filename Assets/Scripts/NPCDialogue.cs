using System.Collections;
using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public TMP_Text npcNameText; // TextMeshPro Text-Objekt für den NPC-Namen
    public TMP_Text dialogueText; // TextMeshPro Text-Objekt für die Anzeige des Dialogtextes
    public GameObject dialogueBox; // Das UI-Element, das den Dialogtext enthält
    public TrashSpawner trashSpawner; // Referenz auf den TrashSpawner

    public string npcName; // Der Name des NPCs
    public string[] initialDialogueLines; // Zeilen des ersten Dialogs
    public string[] followUpDialogueLines; // Zeilen des Folge-Dialogs

    private int currentLineIndex = 0;
    private bool playerInRange = false;
    private bool isDialogueActive = false;
    private bool isTyping = false; // Um zu überprüfen, ob derzeit getippt wird
    private Coroutine typingCoroutine; // Zum Verwalten der Coroutine
    private bool hasCompletedFirstDialogue = false; // Status, ob der erste Dialog abgeschlossen wurde

    public PlayerMovement playerMovement; // Referenz zum playerMovement


    void Start()
    {
        dialogueText.text = "";
        npcNameText.text = "";
        dialogueBox.SetActive(false);
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
                // Vervollständige die aktuelle Zeile sofort
                CompleteLine();
            }
            else
            {
                DisplayNextLine();
            }
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
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}