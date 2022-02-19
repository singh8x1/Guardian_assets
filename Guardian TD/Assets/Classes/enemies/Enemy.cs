using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    public float MaxHealth;
    public float Health;
    public float Speed;
    public int ID;

  public void Init()
    {
        Health = MaxHealth;

    }
}
