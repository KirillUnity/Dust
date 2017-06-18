using UnityEngine;
using System;
using System.Collections.Generic;
using Data;
using Newtonsoft.Json;

public class GameController : MonoBehaviour {
    private MovePlaer move;
    private Vector3 touchPosition; 

    private float dragDistance;  

    public static GameController instanse;
    public Player player;
    private List<Unit> units = new List<Unit>();
    List<Cell> AllCells = new List<Cell>();
    [SerializeField]
    private long level = 1;

    public int unitCount;
    public int itemCount;

    public bool playerCanGo = true;

    public FieldContainer conteiner;
    private String path = "Resources/Json/level.json";

    void Awake () {
        instanse = this;
        var fields = JsonConvert.DeserializeObject<List<FieldContainer>>(FileWriter.Read(path));
        conteiner = fields.Find(m => m.Field.Id == level);

        itemCount = conteiner.currentItem;
        unitCount = conteiner.currentItem;

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

                if (Mathf.Abs(swipe.y) > dragDistance)
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
