using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    private float delay = 0.5f; //
    private int power = 10;

    private GameObject owner;

    public Weapon(GameObject owner)
    {
        this.owner = owner;
    }

    public IEnumerator Fire(GameEntity target)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("trying to attack");
        
    }
}
