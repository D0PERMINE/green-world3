using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondQuestOptionHandler : MonoBehaviour
{ 
    [SerializeField]
    private int optionId = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            Debug.Log("Select option" + optionId);
            QuizUIManager.Instance.SetCurrentSelectedOption(this);
        }
    }

    public int GetOptionId()
    {
        return optionId;
    }
  
}
