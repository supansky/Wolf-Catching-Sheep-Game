using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Vector3 startingPlayerPosition;
    SheepController sheepController;

    private void Awake()
    {
        EventSystem.OnLevelStart += RestartLevel;
        sheepController = SheepController.Instance;
        sheepController.SpawnSheep();
        startingPlayerPosition = player.transform.position;
    }
    private void OnDestroy()
    {
        EventSystem.OnLevelStart -= RestartLevel;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            EventSystem.InvokeOnlevelStart();
        }
    }
    
    public void RestartLevel()
    {
        player.SetActive(false);
        player.transform.position = startingPlayerPosition;
        player.SetActive(true);
        sheepController.SpawnSheep();
    }    

}
