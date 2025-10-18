using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSequence : MonoBehaviour
{
    public DialogueSequenceStep[] Sequence;

    Queue<DialogueSequenceStep> stepSequence = new();


    public void PlaySequence()
    {
        StopDialogue();
        stepSequence.Clear();
        foreach (DialogueSequenceStep step in Sequence)
        {
            stepSequence.Enqueue(step);
            step.Speaker.StopDialogue();
        }
        ShowNext();
    }
    void ShowNext()
    {
        if (stepSequence.Count == 0)
        {
            CancelInvoke(nameof(ShowNext));
            return;
        }
        DialogueSequenceStep step = stepSequence.Dequeue();
        step.Speaker.ShowSequenceStep(step);
        Invoke(nameof(ShowNext), step.Duration);
    }
    void StopDialogue()
    {
        stepSequence.Clear();
        CancelInvoke(nameof(ShowNext));
    }
}

[Serializable]
public class DialogueSequenceStep
{
    public Dialogue Speaker;
    public string Line;
    [Min(0)] public float Duration = 1.5f;
}