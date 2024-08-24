using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    [SerializeField] private TMP_Text textComponent;  // Das UI-Text-Element
    [SerializeField] private string fullText;     // Der volle Text, der angezeigt werden soll
    [SerializeField] private float delay = 0.05f; // Verzögerung zwischen den Zeichen

    [SerializeField] private string currentText = ""; // Der aktuelle Text, der angezeigt wird
    [SerializeField] private bool isTyping;
    [SerializeField] private bool textIsFinished;

    // getter und setter
    public bool IsTyping { get => isTyping; set => isTyping = value; }
    public bool TextIsFinished { get => textIsFinished; set => textIsFinished = value; }

    void Start()
    {
        fullText = textComponent.text;
        //StartCoroutine(TypeText());
    }

    public IEnumerator TypeText()
    {
        isTyping = true;
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textComponent.text = currentText;
            yield return new WaitForSeconds(delay);
        }
        textIsFinished = true;
        Debug.Log("Text finishedDDDDDDDDDDDD");
    }

   

}
