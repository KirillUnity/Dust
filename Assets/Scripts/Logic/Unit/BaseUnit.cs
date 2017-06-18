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

    public Vector2 GetPos()
    {
        return pos;
    }

    public void Walk(Cell cell, Vector2 pos, bool close = true)
    {
        Debug.Log(anim.ToString());
        anim.SetFloat("Blend", 0.5f);
        cell.SetClose(close);
        gameObject.transform.position = cell.gameObject.transform.position + Vector3.up / 4;
        this.pos = pos;
        StartCoroutine(IdelAnim());
    }
    IEnumerator IdelAnim(bool die=false)
    {
        yield return new WaitForSeconds(0.5f);

        if (die)
        {
            gameObject.SetActive(false);
            if (type == UnitType.Player)
            {
                GameController.GameOver();
            }
        }
        else
            anim.SetFloat("Blend", 1f);

    }

    public void Atack(Unit unit)
    {
        anim.SetFloat("Blend", 0.25f);
        StartCoroutine(IdelAnim());
        unit.hp -= damage;

      //  

        if (unit.hp <= 0)
        {
            unit.anim.SetFloat("Blend", 0.75f);
            DeathUnit death = unit as DeathUnit;
            death.Death();
            try
            {
                MovePlaer move = (Player)unit;
                Debug.Log("GameOver");
                GameController.instanse.SetLifeText(0);

            }
            catch
            {
                GameController.instanse.GetCells().Find(v => v.pos == unit.pos).SetClose(false);
                GameController.instanse.removeUnit(unit);
            }

        }else
        {
          //  unit.anim.SetFloat("Blend", 0);
            unit.Defense();
        }

    }

    public void Death()
    {
        anim.SetFloat("Blend", 0.75f);
        StartCoroutine(IdelAnim(true));
        //   gameObject.SetActive(false);
    }

    public void Defense()
    {
        if(type==UnitType.Player)
            GameController.instanse.SetLifeText(hp);

        anim.SetFloat("Blend", 0);
        StartCoroutine(IdelAnim());
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