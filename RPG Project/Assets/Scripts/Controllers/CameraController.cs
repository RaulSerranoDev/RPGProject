using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hace que la cámara siga al personaje
/// </summary>
public class CameraController : MonoBehaviour
{
    #region Inspector
    public Transform Target;     //A quién sigue

    public Vector3 Offset;       //Offset desde el jugador

    public float ZoomSpeed = 4f; //Velocidad a la que hace zoom
    public float MinZoom= 5f;
    public float MaxZoom= 15f;

    public float Pitch = 2f;    //Altura del personaje

    public float YawSpeed = 100f;   //Velocidad de la rotación
    #endregion

    #region Private
    private float currentZoom = 10f;
    private float currentYaw = 0f;
    #endregion

    private void Update()
    {
        //Detecta input para el zoom
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, MinZoom, MaxZoom);

        //Detecta input para la rotación
        currentYaw -= Input.GetAxis("Horizontal") * YawSpeed * Time.deltaTime;
    }
    void LateUpdate()
    {
        //Movimiento
        transform.position = Target.position - Offset * currentZoom;

        //Mira a la cabeza del personaje
        transform.LookAt(Target.position + Vector3.up * Pitch);

        //Rotación
        transform.RotateAround(Target.position, Vector3.up, currentYaw);
    }
}
