using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Maneja de los stats del player y añade/elimina modificadores cuando equipa objetos
/// </summary>
public class PlayerStats : CharactersStats {

    private void Start()
    {
        //Nos suscribimos a la función para que nos informe en el método OnEquipmentChanged
        EquipmentManager.Instance.OnEquipmentChangedCallback += OnEquipmentChanged;
    }

    /// <summary>
    /// Es llamado cuando un item es equipado/desequipado
    /// </summary>
    /// <param name="newItem"></param>
    /// <param name="oldItem"></param>
    private void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        //Añade modificadores del equipo nuevo
        if (newItem != null)
        {
            Armor.AddModifier(newItem.ArmorModifier);
            Damage.AddModifier(newItem.DamageModifier);
        }
        //Elimina modificadores del equipo viejo
        if (oldItem != null)
        {
            Armor.RemoveModifier(oldItem.ArmorModifier);
            Damage.RemoveModifier(oldItem.DamageModifier);
        }
    }
}
