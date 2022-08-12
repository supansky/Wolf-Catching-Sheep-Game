using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SheepController : Singleton<SheepController>
{
    [SerializeField] ObjectPool sheepPool;
    public int maxSheep = 21;
    private List<GameObject> sheepList = new List<GameObject>(); // made a sheep list to keep track of caught sheep

    private void Awake()
    {
        EventSystem.OnSheepCaught += OnSheepCaught;
    }
    private void OnDestroy()
    {
        EventSystem.OnSheepCaught -= OnSheepCaught;
    }

    public void SpawnSheep()
    {
        foreach(GameObject sheep in sheepList) // 23-28 lines are resetting all sheep and most importantly setting number of caught sheep to 0 on starting or restarting the game
        {
            sheep.SetActive(false);
            sheepPool.ReturnPooledObject(sheep);
        }
        sheepList.Clear();

        for (int i = 0; i < maxSheep; i++)
        {
            int posX;
            int posZ;
            Vector3 newPos;

            GameObject clone = sheepPool.GetPooledObject();

            clone.SetActive(true);

            posX = Random.Range(-24, 24); // I'm using double random so sheep won't spawn too close to the wolf
            posZ = Random.Range(-24, 24);
            newPos = new Vector3(posX, 0.25f, posZ);
            clone.transform.position = newPos;
            float angle = Random.Range(0, 360);
            clone.transform.Rotate(0, angle, 0);

            sheepList.Add(clone);

        }
    }

    private void OnSheepCaught(GameObject sheep)
    {
        sheep.SetActive(false);
        sheepPool.ReturnPooledObject(sheep);
        sheepList.Remove(sheep);
        if (sheepList.Count == 0) //set a record when all sheep are caught and restart a game
        {
            EventSystem.InvokeOnRecord();
            EventSystem.InvokeOnlevelStart();
        }
    }
}
