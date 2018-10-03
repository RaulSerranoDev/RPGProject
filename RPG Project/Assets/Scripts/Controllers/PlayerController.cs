using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    #region Inspector
    public Interactable Focus;  //Focus actual: Item, Enemy, etc.
    public LayerMask MovementMask; //Filtrar en que layers se puede mover el personaje
    #endregion

    #region References
    private Camera cam;
    private PlayerMotor motor;
    #endregion

    //Obtiene referencias
    void Start () {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {
        //Si estamos poniendo el puntero del ratón sobre el UI, para que no pueda mover el personaje
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        //Click izquierdo para moverse
		if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit,100,MovementMask))
            {
                //Debug.Log("We hit: " + hit.collider.name + " " + hit.point);
                motor.MoveToPoint(hit.point);   // Mover al jugador

                //Stop focus de cualquier objeto
                RemoveFocus();
            }
        }

        //Click derecho para interactuar con objetos
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                //Comprobar si chocamos con un objeto interactuable
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                //Si lo hace, hacerlo nuestro focus
                if (interactable != null)
                    SetFocus(interactable);
            }
        }
    }

    /// <summary>
    /// Establece un nuevo focus
    /// </summary>
    /// <param name="newFocus"></param>
    private void SetFocus(Interactable newFocus)
    {
        //Comprobar si es un focus Distinto
        if (Focus != newFocus)
        {
            //Comprobar si ya había focus, para informar al new Focus de que va a ser cambiado
            if (Focus != null)
                Focus.OnDefocused();

            //Establecemos nuevo focus e informamos al motor de que siga al nuevo focus
            Focus = newFocus;
            motor.FollowTarget(newFocus);

        }
        Focus.OnFocused(transform);
    }

    /// <summary>
    /// Elimina el focus actual.
    /// </summary>
    private void RemoveFocus()
    {
        // Informa al focus de que ya no le están haciendo focus.
        if (Focus != null)
            Focus.OnDefocused();

        Focus = null;       /// Establece ningún Focus.

        motor.StopFollowingTarget();    /// Informa al motor que pare de seguir al Target
    }
}
