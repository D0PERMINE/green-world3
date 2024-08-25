using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private FadeEffect fadeEffect;
    [SerializeField] private ParticleSystem dirtySmokeParticles;
    [SerializeField] private ShowText showAchievementText;
    [SerializeField] private GameObject storyPanel;
    [SerializeField] private ShowText showText;
    [SerializeField] private AudioSource achievementAudio;
    [SerializeField] float delayBeforeEndScene;
    [SerializeField] GameObject endScene;
    // Start is called before the first frame update
    void Start()
    {
        endScene.SetActive(false);
        StartCoroutine(CityFade());
        StartCoroutine(StopDirtySmokeParticles());
        StartCoroutine(StartTyping());
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
        achievementAudio.Play();
       
        //yield return null;
        //StartCoroutine(showText.TypeText());
    }

    public void StartEndScene()
    {
        StartCoroutine(ShowEndScene());
    }
    IEnumerator ShowEndScene()
    {
        yield return new WaitForSeconds(delayBeforeEndScene);
       endScene.SetActive(true);
    }
}
