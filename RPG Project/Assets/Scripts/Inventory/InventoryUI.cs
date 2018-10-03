using UnityEngine;

/// <summary>
/// Actualiza el UI del inventario
/// </summary>
public class InventoryUI : MonoBehaviour
{
    #region Inspector
    public Transform ItemsParent;   //Objeto padre de todos los objetos
    public GameObject InventoryUIGameObject;    //El UI completo
    #endregion

    #region Private
    private Inventory inventory;    //Referencia al inventario actual
    private InventorySlot[] slots;  //Referencia a la lista de todos los slots
    #endregion

    // Use this for initialization
    void Start()
    {
        inventory = Inventory.Instance;
        slots = GetComponentsInChildren<InventorySlot>();

        //Nos suscribimos a OnItemChanged para que nos informe cada vez que se añade/elimina un objeto al inventario para que haga esta función
        inventory.OnItemChangedCallback += UpdateUI;
    }

    void Update()
    {
        //Para abrir/cerrar el inventario
        if (Input.GetButtonDown("Inventory"))
            InventoryUIGameObject.SetActive(!InventoryUIGameObject.activeSelf);

    }

    /// <summary>
    /// Actualiza el UI del inventario
    /// Recorre todos los slots estableciendo los objetos y los huecos vacios
    /// </summary>
    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            //Si hay un objeto en este hueco lo pinta
            if (i < inventory.Items.Count)  
                slots[i].AddItem(inventory.Items[i]);
            else
                slots[i].ClearSlot();
        }
    }
}
