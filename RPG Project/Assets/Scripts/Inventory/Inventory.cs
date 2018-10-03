using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory Instance;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        Instance = this;
    }

    #endregion

    #region Inspector
    /// <summary>
    /// Cantidad de slots en el inventario
    /// </summary>
    public int Space = 20;

    /// <summary>
    /// Lista actual con los objetos en el inventario
    /// </summary>
    public List<Item> Items = new List<Item>();
    #endregion

    #region Callback

    /// <summary>
    /// Callback que se activa cuando un item es añadido o eliminado
    /// </summary>
    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    /*
     Son listeners en Unity, informan a todos los objetos que implementen la función: 
     Hay un ejemplo en InventoryUI.cs
     Cuando llamamos desde aquí a OnItemChangedCallBack, informa a inventoryUI y a todos los suscritos,
     haciendo el método allí implementado
     */
    #endregion

    /// <summary>
    /// Añade un objeto al inventario si hay hueco
    /// Devuelve si se ha conseguido introducir el objeto en el inventario.
    /// Informa todos los observers de cuando cambia un objeto
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Add(Item item)
    {
        if (!item.IsDefaultItem)
        {
            if (Items.Count >= Space)
            {
                Debug.Log("Espacio insuficiente");
                return false;
            }
            Items.Add(item);

            //Informamos a todos los observers
            if (OnItemChangedCallback != null)
                OnItemChangedCallback.Invoke();
        }

        return true;
    }

    /// <summary>
    /// Elimina un objeto del inventario.
    /// Informa todos los observers de cuando cambia un objeto
    /// </summary>
    /// <param name="item"></param>
    public void Remove(Item item)
    {
        Items.Remove(item);

        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    }
}
