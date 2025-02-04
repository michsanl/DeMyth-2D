using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using System;

public class Pickupable : Interactable
{
    [SerializeField] private DialogueSystemTrigger dialogueSystemTrigger;
    [SerializeField] private GameObject mainGameObject;

    public override void Interact(Player player, Vector3 direction = default)
    {
        dialogueSystemTrigger.OnUse();
    }
    
}
