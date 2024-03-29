using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnPoints
{
    public Transform spawnTransform;
    public bool isOccupied;
}

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] itemArray;

    [SerializeField]
    private SpawnPoints[] playerSpawns;

    [SerializeField]
    private SpawnPoints[] enemySpawns;

    private GameObject[] playerItemObjects;

    private void Start()
    {
        playerItemObjects = GameObject.FindGameObjectsWithTag("PlayerItem");
    }

    public void SpawnItem()
    {
        if (Contestants.s_playerTurn)
        {
            if (playerItemObjects.Length < 4)
            {
                // Find a randomly selected non-occupied item spawn-point
                // Best-case scenario of O(1) and a worst-case of O(infinity) lmao
                // This is amazing - don't remove ever
                int itemSpawnIndex = Random.Range(0, playerSpawns.Length - 1);

                while (playerSpawns[itemSpawnIndex].isOccupied)
                {
                    itemSpawnIndex = Random.Range(0, playerSpawns.Length - 1);
                }

                // Unoccupied spawn-point found - spawn a random item in it
                int itemIndex = Random.Range(0, itemArray.Length - 1);

                GameObject itemPrefab = itemArray[itemIndex];
                Mesh itemPrefabMesh = itemPrefab.GetComponent<MeshFilter>().sharedMesh;

                Vector3 spawnPosition = playerSpawns[itemSpawnIndex].spawnTransform.position;
                spawnPosition.y += itemPrefabMesh.bounds.extents.y * itemPrefab.transform.localScale.y;

                Transform itemSpawnTransform = playerSpawns[itemSpawnIndex].spawnTransform;
                Mesh itemSpawnPlateMesh = itemSpawnTransform.GetComponent<MeshFilter>().mesh;

                // Calculates where the top surface of the spawn mesh is in world-space so
                // we can use it to position the item object directly on top
                spawnPosition.y += itemSpawnPlateMesh.bounds.extents.y * itemSpawnTransform.localScale.y;

                GameObject item = Instantiate(
                    itemPrefab,
                    spawnPosition,
                    itemPrefab.transform.rotation
                );

                item.GetComponent<Item>().slot = itemSpawnIndex;
                playerSpawns[itemSpawnIndex].isOccupied = true;

                item.gameObject.tag = "PlayerItem";
            }
        }

        playerItemObjects = GameObject.FindGameObjectsWithTag("PlayerItem");
    }

    public void OnItemUsed(int slot)
    {
        playerSpawns[slot].isOccupied = false;
    }
}
