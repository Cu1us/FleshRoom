using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI TextField;
    public Color SpeakerColor = Color.white;

    Queue<DialogueSequenceStep> stepSequence = new();

    public void SpeakLine(string line)
    {
        StopDialogue();
        DialogueSequenceStep step = new()
        {
            Speaker = this,
            Line = line,
            Duration = line.Length * 0.04f + 1
        };
        stepSequence.Enqueue(step);
        ShowSequenceStep(step);
    }
    public void SpeakSequence(DialogueSequence sequence)
    {
        StopDialogue();
        stepSequence.Clear();
        foreach (DialogueSequenceStep step in sequence.Sequence)
        {
            stepSequence.Enqueue(step);
        }
        ShowNext();
    }
    void ShowSequenceStep(DialogueSequenceStep step)
    {
        TextField.enabled = true;
        TextField.text = step.Line;
        TextField.color = SpeakerColor;
    }

    void ShowNext()
    {
        if (stepSequence.Count == 0)
        {
            StopDialogue();
            return;
        }
        DialogueSequenceStep step = stepSequence.Dequeue();
        ShowSequenceStep(step);
        Invoke(nameof(ShowNext), step.Duration);
    }

    void StopDialogue()
    {
        stepSequence.Clear();
        CancelInvoke(nameof(ShowNext));
        TextField.enabled = false;
        TextField.text = string.Empty;
    }
}
