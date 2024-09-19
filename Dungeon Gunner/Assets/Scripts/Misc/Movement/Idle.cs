using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(IdleEvent))]
[DisallowMultipleComponent]
public class Idle : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    private IdleEvent idleEvent;


    public void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        idleEvent = GetComponent<IdleEvent>();
    }

    public void OnEnable()
    {
        idleEvent.OnIdle += IdleEvent_OnIdle;
    }

    public void OnDisable()
    {
        idleEvent.OnIdle -= IdleEvent_OnIdle;
    }


    public void IdleEvent_OnIdle(IdleEvent onIdle)
    {
        MoveRigidBody();
    }

    public void MoveRigidBody()
    {
        rigidBody2D.velocity = Vector2.zero;
    }
}
