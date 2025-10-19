using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueSequence : MonoBehaviour
{
    public DialogueSequenceStep[] Sequence;

    Queue<DialogueSequenceStep> stepSequence = new();

    public UnityEvent OnStart;
    public UnityEvent OnFinish;


    public void PlaySequence()
    {
        Debug.Log("Starts");
        StopDialogue();
        stepSequence.Clear();
        foreach (DialogueSequenceStep step in Sequence)
        {
            stepSequence.Enqueue(step);
            step.Speaker.StopDialogue();
        }
        OnStart?.Invoke();
        ShowNext();
    }
    void ShowNext()
    {
        if (stepSequence.Count == 0)
        {
            CancelInvoke(nameof(ShowNext));
            OnFinish?.Invoke();
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
    public AudioClip Audio;
}