using System;
using System.Collections.Generic;
using JE.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryRaycastHandler : MonoBehaviour
{
    [SerializeField] private GridDisplayInventory _gridDisplayInventory;
    private EventSystem _eventSystem;
    private PointerEventData _pointerEventData;
    private GraphicRaycaster _graphicRaycaster;
    private readonly List<RaycastResult> _raycastResults = new List<RaycastResult>();
    private Vector2 _initPosition;
    
    public void Initialise()
    {
        _eventSystem = EventSystem.current;
        _pointerEventData = new PointerEventData(EventSystem.current);
        _graphicRaycaster = GetComponent<GraphicRaycaster>();
    }

    public bool TryGetHoveredSlotContainer(out InventoryDisplaySlotContainer slotContainer)
    {
        _pointerEventData.position = Mouse.current.position.ReadValue();
        _raycastResults.Clear();
        _graphicRaycaster.Raycast(_pointerEventData, _raycastResults);

        foreach (RaycastResult raycastResult in _raycastResults)
        {
            InventoryDisplaySlotContainer parentSlotContainer =
                raycastResult.gameObject.GetComponentInParent<InventoryDisplaySlotContainer>();

            if (parentSlotContainer)
            {
                slotContainer = parentSlotContainer;
                return true;
            }
        }
        slotContainer = null;
        return false;
    }

    public InventoryDisplaySlotContainer GetHoveredSlotContainer()
    {
        _pointerEventData.position = Mouse.current.position.ReadValue();
        _raycastResults.Clear();
        _graphicRaycaster.Raycast(_pointerEventData, _raycastResults);

        foreach (RaycastResult raycastResult in _raycastResults)
        {
            InventoryDisplaySlotContainer slotContainer =
                raycastResult.gameObject.GetComponentInParent<InventoryDisplaySlotContainer>();

            if (slotContainer)
                return slotContainer;
        }
        return null;
    }
    
    public bool TryGetHoveredSlot()
    {
        Vector2 initPosition = _gridDisplayInventory._rectInitialPosition;
        Vector2 spacing = _gridDisplayInventory._rectSpacing;
        Vector2 scale = _gridDisplayInventory._rectScale;
        
        Vector2 mouseRectPosition = Mouse.current.position.ReadValue();
        
        int x = Mathf.FloorToInt((mouseRectPosition.x - initPosition.x) / (scale.x * spacing.x));
        int y = Mathf.FloorToInt((mouseRectPosition.y - initPosition.y) / (scale.y * spacing.y));
        
        Debug.Log($"X: {x}, Y: {y}");

        return true;
    }
}