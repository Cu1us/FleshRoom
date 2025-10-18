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
        Debug.Log("tries to move");
        transform.parent.position = Vector3.Lerp(transform.parent.position, targetPosition, movementSpeed * Time.deltaTime / (transform.parent.position - targetPosition).magnitude);
        if ((transform.parent.position - targetPosition).magnitude < positionTolerance) currentAction = actionQueue.Pop();
    }

    private void PerformAction()
    {
        //change player rotation & play animation before returning to sleep
        currentAction = actionQueue.Pop();
        interactAction?.Invoke();
    }
    private void OnPositionChangedEvent(float position, int animationID, bool right, Action iAction)
    {
        Debug.Log("does the thing");
        actionQueue.Clear();
        actionQueue.Push(Sleep);
        if (animationID != 0)
        {
            facingRight = right;
            this.animationID = animationID;
            interactAction = iAction;
            actionQueue.Push(PerformAction);
        }
        targetPosition = new Vector3(position, transform.parent.position.y, 0);
        actionQueue.Push(Move);
        currentAction = actionQueue.Pop();
    }

    private void OnDisable()
    {
        EventHandler.Instance.PlayerChangeEvent -= OnPositionChangedEvent;
    }
    private void Start() //change to OnEnable when applicable
    {
        actionQueue.Push(Sleep);
        currentAction = Sleep;
        EventHandler.Instance.PlayerChangeEvent += OnPositionChangedEvent;
    }
}
