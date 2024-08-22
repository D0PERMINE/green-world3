using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] FadeEffect fadeEffect;
    [SerializeField] ParticleSystem dirtySmokeParticles;
    [SerializeField] ShowText showAchievementText;
    [SerializeField] GameObject storyPanel;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CityFade());
        StartCoroutine(StopDirtySmokeParticles());
        StartCoroutine(StartTyping());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CityFade()
    {
        Debug.Log("hi!");
        yield return new WaitForSeconds(5f);
        fadeEffect.StartFadeIn();
    }

    IEnumerator StopDirtySmokeParticles()
    {
        yield return new WaitForSeconds(10f);
        dirtySmokeParticles.Stop();
    }

    IEnumerator StartTyping()
    {
        yield return new WaitForSeconds(3f);
        storyPanel.SetActive(true);
        yield return new WaitForSeconds(.01f);
        StartCoroutine(showAchievementText.TypeText());
    }
}
