using UnityEngine;
using System.Collections;
using System;

public abstract class Unit : MonoBehaviour, DeathUnit {
    
    private int damage;
    private int hp;
    Vector2 pos;

    public void Init(int damage, int hp, Vector2 pos)
    {
        this.damage = damage;
        this.hp = hp;
        this.pos = pos;
    }

    public void Atack(Unit unit)
    {
        unit.hp -= damage;

        if (unit.hp <= 0)
        {
            DeathUnit death = unit as DeathUnit;
            death.Death();
        }
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }

    public void Defense()
    {
    }

}

public interface SimpLeSerchPlayer
{
    void Serch();
}

public interface DeathUnit
{
    void Death();
}