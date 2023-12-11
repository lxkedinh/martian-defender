using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Wall : MonoBehaviour
{
    public GameObject wallPrefab;
    public SpriteRenderer wallSpriteRenderer;
    public Sprite defaultWallSprite;
    public Sprite alternateWallSprite;

    // Attempt to use rule tiles
    /* public Tilemap wallTilemap; */
    /**/
    /* // Method to change the wall sprite using Rule Tiles */
    /* public void ChangeWallSprite(Vector3Int cellPosition) */
    /* { */
    /*     if (wallTilemap != null) */
    /*     { */
    /*         wallTilemap.RefreshTile(cellPosition); // Refresh the tile to update its appearance */
    /*     } */
    /*     else */
    /*     { */
    /*         Debug.LogError("Tilemap reference is null."); */
    /*     } */
    /* } */

    // Method to change the wall sprite based on surrounding walls
    public void ChangeWallSprite(Vector3 newPosition, HashSet<Wall> walls)
    {
        bool isNextToWall = false;

        foreach (Wall existingWall in walls)
        {
            if (existingWall != this) // Skip checking against itself
            {
                Vector3 existingPosition = existingWall.transform.position;

                // Check if the new position is next to any existing wall
                if (Mathf.Abs(existingPosition.x - newPosition.x) <= 1 &&
                    Mathf.Abs(existingPosition.y - newPosition.y) <= 1)
                {
                    // Calculate the relative position of the new wall
                    float diffX = existingPosition.x - newPosition.x;
                    float diffY = existingPosition.y - newPosition.y;

                    // Check if it's in the southeast or northwest direction
                    if ((diffX > 0 && diffY > 0) || (diffX < 0 && diffY < 0))
                    {
                        isNextToWall = true;
                        wallSpriteRenderer.sprite = alternateWallSprite; // Change to alternate sprite
                        break; // No need to check further if adjacent wall is found
                    }
                }
            }
        }

        if (!isNextToWall)
        {
            wallSpriteRenderer.sprite = defaultWallSprite; // Set default sprite
        }
    }

}
