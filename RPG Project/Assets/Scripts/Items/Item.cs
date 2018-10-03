using UnityEngine;

/// <summary>
/// Clase base de los objetos. Todos los items derivan de este
/// </summary>
[CreateAssetMenu(fileName = ("New Item"),menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    public string Name = "New Item";
    public Sprite Icon = null;
    public bool IsDefaultItem = false;

    /// <summary>
    /// Es llamado cuando se pulsa en el inventario
    /// </summary>
    public virtual void Use()
    {
        Debug.Log("Using: " + Name);
    }

    public void RemoveFromInventory()
    {
        Inventory.Instance.Remove(this);
    }
}
