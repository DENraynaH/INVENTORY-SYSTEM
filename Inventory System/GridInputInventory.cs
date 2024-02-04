using System;
using UnityEngine;
using UnityEngine.UI;

public class GridInputInventory : MonoBehaviour
{
    private InventoryRaycastHandler _raycastHandler;

    public event Action<bool, InventoryDisplaySlotContainer> OnHover; 

    private void Awake()
    {
        _raycastHandler = GetComponentInChildren<InventoryRaycastHandler>();
        _raycastHandler.Initialise();
    }

    private void HandleSlotHovering()
    {
        bool isHoveredSlot = _raycastHandler.TryGetHoveredSlotContainer
            (out InventoryDisplaySlotContainer slotContainer);

        OnHover?.Invoke(isHoveredSlot, slotContainer);

        if (isHoveredSlot)
        {
            Debug.Log(slotContainer.SlotIndex);
        }
        else
        {
            Debug.Log("Hovering Nothing SIr!");
        }
    }

    private void Update()
    {
        _raycastHandler.TryGetHoveredSlot();
        //HandleSlotHovering();
    }
}
