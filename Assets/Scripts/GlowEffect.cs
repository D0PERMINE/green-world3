using System.Collections;
using UnityEngine;

public class GlowEffect : MonoBehaviour
{
    private Material material;
    private bool isGlowing = false;

    [Range(0, 1)]
    public float maxGlowStrength = 0.5f; // Maximale Glow-Stärke, die du anpassen kannst
    public float glowStrengthOnSelection = 0.5f; // Maximale Glow-Stärke, die du anpassen kannst
    public float glowDuration = 0.25f; // Dauer des Glow-Effekts in Sekunden
    public Color glowColor = Color.white; // Glow-Farbe, die du im Inspector einstellen kannst

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        material.SetFloat("_GlowStrength", 0.0f); // Initiale Glow-Stärke auf 0 setzen
        material.SetColor("_GlowColor", glowColor); // Setze die Glow-Farbe auf die gewünschte Farbe
    }

    public void TriggerGlow(float glow)
    {
        if (!isGlowing)
        {
            StartCoroutine(GlowCoroutine(glow));
        }
    }

    private IEnumerator GlowCoroutine(float glow)
    {
        isGlowing = true;
        float glowStrength = 0.0f;
        float halfDuration = glowDuration / 2.0f;

        // Aufglühen
        while (glowStrength < glow)
        {
            glowStrength += Time.deltaTime / halfDuration;
            material.SetFloat("_GlowStrength", Mathf.Min(glowStrength, glow));
            yield return null;
        }

        // Abklingen
        while (glowStrength > 0.0f)
        {
            glowStrength -= Time.deltaTime / halfDuration;
            material.SetFloat("_GlowStrength", Mathf.Max(glowStrength, 0.0f));
            yield return null;
        }

        isGlowing = false;
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        Debug.Log("GLOWWWW");
    //        GetComponent<GlowEffect>().TriggerGlow();
    //    }
    //}
}
