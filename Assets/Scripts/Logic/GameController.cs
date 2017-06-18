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
    private Player plaer;
    private Unit[] units;
    List<Cell> AllCells = new List<Cell>();
    [SerializeField]
    private long level = 1;

    public int unitCount;
    public int itemCount;

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
       this.units = units;
    }

    public void AddPlayer(Player plaer)
    {
        this.plaer = plaer;
        move = plaer;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 swipe = touchPosition - Input.mousePosition;

            if (Mathf.Abs(swipe.x) > dragDistance)
            {
                if (swipe.x > 0)
                    move.Move(SwipeDirection.Left);
                else
                    move.Move(SwipeDirection.Rihgt);
            }
          
            if (Mathf.Abs(swipe.y) > dragDistance)
            {
                if (swipe.y > 0)
                    move.Move(SwipeDirection.Down);
                else
                    move.Move(SwipeDirection.Up);
            }
        }


        //foreach (Touch touch in Input.touches)  //используем цикл для отслеживания больше одного свайпа
        //{ //должны быть закоментированы, если вы используете списки 
        //  /*if (touch.phase == TouchPhase.Began) //проверяем первое касание
        //  {
        //      fp = touch.position;
        //      lp = touch.position;

        //  }*/

        //    if (touch.phase == TouchPhase.Moved) //добавляем касания в список, как только они определены
        //    {
        //        touchPositions.Add(touch.position);
        //    }

        //    if (touch.phase == TouchPhase.Ended) //проверяем, если палец убирается с экрана
        //    {
        //        //lp = touch.position;  //последняя позиция касания. закоментируйте если используете списки
        //        fp = touchPositions[0]; //получаем первую позицию касания из списка касаний
        //        lp = touchPositions[touchPositions.Count - 1]; //позиция последнего касания

        //        //проверяем дистанцию перемещения больше чем 20% высоты экрана
        //        if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
        //        {//это перемещение
        //         //проверяем, перемещение было вертикальным или горизонтальным 
        //            if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
        //            {   //Если горизонтальное движение больше, чем вертикальное движение ...
        //                if ((lp.x > fp.x))  //Если движение было вправо
        //                {   //Свайп вправо
        //                    Debug.Log("Right Swipe");
        //                }
        //                else
        //                {   //Свайп влево
        //                    Debug.Log("Left Swipe");
        //                }
        //            }
        //            else
        //            {   //Если вертикальное движение больше, чнм горизонтальное движение
        //                if (lp.y > fp.y)  //Если движение вверх
        //                {   //Свайп вверх
              
        //                    //     Debug.Log("Up Swipe");
        //                }
        //                else
        //                {   //Свайп вниз
        //                    Debug.Log("Down Swipe");
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {   //Это ответвление, как расстояние перемещения составляет менее 20% от высоты экрана

        //    }
        //}
    }
}
