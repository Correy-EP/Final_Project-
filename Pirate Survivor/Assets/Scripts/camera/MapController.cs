using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MapController : MonoBehaviour
{

    public List<GameObject> terrainChunks;
    public GameObject Player;
    public float checkerRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    PlayerScript pm;

    [Header("optimization")]
    public List<GameObject> spawnedChunks;
    public GameObject latestChunk;
    public float MaxOpDist; // must be greater than the legnth and width  of the tilemap
    float opDist;
    float optimizerCooldown;
    public float optimizationCooldowndur;

    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        chunkCheker();

        chunkOptimizer();
    }

    void chunkCheker()
    {
        if (!currentChunk)
        {
            return;
        }
        if (pm.moveDir.x > 0 && pm.moveDir.y == 0) //right 
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("right").position;

                spawnChunk();

            }
        }

        else if (pm.moveDir.x < 0 && pm.moveDir.y == 0) //left
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("left").position;

                spawnChunk();

            }
        }

        else if (pm.moveDir.x == 0 && pm.moveDir.y > 0) //up
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("up").position;

                spawnChunk();

            }

        }

        else if (pm.moveDir.x == 0 && pm.moveDir.y < 0) //down
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("down").position;

                spawnChunk();

            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y > 0) //right up
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("right up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("right up").position;

                spawnChunk();

            }
        }

        else if (pm.moveDir.x > 0 && pm.moveDir.y < 0) //right down
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("right down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("right down").position;

                spawnChunk();

            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y > 0) //Left UP
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("left up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("left up").position;

                spawnChunk();

            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y < 0) //Left down
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("left down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("left down").position;

                spawnChunk();

            }
        }

        void spawnChunk()
        {
            int rand = Random.Range(0, terrainChunks.Count);
            latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
            spawnedChunks.Add(latestChunk);
        }

    }

    void chunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0f) 
        {
            optimizerCooldown = optimizationCooldowndur;
        }
        else
        {
            return;
        }
        foreach (GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(Player.transform.position, chunk.transform.position);

            if(opDist > MaxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
 