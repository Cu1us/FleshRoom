using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float positionTolerance;
    Vector3 targetPosition;
    Action currentAction, interactAction;
    private Stack<Action> actionQueue = new Stack<Action>();
    int animationID; bool facingRight;

    void Update()
    {
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

    private void PerformAction()
    {
        //change player rotation & play animation before returning to sleep
        currentAction = actionQueue.Pop();
        interactAction?.Invoke();
    }
    private void OnPositionChangedEvent(float position, int animationID, bool right, Action iAction)
    {
        actionQueue.Clear();
        actionQueue.Push(Sleep);
        if (animationID != 0)
        {
            facingRight = right;
            this.animationID = animationID;
            interactAction = iAction;
            actionQueue.Push(PerformAction);
        }
        targetPosition = new Vector3(position, transform.position.y, 0);
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
