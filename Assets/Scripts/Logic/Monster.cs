using UnityEngine;
using System.Collections;
using System;

public abstract class Unit : MonoBehaviour, DeathUnit {
    
    private int damage;
    private int hp;
    private Vector2 pos;
    private UnitType type;

    public void Init(int damage, int hp, Vector2 pos, UnitType type = UnitType.SimpleMob)
    {
        this.damage = damage;
        this.hp = hp;
        this.pos = pos;
        this.type = type;
    }

    public UnitType GetType()
    {
        return type;
    }

    public void ChangePos(Vector2 pos)
    {
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
    Warrior
}