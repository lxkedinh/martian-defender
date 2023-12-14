using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class OreVein : MonoBehaviour
{
    public abstract Materials MaterialType { get; }
    public Animator animator;
    public Animator textAnimator;
    public TextMeshPro floatingText;
    public TextMeshPro mineTooltip;
    public Interactable interactHitbox;
    public bool hasPlayerMined;

    void Update()
    {
        if (interactHitbox.isPlayerInRange && !hasPlayerMined)
        {
            mineTooltip.renderer.enabled = true;
        }
        else
        {
            mineTooltip.renderer.enabled = false;
        }
    }

    public void BreakOre()
    {
        hasPlayerMined = true;

        int materialsToAdd = Random.Range(1, 5);
        InventoryController.Instance.AddMaterial(MaterialType, materialsToAdd);

        switch (MaterialType)
        {
            case Materials.Iron:
                floatingText.SetText($"+{materialsToAdd} <sprite name=\"icon_iron_ore\">");
                break;
            case Materials.Copper:
                floatingText.SetText($"+{materialsToAdd} <sprite name=\"icon_copper_ore\">");
                break;
        }

        animator.SetTrigger("isBreaking");
        textAnimator.SetTrigger("isBreaking");
    }
}
