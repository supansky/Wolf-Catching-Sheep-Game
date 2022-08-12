using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchSheep : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        WanderingSheepAI sheep = other.GetComponent<WanderingSheepAI>();
        if (sheep != null)
        {
            sheep.Caught();
        }
    }
}
