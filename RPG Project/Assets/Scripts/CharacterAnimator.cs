using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour {

    private const float LOCOMOTIONANIMATIONSMOOTHTIME = .1f;

    private Animator animator;  //Referencia al animatorComponent
    private NavMeshAgent agent;   //Para acceder a la velocidad

	void Start () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
	}
	
	void Update () {
        //Obtenemos el porcentaje de velocidad
        float speedPercent = agent.velocity.magnitude / agent.speed;

        //Le damos la velocidad al animator
        animator.SetFloat("speedPercent", speedPercent, LOCOMOTIONANIMATIONSMOOTHTIME, Time.deltaTime);
	}
}
