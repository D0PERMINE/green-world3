using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RightOption
{
    firstOption,
    secondOption
}

[CreateAssetMenu(fileName = "QuizQuestion_", menuName = "ScriptableObjects/QuizQuestionScriptableObject", order = 1)]
public class QuizQuestionSO : ScriptableObject
{
    public string Question;
    public string Description_1;
    public Sprite Image_1;

    public string Description_2;
    public Sprite Image_2;

    public RightOption rightOption;

}
