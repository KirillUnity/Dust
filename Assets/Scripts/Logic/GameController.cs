using UnityEngine;
using System;
using System.Collections.Generic;
using Data;
using Newtonsoft.Json;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instanse;
   
    private MovePlaer move;
    public Player player;

    private Vector3 touchPosition;
    [SerializeField]
    private Text lifeCount;

    private List<Unit> units = new List<Unit>();
    private List<Cell> AllCells = new List<Cell>();

    [SerializeField]
    private long level = 1;
    private float dragDistance;

    public bool playerCanGo = true;

    private String path = "Resources/Json/level.json";

    public int unitCount = 3;
    public int itemCount = 5;


    void Awake () {
        instanse = this;
        dragDistance = Screen.height * 10 / 100; //10% высоты экрана
    }

    public void AddCells(Cell  cell)
    {
        AllCells.Add(cell);
    }

    public List<Cell> GetCells()
    {
       return AllCells;
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
            if (Input.GetMouseButtonDown(0))
                touchPosition = Input.mousePosition;


            if (Input.GetMouseButtonUp(0))
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
