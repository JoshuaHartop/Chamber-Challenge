using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public struct SpawnPoints
{
    public Transform spawnTransform;
    public bool isOccupied;
}



public class itemSpawning : MonoBehaviour
{

    public GameObject[] itemArray;
    GameObject[] playerItemObjects;
    public SpawnPoints[] spawnPointsPlayer;
    SpawnPoints[] spawnPointsEnemy;


    public void Start()
    {
        playerItemObjects = GameObject.FindGameObjectsWithTag("PlayerItem");
    }
    public void OnItemUsed(int slot)
    {
        spawnPointsPlayer[slot].isOccupied = false;
    }
    // Start is called before the first frame update
    public void spawnItem()
    {
        int itemIndex = Random.Range(0, itemArray.Length - 1); // getting a random item from prefabs to spawn
        int itemSpawnIndex = Random.Range(0, spawnPointsPlayer.Length - 1); // getting a random spawnpoint to spawn from the players side
        if (Contestants.s_playerTurn)
        {


            if (playerItemObjects.Length == 4) // checks if all squares are occupied for the player
            {

            }
            else
            {
                


                while (spawnPointsPlayer[itemSpawnIndex].isOccupied) // checks if the current item spawn is already occupied and if it is it loops again
                {
                    itemSpawnIndex = Random.Range(0, spawnPointsPlayer.Length - 1);
                }
                Vector3 position = spawnPointsPlayer[itemSpawnIndex].spawnTransform.position; // sets the position variable to the current item being spawned
               // position.y += 0.12f; // moves the item to above the spawnpad

                var item = Instantiate(itemArray[itemIndex], position, itemArray[itemIndex].transform.rotation); // instantiates the spawned item
                item.gameObject.tag = "PlayerItem"; // sets the items tag
                item.GetComponent<Item>().AssignSlot(itemSpawnIndex);
                
                }
        }
        playerItemObjects = GameObject.FindGameObjectsWithTag("PlayerItem");
        // getting and setting the spots that are occupied for the player
        for (int i = 0; i < playerItemObjects.Length; i++)
        {
            for (int j = 0; j < spawnPointsPlayer.Length; j++)
            {
                if (playerItemObjects[i].transform.position == spawnPointsPlayer[j].spawnTransform.position)
                {
                    spawnPointsPlayer[j].isOccupied = true;
                }

            }

        }

    }
        /*
                      for (int i = 0; i < playerItemObjects.Length; i++)
                {
                    switch (itemSpawnIndex) //
                    {
                        case 0:
                            playerItemObjects[itemSpawnIndex].transform.position = position;
                            break;
                        case 1:
                            playerItemObjects[itemSpawnIndex].transform.position = position;
                            break;
                        case 2:
                            playerItemObjects[itemSpawnIndex].transform.position = position;
                            break;
                        case 3:
                            playerItemObjects[itemSpawnIndex].transform.position = position;
                            break;
                    }
                }
        */
        /* else if (Contestants.s_playerTurn == false)
         {
             Instantiate(itemArray[itemIndex], spawnPointsEnemy[Random.Range(0, spawnPointsEnemy.Length - 1)].position, itemArray[itemIndex].transform.rotation).transform.Rotate(Vector3.up, 180);
         }*/

    
}
