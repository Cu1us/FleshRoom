using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform graphicalRepresentation;
    [SerializeField] Animator animator;
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
        transform.parent.position = Vector3.Lerp(transform.parent.position, targetPosition, movementSpeed * Time.deltaTime / (transform.parent.position - targetPosition).magnitude);
        if ((transform.parent.position - targetPosition).magnitude < positionTolerance)
        {
            animator.SetBool("Walk", false);
            currentAction = actionQueue.Pop();
        }
    }

    private void PerformAction()
    {
        graphicalRepresentation.localScale = new Vector3(5, 5, 1);
        animator.SetInteger("AnimationID", animationID);
        currentAction = actionQueue.Pop();
        interactAction?.Invoke();
    }
    private void AssignAnimation(int animationID)
    {
        this.animationID = animationID;
    }
    private void OnPositionChangedEvent(float position, Action iAction)
    {
        actionQueue.Clear();
        animationID = 0;
        animator.SetInteger("AnimationID", animationID);
        actionQueue.Push(Sleep);
        if (iAction != null)
        {
            interactAction = iAction;
            actionQueue.Push(PerformAction);
        }
        targetPosition = new Vector3(position, transform.parent.position.y, 0);
        if (targetPosition.x < transform.position.x)
        {
            graphicalRepresentation.localScale = new Vector3(-5,5, 1);
        }
        else
        {
            graphicalRepresentation.localScale = new Vector3(5, 5, 1);
        }
        animator.SetBool("Walk", true);
        actionQueue.Push(Move);
        currentAction = actionQueue.Pop();
    }

    private void OnDisable()
    {
        EventHandler.Instance.PlayerChangeEvent -= OnPositionChangedEvent;
    }
    private void OnEnable()
    {
        actionQueue.Push(Sleep);
        currentAction = Sleep;
        EventHandler.Instance.PlayerChangeEvent += OnPositionChangedEvent;
    }
}
