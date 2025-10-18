using System;
using UnityEngine;

public class DialogueSequence : MonoBehaviour
{
    public DialogueSequenceStep[] Sequence;
}

[Serializable]
public class DialogueSequenceStep
{
    public Dialogue Speaker;
    public string Line;
    [Min(0)] public float Duration = 1.5f;
}