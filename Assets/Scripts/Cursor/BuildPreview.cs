using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPreview : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer rangeRenderer;
    public Sprite wallSprite;
    public Sprite towerSprite;
    public Sprite CurrentSprite
    {
        get
        {
            switch (MapController.Instance.objectPlacementType)
            {
                case ObjectPlacementType.Tower:
                    return towerSprite;
                case ObjectPlacementType.Wall:
                    return wallSprite;
            }

            return towerSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = CurrentSprite;

        if (CurrentSprite != towerSprite || PlayerController.Instance.playMode != PlayMode.Build)
        {
            rangeRenderer.enabled = false;
        }
        else
        {
            rangeRenderer.enabled = true;
        }
    }
}
