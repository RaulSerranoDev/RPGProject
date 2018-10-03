using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Un item que puede ser equipado
/// </summary>
[CreateAssetMenu(fileName = ("New Equipment"), menuName = "Inventory/Equipment")]
public class Equipment : Item {

    /// <summary>
    /// Slot en el que se puede equpar
    /// </summary>
    public EquipmentSlot EquipSlot;

    //Atributos
    public int ArmorModifier;
    public int DamageModifier;

    /// <summary>
    /// Cuando es pulsado en el inventario.
    /// Lo equipa y lo elimina del inventario
    /// </summary>
    public override void Use()
    {
        base.Use();
        EquipmentManager.Instance.Equip(this);
        RemoveFromInventory();
    }

}

public enum EquipmentSlot
{
    HEAD,CHEST,LEGS,WEAPON,SHIELD,FEET
}