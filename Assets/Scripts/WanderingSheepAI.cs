using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingSheepAI : MonoBehaviour
{
    public float speed = 2.0f;
    public float obstacleRange = 2.0f;
    public float scaredRange = 4.0f;

    private float nextTurnTime;

    private bool isAvoidingObstacles;

    public bool IsScared { get; private set; }
    private Vector3 wolfPos = Vector3.zero;

    private void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
        ObstacleCheckAndAvoid();
        if(isAvoidingObstacles)
            IsScared = false;

        if (IsScared && Vector3.Distance(wolfPos, transform.position) < scaredRange) // scared sheep behaviour below
        {
            wolfPos.y = transform.position.y;
            transform.LookAt(transform.position - (wolfPos - transform.position));
        }
        else
            IsScared = false; //stop being scared when out of 4 meter range

        if (!IsScared && !isAvoidingObstacles) //if not scared and there's no obstacles turn randomly
        {
            if (Time.time >= nextTurnTime)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
                nextTurnTime = Time.time + Random.Range(2.0f, 4.0f);
            }
        }
    }
    
    private void ObstacleCheckAndAvoid() // method to avoid obstacles wich also returns a boolean to use it so sheep will prioritize avoiding an obstacle
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.5f, out hit))
        {
            if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-90, 90);
                transform.Rotate(0, angle, 0);
                isAvoidingObstacles = true;
            }
            else isAvoidingObstacles = false;
        }
        else isAvoidingObstacles = false;
    }
    public void Scare()
    {
        if (isAvoidingObstacles)
                IsScared = false;

        else if (!IsScared && !isAvoidingObstacles)
                IsScared = true;

        else if (IsScared && !isAvoidingObstacles) //keep scaring sheep while it's not avoiding obstacles
                IsScared = true;

    }
    public void SendWolfPosition(Vector3 posToRunFrom)
    {
        wolfPos = posToRunFrom;
    }
    public void Caught()
    {
        EventSystem.InvokeOnSheepCaught(this.gameObject);
    }
}
