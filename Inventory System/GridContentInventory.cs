using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridContentInventory : MonoBehaviour
{
    [field:SerializeField] public int Rows { get; private set; }
    [field:SerializeField] public int Columns { get; private set; }
    
    [field:SerializeField] public InventorySlot[] InventorySlots;
    
    public event Action<InventorySlot, int> OnItemChange;

    private void Awake()
    {
        InitialiseInventory();
    }

    public void PlaceItem(int x, int y, InventoryItem inventoryItem)
    {
        int slotIndex = GetSlotIndex(x, y);
        
        InventorySlot inventorySlot = InventorySlots[slotIndex];
            inventorySlot.Item = inventoryItem;
        
        OnItemChange?.Invoke(inventorySlot, slotIndex);
    }
    
    public int GetSlotIndex(int x, int y)
    {
        return y * Rows + x;
    }

    private void InitialiseInventory()
    {
        InventorySlots = new InventorySlot[Rows * Columns];

        for (int i = 0; i < InventorySlots.Length; i++)
            InventorySlots[i] = new InventorySlot();
    }
}
