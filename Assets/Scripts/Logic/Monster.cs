using UnityEngine;
using System.Collections;
using System;

public abstract class Unit : MonoBehaviour, DeathUnit {
    
    private int damage;
    private int hp;
    private Vector2 pos;
    private UnitType type;
    private Animator anim;

    public void Init(int damage, int hp, Vector2 pos, Animator anim, UnitType type = UnitType.SimpleMob)
    {
        this.anim = anim;
        this.damage = damage;
        this.hp = hp;
        this.pos = pos;
        this.type = type;
    }

    public UnitType GetType()
    {
        return type;
    }

    public void Walk(Cell cell, Vector2 pos)
    {
      //  anim.SetFloat("Blend", 0.5f);
        cell.SetClose(true);
        gameObject.transform.position = cell.gameObject.transform.position + Vector3.up / 4;
        this.pos = pos;
    }

    public void Atack(Unit unit)
    {
        unit.hp -= damage;
        Debug.Log("Unit HP: " + unit.hp);

        if (unit.hp <= 0)
        {
            DeathUnit death = unit as DeathUnit;
            death.Death();
            try
            {
                MovePlaer move = (Player)unit;
                Debug.Log("GameOver");
            }
            catch
            {
                GameController.instanse.GetCells().Find(v => v.pos == unit.pos).SetClose(false);
                GameController.instanse.removeUnit(unit);
            }
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

public interface SerchPlayer
{
    void Serch();
}

public interface DeathUnit
{
    void Death();
}

public enum UnitType
{
    SimpleMob,
    Archer,
    Wizard,
    Warrior,
    Player
}