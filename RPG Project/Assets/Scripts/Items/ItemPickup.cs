using UnityEngine;

public class ItemPickup : Interactable {

    /// <summary>
    /// Objeto que se pondrá en el inventario cuando sea cogido
    /// </summary>
    public Item Item;

    /// <summary>
    /// Es llamado cuando el objeto es cogido por el player
    /// </summary>
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    /// <summary>
    /// Coge el objeto y lo añade al inventario
    /// </summary>
    private void PickUp()
    {
        Debug.Log("Picking up " + Item.Name);

        //Si lo puede coger, lo elimina de la escena y lo añade al inventario
        if (Inventory.Instance.Add(Item))
            Destroy(gameObject);
    }
}
