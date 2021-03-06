﻿using UnityEngine;
using System;
using System.Collections.Generic;
using Data;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instanse;

    private MovePlaer move;
    public Player player;

    private Vector3 touchPosition;
    [SerializeField]
    private Text lifeCount;

    private List<Unit> units = new List<Unit>();
    private List<Cell> allCells = new List<Cell>();

    [SerializeField]
    private long level = 1;
    private float dragDistance;
    public int unitCount;
    public int itemCount;

    public bool playerCanGo = true;
    public FieldContainer conteiner;

    public GameController(FieldContainer _conteiner)
    {
        instanse = this;

        conteiner = _conteiner;

        itemCount = conteiner.currentItem;
        unitCount = conteiner.currentItem;
        dragDistance = Screen.height * 20 / 100; //20% высоты экрана
    }

    public void AddCells(Cell  cell)
    {
        allCells.Add(cell);
    }

    public List<Cell> GetCells()
    {
       return allCells;
    }

    public void AddUnits(Unit[] units)
    {
       this.units.AddRange(units);
    }

    public List<Unit> GetUnits()
    {
       return units;
    }

    public void removeUnit(Unit unit)
    {
         units.Remove(unit);
    }

    public void AddPlayer(Player plaer)
    {
        this.player = plaer;
        move = plaer;
    }

    public void SetLifeText(int text)
    {
        lifeCount.text = text + "";
    }

    public static void GameOver()
    {
        Application.LoadLevel(2);
    }

    public static void Win()
    {
        Application.LoadLevel(3);

    }

    void Update()
    {
        if (playerCanGo)
        {
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
                touchPosition = Input.mousePosition;


            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Vector2 swipe = touchPosition - Input.mousePosition;

                if (Mathf.Abs(swipe.x) > dragDistance)
                {
                    if (swipe.x > 0)
                        move.Move(SwipeDirection.Rihgt);
                    else
                        move.Move(SwipeDirection.Left);
                }

               else if (Mathf.Abs(swipe.y) > dragDistance)
                {
                    if (swipe.y > 0)
                        move.Move(SwipeDirection.Up);
                    else
                        move.Move(SwipeDirection.Down);
                }
            }

        } else {
            try
            {
                for (int i = 0; i < units.Count; i++)
                {

                    switch (units[i].GetType())
                    {
                        case UnitType.SimpleMob:
                            SerchPlayer serch = (SimpleMonster)units[i];
                            serch.Serch();
                            break;

                        default:

                            break;
                    }

                }
                playerCanGo = true;
            }
            catch {}
        }
    }
}
