using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public struct AudioData
{
    public float volume;
}

public class UIManager : MonoBehaviour
{
    AudioData audData;
    public Slider masterVolSlider;
    public AudioMixer mixer;
    public Button button;

    private void Start()
    {
        masterVolSlider.value = audData.volume;
        mixer.SetFloat("MasterVol", audData.volume);

        masterVolSlider.onValueChanged.AddListener(MasterVolChanged);

        button.onClick.AddListener(ExitGamePressed);

    }

    private void MasterVolChanged(float _value)
    {
        audData.volume = _value;
        mixer.SetFloat("MasterVol", Mathf.Log10(_value) * 20f);
    }

    private void ExitGamePressed()
    {
        Application.Quit();
    }
}
