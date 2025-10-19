using Cu1uSFX;
using Cu1uSFX.Internal;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI TextField;
    public Color SpeakerColor = Color.white;

    public void SpeakLine(string line)
    {
        DialogueSequenceStep step = new()
        {
            Speaker = this,
            Line = line,
            Duration = line.Length * 0.04f + 1,
        };
        SFX.Interact.Play();
        ShowSequenceStep(step);
    }
    public void ShowSequenceStep(DialogueSequenceStep step)
    {
        CancelInvoke(nameof(StopDialogue));
        TextField.enabled = true;
        TextField.text = step.Line;
        TextField.color = SpeakerColor;
        new SFXDefinition(step.Audio).Play();
        Invoke(nameof(StopDialogue), step.Duration);
    }
    public void StopDialogue()
    {
        TextField.enabled = false;
        TextField.text = string.Empty;
    }
}
