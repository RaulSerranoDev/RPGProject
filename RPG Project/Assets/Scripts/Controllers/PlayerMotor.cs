using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Mueve al personaje utilizando el NavMeshAgent
/// </summary>
[RequireComponent(typeof(NavMesh))]
public class PlayerMotor : MonoBehaviour
{
    private Transform target;   //Target al que sigue
    private NavMeshAgent agent; //Referencia al agente

    //Obtiene referencias
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    //TODO: Se podría hacer en una corrutina mejor
    private void Update()
    {
        //Si tenemos un target, establecemos al agente el destino y hacemos que le mire
        if (target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget(); 
        }
    }

    /// <summary>
    /// Establece el destino del agente
    /// </summary>
    /// <param name="point"></param>
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    /// <summary>
    /// Empieza a seguir a un target
    /// </summary>
    /// <param name="newTarget"></param>
    public void FollowTarget(Interactable newTarget)
    {
        //Cuando el personaje llega al radio, deja de seguir al objeto
        agent.stoppingDistance = newTarget.Radius* .8f;

        //Para que el personaje mire al objeto aunque haya llegado
        agent.updateRotation = false;

        target = newTarget.InteractionTransform;
    }

    /// <summary>
    /// Deja de seguir a un target
    /// </summary>
    public void StopFollowingTarget()
    {
        //Limpiamos los valores que hemos utilizado en follow
        agent.stoppingDistance = 0;
        agent.updateRotation = true;

        target = null;
    }

    /// <summary>
    /// Nos aseguramos de mirar al target
    /// Mira hacia el target iterativamente hasta estar de frente
    /// </summary>
    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z)); //No queremos que rote en y
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime * 5f); //TODO: PUBLIC VARIABLE
    }
}
