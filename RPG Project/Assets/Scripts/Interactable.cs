using UnityEngine;

/// <summary>
/// Componente para todos los objetos con los que el player
/// puede interactuar como enemigos,items,etc. Es usada como una clase base
/// </summary>
public class Interactable : MonoBehaviour
{
    #region Inspector
    /// <summary>
    /// Distancia a la que tiene que estar el objeto para que el personaje pueda interactuar con él
    /// </summary>
    public float Radius = 3f;

    /// <summary>
    /// Punto de interacción con el objeto(Util en una puerta por ejemplo)
    /// </summary>
    public Transform InteractionTransform;
    #endregion

    #region Private
    /// <summary>
    /// Variable que guarda si el objeto está siendo en este momento focuseado
    /// </summary>
    private bool isFocus = false;

    /// <summary>
    /// Referencia al jugador
    /// </summary>
    private Transform player;

    /// <summary>
    /// Guarda si el objeto ya ha sido interactuado una vez
    /// </summary>
    private bool hasInteracted = false;
    #endregion

    /// <summary>
    /// Es llamado cuando el player llega a la distancia para interactuar
    /// </summary>
    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }

    private void Update()
    {
        //Cuando el personaje llega al objeto, interactua con él
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.transform.position, InteractionTransform.position);

            if (distance <= Radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    /// <summary>
    /// Llamado cuando el objeto empieza a ser focuseado.
    /// (Click derecho sobre el)
    /// </summary>
    /// <param name="playerTransform"></param>
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    /// <summary>
    /// Llamado cuando el objeto deja de ser focuseado.
    /// (Click sobre otra parte)
    /// </summary>
    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    /// <summary>
    /// Dibuja en el editor el radio de interacción
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (InteractionTransform == null)
            InteractionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(InteractionTransform.position, Radius);
    }
}
