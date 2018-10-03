using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Necesario en todos los slots
/// </summary>
public class InventorySlot : MonoBehaviour {

    #region References
    /// <summary>
    /// Referencia al icono del objeto
    /// </summary>
    public Image Icon;

    /// <summary>
    /// Referencia al botón de deshacer
    /// </summary>
    public Button RemoveButton;
    #endregion

    /// <summary>
    /// Item que hay en el slot
    /// </summary>
    private Item item;

    /// <summary>
    /// Añade un objeto al slot.
    /// Establece su imagen y lo activa
    /// </summary>
    /// <param name="newItem"></param>
    public void AddItem(Item newItem)
    {
        item = newItem;

        Icon.sprite = item.Icon;
        Icon.enabled = true;
        RemoveButton.interactable = true;
    }

    /// <summary>
    /// Limpia el slot
    /// Elimina la imagen y lo desactiva
    /// </summary>
    public void ClearSlot()
    {
        item = null;

        Icon.sprite = null;
        Icon.enabled = false;
        RemoveButton.interactable = false; 
    }


    /// <summary>
    /// Es llamado cuando pulsamos el botón de deshacer.
    /// Destruye el objeto del inventario
    /// </summary>
    public void OnRemoveButton()
    {
        Inventory.Instance.Remove(item);
    }

    /// <summary>
    /// Es llamado cuando se pulsa el item
    /// Utiliza el objeto
    /// </summary>
    public void UseItem()
    {
        if (item != null)
            item.Use();
    }
}
