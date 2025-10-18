using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] InputActionReference inputReference;
    [SerializeField] float movementSpeed;
    [SerializeField] float positionTolerance;
    Vector3 targetPosition;
    Action currentAction;
    private Stack<Action> actionQueue = new Stack<Action>();
    int animationID; bool facingRight;

    void Update()
    {
        if (inputReference.action.IsPressed())  
        {
            EventHandler.Instance.PlayerChangeEvent?.Invoke(new Vector3(0, 0, 0), 0, false);
        }
        currentAction();
    }
    
    void Sleep()
    {

    }

    private void Move()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);
        if ((transform.position - targetPosition).magnitude < positionTolerance) currentAction = actionQueue.Pop();
    }

    private void PlayAnimation()
    {
        //change player rotation & play animation before returning to sleep
        currentAction = actionQueue.Pop();
    }
    private void OnPositionChangedEvent(Vector3 position, int animationID, bool right)
    {
        actionQueue.Clear();
        actionQueue.Push(Sleep);
        if (animationID != 0)
        {
            facingRight = right;
            this.animationID = animationID;
            actionQueue.Push(PlayAnimation);
        }
        targetPosition = position;
        actionQueue.Push(Move);
        currentAction = actionQueue.Pop();
    }

    private void OnDisable()
    {
        EventHandler.Instance.PlayerChangeEvent -= OnPositionChangedEvent;
    }
    private void Start() //change to OnEnable when applicable
    {
        currentAction = Sleep;
        EventHandler.Instance.PlayerChangeEvent += OnPositionChangedEvent;
    }
}
