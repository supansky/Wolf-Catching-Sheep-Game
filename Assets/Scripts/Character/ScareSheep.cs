using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareSheep : MonoBehaviour
{
    public float scareRange = 4f;
    public float initialScareRange = 2f;
    private float obstacleRange = 4f;

    private Vector3 posToSend;

    private void Update()
    {
        posToSend = transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, scareRange);
        foreach (Collider hitCollider in hitColliders)
        {
            WanderingSheepAI sheep = hitCollider.GetComponent<WanderingSheepAI>();
            if (sheep != null)
            {
                sheep.SendWolfPosition(posToSend);

                Ray ray = new Ray(transform.position, sheep.transform.position); //using sheep position for direction of the ray
                RaycastHit hit;
                if (Physics.SphereCast(ray, 1f, out hit, 100, ~1 << 3)) //ignore Sheep's layer (3) so wolf detects walls through them
                {
                    if (hit.distance < obstacleRange) // wolf has to check for walls near him so sheep won't be scared into them
                    {
                        Debug.Log("wall close");
                    }
                    else if (Vector3.Distance(transform.position, sheep.transform.position) < initialScareRange)
                    {
                        sheep.Scare();
                    }
                }
            }
        }

    }
}
