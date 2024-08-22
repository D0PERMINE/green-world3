using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    public TMP_Text textComponent;  // Das UI-Text-Element
    public string fullText;     // Der volle Text, der angezeigt werden soll
    public float delay = 0.05f; // Verzögerung zwischen den Zeichen

    private string currentText = ""; // Der aktuelle Text, der angezeigt wird
    public bool isTyping; 
    public bool textIsFinished;

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
