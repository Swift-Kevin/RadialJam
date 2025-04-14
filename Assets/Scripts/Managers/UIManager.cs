using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public struct AudioData
{
    public float volume;
}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public void Awake()
    {
        Instance = this;
    }

    AudioData audData;
    public Slider masterVolSlider;
    public AudioMixer mixer;
    public Button button;

    [Seperator]
    [Header("Fading")]
    public Image fadeCurtain;
    public float fadeTime = 2.5f;
    public AnimationCurve curve;


    [Seperator]
    public Dictionary<string, TextMeshProUGUI> upgradeValues;

    private void Start()
    {
        masterVolSlider.value = audData.volume;
        mixer.SetFloat("MasterVol", audData.volume);

        masterVolSlider.onValueChanged.AddListener(MasterVolChanged);

        button.onClick.AddListener(ExitGamePressed);
    }

    public void StartFadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(FadeIn());
    }

    private void MasterVolChanged(float _value)
    {
        audData.volume = _value;
        mixer.SetFloat("MasterVol", Mathf.Log10(_value) * 20f);
    }

    private void ExitGamePressed()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        fadeCurtain.color = new Color(0f, 0f, 0f, 0f);
        fadeCurtain.gameObject.SetActive(true);

        // Create a float storing the timer
        float timerFadeIn = fadeTime;

        // While the timer is above a 'second'
        while (timerFadeIn > 0f)
        {
            // Add the Time.deltatime (interval in seconds from last frame to current frame) to the timer
            timerFadeIn -= Time.unscaledDeltaTime;
            // Evaluate the curve and then set that the alpha's amount
            float alphaColorFadeIn = curve.Evaluate(timerFadeIn);
            // Set the image color component to a new color of an decreased alpha
            fadeCurtain.color = new Color(0f, 0f, 0f, alphaColorFadeIn);
            yield return 0;
        }

        fadeCurtain.gameObject.SetActive(false);
    }

    private IEnumerator FadeOut()
    {
        fadeCurtain.color = new Color(0f, 0f, 0f, 0f);
        fadeCurtain.gameObject.SetActive(true);

        float timerFadeOut = 0f;
        while (timerFadeOut < fadeTime)
        {
            timerFadeOut += Time.unscaledDeltaTime;
            float alphaColorFadeOut = curve.Evaluate(timerFadeOut);
            fadeCurtain.color = new Color(0f, 0f, 0f, alphaColorFadeOut);
            yield return 0;
        }

        fadeCurtain.gameObject.SetActive(false);
        Application.Quit();
    }
}
