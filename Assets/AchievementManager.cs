using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] FadeEffect fadeEffect;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CityFade());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CityFade()
    {
        Debug.Log("hi!");
        yield return new WaitForSeconds(3f);
        fadeEffect.StartFadeIn();
    }
}
