using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;

    private List<GameObject> inactiveObjects = new List<GameObject>();


    private void SpawnObject()
    {
        GameObject obj = Instantiate(objectPrefab);
        obj.SetActive(false);
        inactiveObjects.Add(obj);
    }

    public GameObject GetPooledObject()
    {
        if(inactiveObjects.Count == 0)
        {
            SpawnObject();
        }
        GameObject inactiveObj = inactiveObjects[0];
        inactiveObjects.RemoveAt(0);
        return inactiveObj;
    }
    public void ReturnPooledObject(GameObject returnedObj)
    {
        inactiveObjects.Add(returnedObj);
    }
}
