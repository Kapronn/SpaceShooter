using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damageDealer = 100;
    
    
    public int GetDamageDealer()
    {
        return damageDealer;
    }
    public void Hit()
    {
        Destroy(gameObject);
    }
}
