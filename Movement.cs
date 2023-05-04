using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//using NN;
//using System;
// using MathNet.Numerics;
// using MathNet.Numerics.LinearAlgebra;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    private bool hasController = false;
    private Vector3 playerVelocity;
    private float gravityValue = -9.81f;
    public float speed = 10.0F;
    public float rotateSpeed = 10.0F;
    public float FB = 0;
    public float LR = 0;

    private ObjectTracker objectTracker;
    private Creature creature;

    void Awake()
    {
        objectTracker = FindObjectOfType<ObjectTracker>();
        creature = GetComponent<Creature>();
        controller = GetComponent<CharacterController>();
    }

    public void Move(float FB, float LR)
    {
        //clamp the values of LR and FB
        LR = Mathf.Clamp(LR, -1, 1);
        FB = Mathf.Clamp(FB, 0, 1);

        //move the agent
        if (!creature.isDead)
        {
            // Rotate around y - axis
            transform.Rotate(0, LR * rotateSpeed, 0);

            // Move forward / backward
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            controller.SimpleMove(forward * speed * FB * -1);
        }


        //Checks to see if the agent is grounded, if it is, don't apply gravity
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        else
        {
            // Gravity
            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
}
