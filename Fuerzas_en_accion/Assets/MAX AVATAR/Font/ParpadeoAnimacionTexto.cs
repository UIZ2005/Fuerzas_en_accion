using System.Collections;
using TMPro;
using UnityEngine;

public class ParpadeoAnimacionTexto : MonoBehaviour
{
    [Header("Colors")]
    public Color idleColor = Color.gray;
    public Color activeColor = Color.white;
    public Color glowColor = new Color(0f, 1f, 1f);

    [Header("Glow")]
    public float idleGlow = 0f;
    public float activeGlow = 0.5f;

    [Header("Timing")]
    public float idleTime = 1f;
    public float activeTime = 0.5f;

    private TextMeshPro text;
    private Material materialInstance;

    void Start()
    {
        text = GetComponent<TextMeshPro>();

        // crear instancia del material para no modificar el global
        materialInstance = Instantiate(text.fontMaterial);
        text.fontMaterial = materialInstance;

        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            SetState(idleColor, idleGlow);
            yield return new WaitForSeconds(idleTime);

            SetState(activeColor, activeGlow);
            yield return new WaitForSeconds(activeTime);
        }
    }

    void SetState(Color color, float glowPower)
    {
        text.color = color;
        materialInstance.SetColor("_GlowColor", glowColor);
        materialInstance.SetFloat("_GlowPower", glowPower);
    }
}