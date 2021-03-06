﻿using UnityEngine;
using System.Collections.Generic;

public class SimpleMonster : Unit,  SerchPlayer
{
  
    private int currentSteps = 1;

    private List<Cell> cellsNoClose = new List<Cell>();
    private List<Cell> cells = new List<Cell>();

    public void Init(int damage, int hp, Vector2 pos, Animator anim) 
    {
        base.Init(damage, hp, pos, anim);
       
    }

    public Vector2 GetPos()
    {
        return base.GetPos();
    }


    public bool Atack()
    {
        Vector2 posMob = GetPos();
        bool atack = false;
        Player player = GameController.instanse.player;
        cells = new List<Cell>() {
            GameController.instanse.GetCells().Find(v => v.pos == new Vector2(posMob.x, posMob.y - currentSteps)),
            GameController.instanse.GetCells().Find(v => v.pos == new Vector2(posMob.x, posMob.y + currentSteps)),
            GameController.instanse.GetCells().Find(v => v.pos == new Vector2(posMob.x + currentSteps, posMob.y)),
            GameController.instanse.GetCells().Find(v => v.pos == new Vector2(posMob.x - currentSteps, posMob.y))
        };

        foreach (Cell cell in cells)
        {
            if (cell != null && cell == GameController.instanse.GetCells().Find(v => v.pos == player.pos))
            {
                Debug.Log("Atack");
                base.Atack(player);
                atack = true;
            }

            if (cell != null && !cell.GetClose() && !cell.GetExit())
            {
                cellsNoClose.Add(cell);
            }
        }
        return atack;

    }

    void SerchPlayer.Serch()
    {
        bool atack = false;
        cellsNoClose = new List<Cell>();

        if (!Atack())
        {
            if (!atack && cellsNoClose.Count > 0)
            {
                Vector2 posMob = GetPos();
                GameController.instanse.GetCells().Find(v => v.pos == new Vector2(posMob.x, posMob.y)).SetClose(false);
                Cell cell = cellsNoClose[Random.Range(0, cellsNoClose.Count)];
                base.Walk(cell, cell.pos);
                Atack();
            }
        }
    }


}
