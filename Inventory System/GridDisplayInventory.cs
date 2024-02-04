using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridContentInventory))]
public class GridDisplayInventory : MonoBehaviour
{
    [SerializeField] private Canvas _inventoryCanvas;
    [SerializeField] private Sprite _inventorySlotSprite;
    [SerializeField] private Sprite _inventoryOutlineSprite;
    [SerializeField] private Sprite _inventoryItemSprite;
    [SerializeField] private Sprite _inventoryHoverSprite;

    //Public for Debug
    [SerializeField] public List<InventoryDisplaySlot> _inventoryDisplaySlots = new();
    [SerializeField] public Vector2 _rectInitialPosition;
    [SerializeField] public Vector2 _rectSpacing;
    [SerializeField] public Vector2 _rectScale;

    private GridContentInventory _gridContentInventory;

    private float currentHorizontalSpacing;
    private float currentVerticalSpacing;

    private const string DISPLAY_HOVER_TEXT = "Hover Container";
    private const string DISPLAY_PARENT_TEXT = "Inventory Slot";
    private const string DISPLAY_SLOT_TEXT = "Slot";
    private const string DISPLAY_ITEM_TEXT = "Item";
    private const string DISPLAY_OUTLINE_TEXT = "Outline";

    private Image _hoverContainer;

    private void Awake()
    {
        _gridContentInventory = GetComponent<GridContentInventory>();
        PopulateInventoryDisplay();
    }

    private void OnEnable()
    {
        _gridContentInventory.OnItemChange += UpdateSlot;
    }

    private void OnDisable()
    {
        _gridContentInventory.OnItemChange -= UpdateSlot;
    }

    private void OnValidate()
    {
        if (_gridContentInventory == null)
            return;
        
        DeleteInventoryDisplay();
        PopulateInventoryDisplay();
    }

    private void DeleteSlot(InventoryDisplaySlot inventoryDisplaySlot)
    {
        Destroy(inventoryDisplaySlot.Item.gameObject);
        Destroy(inventoryDisplaySlot.Outline.gameObject);
        Destroy(inventoryDisplaySlot.Slot.gameObject);
    }

    private void DeleteInventoryDisplay()
    {
        foreach (InventoryDisplaySlot inventorySlot in _inventoryDisplaySlots)
            DeleteSlot(inventorySlot);
        
        _inventoryDisplaySlots.Clear();
    }

    private void PopulateInventoryDisplay()
    {
        //_rectInitialPosition = _inventorySlotSprite.rect.position / 2;
        
        //Temp >>
        _hoverContainer = InstantiateInventoryComponent
            (_inventoryCanvas.gameObject, Vector2.zero, DISPLAY_HOVER_TEXT, false);
        _hoverContainer.color = Color.red;
        //Temp <<

        currentHorizontalSpacing = 0.0f;
        currentVerticalSpacing = 0.0f;

        currentVerticalSpacing += _rectSpacing.y;

        for (int y = 0; y < _gridContentInventory.Columns; y++)
        {
            currentVerticalSpacing -= _rectSpacing.y;
            currentHorizontalSpacing = -_rectSpacing.x;
            
            for (int x = 0; x < _gridContentInventory.Rows; x++)
            {
                currentHorizontalSpacing += _rectSpacing.x;

                Vector2 currentSlotPosition = new Vector2
                    (_rectInitialPosition.x + currentHorizontalSpacing,
                        _rectInitialPosition.y + currentVerticalSpacing);

                InstantiateInventorySlot(x, y, currentSlotPosition);
            }
        }
    }

    private void InstantiateInventorySlot(int x, int y, Vector2 currentSlotPosition)
    {
        InventoryDisplaySlot inventoryDisplaySlot = new InventoryDisplaySlot();

        GameObject inventorySlot = new GameObject($"{DISPLAY_PARENT_TEXT} [{y}] [{x}]");
        inventorySlot.transform.SetParent(_inventoryCanvas.transform);

        inventorySlot.AddComponent<InventoryDisplaySlotContainer>().SlotIndex =
            _gridContentInventory.GetSlotIndex(x, y);

        Image slotImage = InstantiateInventoryComponent
            (inventorySlot, currentSlotPosition, DISPLAY_SLOT_TEXT);

        Image outlineImage = InstantiateInventoryComponent
            (inventorySlot, currentSlotPosition, DISPLAY_OUTLINE_TEXT);
        
        Image itemImage = InstantiateInventoryComponent
            (inventorySlot, currentSlotPosition, DISPLAY_ITEM_TEXT);

        inventoryDisplaySlot.Slot = slotImage;
        inventoryDisplaySlot.Outline = outlineImage;
        inventoryDisplaySlot.Item = itemImage;
        
        _inventoryDisplaySlots.Add(inventoryDisplaySlot);
    }
    
    private Image InstantiateInventoryComponent
        (GameObject parentObject, Vector2 currentPosition, 
            string componentName, bool isActive = true)
    {
        GameObject partObject = new GameObject(componentName);
        Image partImage = partObject.AddComponent<Image>();
        partImage.sprite = _inventorySlotSprite;
        partImage.rectTransform.position = currentPosition;
        partImage.rectTransform.localScale = _rectScale;
        partImage.transform.SetParent(parentObject.transform);
        partImage.gameObject.SetActive(isActive);

        return partImage;
    }

    private void UpdateSlot(InventorySlot itemSlot, int slotIndex)
    {
        InventoryDisplaySlot displaySlot = _inventoryDisplaySlots[slotIndex];
        
        displaySlot.Item.sprite = itemSlot.Item.Icon;
        displaySlot.Item.sprite = itemSlot.Item.Icon;
        displaySlot.Item.sprite = itemSlot.Item.Icon;
    }
}
