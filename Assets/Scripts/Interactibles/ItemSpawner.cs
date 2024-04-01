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
    private List<GameObject> itemArray = new List<GameObject>();

    [SerializeField]
    private List<SpawnPoints> playerSpawns = new List<SpawnPoints>();

    [SerializeField]
    private List<SpawnPoints> enemySpawns = new List<SpawnPoints>();

    private List<GameObject> playerItemObjects = new List<GameObject>();
    private List<GameObject> enemyItemObjects = new List<GameObject>();

    private void Start()
    {
        playerItemObjects.AddRange(GameObject.FindGameObjectsWithTag("PlayerItem"));
        enemyItemObjects.AddRange(GameObject.FindGameObjectsWithTag("EnemyItem"));
    }

    public void SpawnItem()
    {
        if (Contestants.s_playerTurn)
        {
            if (playerItemObjects.Count < 4)
            {
                // Find a randomly selected non-occupied item spawn-point
                // Best-case scenario of O(1) and a worst-case of O(infinity) lmao
                // This is amazing - don't remove ever
                int itemSpawnIndex = Random.Range(0, playerSpawns.Count - 1);

                while (playerSpawns[itemSpawnIndex].isOccupied)
                {
                    playerSpawns.RemoveAt(itemSpawnIndex);
                    itemSpawnIndex = Random.Range(0, playerSpawns.Count - 1);
                }

                // Unoccupied spawn-point found - spawn a random item in it
                int itemIndex = Random.Range(0, itemArray.Count - 1);

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
                playerSpawns[itemSpawnIndex] = new SpawnPoints { isOccupied = true, spawnTransform = playerSpawns[itemSpawnIndex].spawnTransform };


                item.gameObject.tag = "PlayerItem";
            }
        }
        else
        {
            SpawnEnemyItem();
        }

        playerItemObjects.AddRange(GameObject.FindGameObjectsWithTag("PlayerItem"));
    }

    private void SpawnEnemyItem()
    {
        if (enemyItemObjects.Count < 4)
        {
            // Find a randomly selected non-occupied item spawn-point
            // Best-case scenario of O(1) and a worst-case of O(infinity) lmao
            // This is amazing - don't remove ever
            int itemSpawnIndex = Random.Range(0, enemySpawns.Count - 1);

            while (enemySpawns[itemSpawnIndex].isOccupied)
            {
                enemySpawns.RemoveAt(itemSpawnIndex);
                itemSpawnIndex = Random.Range(0, enemySpawns.Count - 1);
            }

            // Unoccupied spawn-point found - spawn a random item in it
            int itemIndex = Random.Range(0, itemArray.Count - 1);

            GameObject itemPrefab = itemArray[itemIndex];
            Mesh itemPrefabMesh = itemPrefab.GetComponent<MeshFilter>().sharedMesh;

            Vector3 spawnPosition = enemySpawns[itemSpawnIndex].spawnTransform.position;
            spawnPosition.y += itemPrefabMesh.bounds.extents.y * itemPrefab.transform.localScale.y;

            Transform itemSpawnTransform = enemySpawns[itemSpawnIndex].spawnTransform;
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

            enemySpawns[itemSpawnIndex] = new SpawnPoints {
                isOccupied = true,
                spawnTransform = enemySpawns[itemSpawnIndex].spawnTransform
            };


            item.gameObject.tag = "EnemyItem";
        }
    }

    public void OnItemUsed(int slot)
    {
        if (Contestants.s_playerTurn)
            playerSpawns[slot] = new SpawnPoints { isOccupied = true, spawnTransform = playerSpawns[slot].spawnTransform };
        else
            enemySpawns[slot] = new SpawnPoints { isOccupied = true, spawnTransform = enemySpawns[slot].spawnTransform };
    }
}
