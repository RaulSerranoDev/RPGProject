using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase usada para todos los atributos que queremos que nos permitan añadir o eliminar modificadores
/// </summary>
[System.Serializable]
public class Stat  {

    [SerializeField]
    private int baseValue; //Valor inicial 

    /// <summary>
    /// Lista de todos los modificadores que cambian el valor inicial
    /// </summary>
    private List<int> modifiers = new List<int>();

    /// <summary>
    /// Nos da el valor final con todos los modificadores
    /// </summary>
    /// <returns></returns>
    public int GetValue()
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);

        return finalValue;
    }

    /// <summary>
    /// Añade un nuevo modificador
    /// </summary>
    /// <param name="modifier"></param>
    public void AddModifier(int modifier)
    {
        if (modifier != 0)
            modifiers.Add(modifier);
    }

    //Elimina un modificador
    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
            modifiers.Remove(modifier);
    }
}
