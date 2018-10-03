using UnityEngine;

/// <summary>
/// Clase base que player y enemigos pueden derivar para incluir nuevos stats
/// </summary>
public class CharactersStats : MonoBehaviour
{
    //Vida
    public int MaxHealth = 100;
    public int CurrentHealth { get; private set; }

    public Stat Damage;
    public Stat Armor;

    /// <summary>
    /// Establece la vida inicial del personaje a la máxima
    /// </summary>
    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    //TODO: ONLY FOR DEBUG
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            TakeDamage(10);
    }

    /// <summary>
    /// Es llamado cuando el personaje recibe daño
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        //Actualizamos el daño con la defensa
        damage -= Armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        //Hacemos daño al personaje
        CurrentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        //Comprobamos si ha muerto
        if (CurrentHealth <= 0)
            Die();
    }

    public virtual void Die()
    {
        //Este método tiene que ser sobreescrito por un hijo
        Debug.Log(transform.name + " died." );

    }
}
