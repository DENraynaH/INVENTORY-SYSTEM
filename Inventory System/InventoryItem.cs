using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
[Serializable]
public class InventoryItem : ScriptableObject
{
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public string Description { get; private set; }
    [field:SerializeField] public Sprite Icon { get; private set; }
    [field:SerializeField] public Sprite IconOutline { get; set; }
    [field:SerializeField] public GameObject Prefab { get; private set; }
    [field:SerializeField] public float Durability { get; private set; }
    [field:SerializeField] public int StackSize { get; private set; } = 1;
}
