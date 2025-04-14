using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> tracks = new List<AudioClip>();
    public AudioSource audSrc;
    public float waitToPlay = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (tracks.Count > 0)
        {
            audSrc.clip = tracks[Random.Range(0, tracks.Count)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!audSrc.isPlaying)
        {
            if (waitToPlay <= 0)
            {
                if (tracks.Count > 0)
                {
                    audSrc.clip = tracks[Random.Range(0, tracks.Count)];
                    audSrc.Play();
                    waitToPlay = 5f;
                }
            }
            else
            {
                waitToPlay -= Time.deltaTime;
            }
        }
    }
}
