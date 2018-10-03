using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mantiene el seguimiento de la equipación.
/// Tiene funciones para añadir y eliminar items
/// </summary>
public class EquipmentManager : MonoBehaviour {

    #region Singleton
    public static EquipmentManager Instance;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of EquipmentManager found!");
            return;
        }
        Instance = this;
    }

    #endregion

    #region Callback
    /*Callback cuando un objeto es equipado/desequipado*/
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged OnEquipmentChangedCallback;
    #endregion

    /// <summary>
    /// Equipo por defecto que tiene el personaje
    /// </summary>
    public Equipment[] defaultEquipment;

    /// <summary>
    /// Objetos que tiene actualmente equipados
    /// </summary>
    private Equipment[] currentEquipment;

    /// <summary>
    /// Referencia al inventario
    /// </summary>
    private Inventory inventory;

    private void Start()
    {
        inventory = Inventory.Instance; //Obtiene referencias

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;  //Número de slots que existen

        currentEquipment = new Equipment[numSlots];

        //Equipamos los items por defecto
        EquipDefaults();
    }

    /// <summary>
    /// Equipa todos los objetos por defecto
    /// </summary>
    private void EquipDefaults()
    {
        foreach (Equipment e in defaultEquipment)
            Equip(e);
    }

    /// <summary>
    /// Es llamado cuando equipamos un nuevo item (Al pulsar en él desde el inventario)
    /// </summary>
    /// <param name="newItem"></param>
    public void Equip(Equipment newItem)
    {
        int slotIndex = (int) newItem.EquipSlot;    //Obtenemos a qué slot pertenece el objeto

        Equipment oldItem = currentEquipment[slotIndex];

        //Detectar si ya hay un objeto en ese slot para desequiparlo
        if (oldItem != null)
            Unequip(slotIndex);

        //Informamos al callback
        if (OnEquipmentChangedCallback != null)
            OnEquipmentChangedCallback.Invoke(newItem, oldItem);

        //Insertamos el objeto en el slot
        currentEquipment[slotIndex] = newItem;
    }

    /// <summary>
    /// Desequipa un objeto recibido por un index
    /// </summary>
    /// <param name="slotIndex"></param>
    public void Unequip(int slotIndex)
    {
        Equipment oldItem = currentEquipment[slotIndex];

        //Si hay algún objeto, lo desequipa
        if (oldItem != null)
        {
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (OnEquipmentChangedCallback != null)
                OnEquipmentChangedCallback.Invoke(null, oldItem);
        }
    }

    /// <summary>
    /// Desequipa todos los objetos y equipa los objetos por defecto
    /// </summary>
    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
            Unequip(i);

        EquipDefaults();
    }

    /// <summary>
    /// TODO: Debug. Desequipa todos los objetos con la U
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }
}
