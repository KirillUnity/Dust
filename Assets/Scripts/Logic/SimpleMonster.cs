using UnityEngine;
using System.Collections;

public class SimpleMonster : Unit,  SimpLeSerchPlayer
{
    [SerializeField]
    private Vector2 posMob;

    public void Init(int damage, int hp, Vector2 pos) 
    {
        base.Init(damage, hp, pos);
        posMob = pos;
    }

    void Awake()
    {
      //  Init(2, 2, Vector2.zero);
    }

    public void Atack(Unit unit)
    {
        base.Atack(unit);
    }
    void SimpLeSerchPlayer.Serch()
    {

    }
}
