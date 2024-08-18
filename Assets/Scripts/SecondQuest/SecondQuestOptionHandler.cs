using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SecondQuestOptionHandler : MonoBehaviour
{ 
    [SerializeField]
    private int optionId = 0;
    [SerializeField] GameObject text;

    private void Start()
    {
        SetText(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            Debug.Log("Select option" + optionId);
            QuizUIManager.Instance.SetCurrentSelectedOption(this);
            SetText(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            QuizUIManager.Instance.SetCurrentSelectedOption(null);
            SetText(false);
        }
    }

    public int GetOptionId()
    {
        return optionId;
    }

    void SetText(bool isSelected)
    {
        if (isSelected)
        {
            text.GetComponent<TMP_Text>().fontStyle = FontStyles.Bold;
            text.GetComponent<TMP_Text>().color = new Color(text.GetComponent<TMP_Text>().color.r, text.GetComponent<TMP_Text>().color.g, text.GetComponent<TMP_Text>().color.b, 1);
        }
        else 
        {
            text.GetComponent<TMP_Text>().fontStyle = FontStyles.Normal;
            text.GetComponent<TMP_Text>().color = new Color(text.GetComponent<TMP_Text>().color.r, text.GetComponent<TMP_Text>().color.g, text.GetComponent<TMP_Text>().color.b, 0.5f);
        }
    }
  
}
