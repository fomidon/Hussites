using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource currentAudio;

    public readonly List<string> musicNames = new List<string>() { 
        "Dve_nevesti", "Зов крови", "Naranca", "GustaMiMagla", "Lazare" };
    public int musicCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentAudio = GameObject.Find(musicNames[musicCounter]).GetComponent<AudioSource>();
        currentAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAudio.clip.length - currentAudio.time <= 1e-5)
        {
            UpdateMusic();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            UpdateMusic();
        }
    }

    private void UpdateMusic()
    {
        musicCounter = (musicCounter + 1) % musicNames.Count;
        Debug.Log(musicCounter);
        currentAudio.Stop();
        currentAudio.time = 0;
        currentAudio = GameObject.Find(musicNames[musicCounter]).GetComponent<AudioSource>();
        currentAudio.Play();
    }
}
