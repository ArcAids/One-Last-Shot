using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcAid.Audio;

public class MusicDialogue : MonoBehaviour
{
    [SerializeField] DialogueSequence litMusicDialogue;
    [SerializeField] DialogueSequence noMusicDialogue;
    [SerializeField] AudioData audioData; 

    public void PlayDialogue()
    {
        if (audioData.Muted)
            noMusicDialogue.Play();
        else
            litMusicDialogue.Play();
    }
}
