using UnityEngine;
using System.Collections;
using System;

public class Player : Unit, MovePlaer
{
    public Vector2 pos;

    private int currentSteps = 1;

    public void Atack(Unit unit)
    {
        base.Atack(unit);
    }

    // Use this for initialization
    public void Init (int damage, int hp, Vector2 position ) {
        base.Init(damage, hp, position);
        pos = position;
        GameController.instanse.AddPlayer(this);
    }

    public void Move(SwipeDirection Type)
    {
        Cell cell = new Cell();

        switch (Type)
        {
            case SwipeDirection.Up:
                cell = GameController.instanse.GetCells().Find(v => v.pos == new Vector2(pos.x, pos.y - currentSteps));
                break;

            case SwipeDirection.Down:
                cell = GameController.instanse.GetCells().Find(v => v.pos == new Vector2(pos.x, pos.y + currentSteps));
                break;

            case SwipeDirection.Left:
                cell = GameController.instanse.GetCells().Find(v => v.pos == new Vector2(pos.x + currentSteps, pos.y ));
                break;

            case SwipeDirection.Rihgt:
                cell = GameController.instanse.GetCells().Find(v => v.pos == new Vector2(pos.x - currentSteps, pos.y ));
                break;
        }

        if (cell != null && !cell.GetClose())
        {
            GameController.instanse.playerCanGo = false;
            if (cell.GetExit())
            {
                Debug.Log("Win");
            }

            pos = cell.pos;
            gameObject.transform.position = cell.gameObject.transform.position + Vector3.up / 4;

        }

        else if (cell != null)
        {try
            {
                foreach (Unit unit in GameController.instanse.GetUnits())
                {
                    if (unit.GetType() == UnitType.SimpleMob)
                    {

                        SimpleMonster monster = (SimpleMonster)unit;

                        if (cell == GameController.instanse.GetCells().Find(v => v.pos == monster.GetPos()))
                        {
                            Atack(unit);
                        }

                    }
                }
            }
            catch { }
        }
    }
}

interface MovePlaer
{
    void Move(SwipeDirection Type);
}

public enum SwipeDirection{
Left, Up, Down, Rihgt
}
