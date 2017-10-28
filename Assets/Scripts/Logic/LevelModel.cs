using UnityEngine;
using System;
using System.Collections.Generic;

public class LevelModel : MonoBehaviour {

    public Transform tilePrefab;
    public Transform mobPrefab;
    public Transform plaierPrefab;
    public Transform exitPrefab;

    public Unit unit;
    public List<Cell> cells = new List<Cell>();

    public LevelModel() {
        GenerateMap();
    }

    [Range(0, 1)]
    public float outlinePercent;

    private void GenerateMap()
    {
        GenerateExit();
        GenerateMob();
        GeneratePlayer();
        GenerateItem();
    }

    public void GenerateExit()
    {
        Cell cell = new Cell();

      //  if (GameController.instanse.conteiner.Field.RandomEnterPosition)
            cell = getClearCeil(false);
       // else {
       //     Vector2 exitPos = new Vector2(GameController.instanse.conteiner.Field.EnterPosition.X, GameController.instanse.conteiner.Field.EnterPosition.Y);
       //     cell = GameController.instanse.GetCells().Find(c => c.pos == exitPos);
       // }
       // cells.Remove(cell);
        cell.SetExit();
        Transform exit = Instantiate(exitPrefab, cell.gameObject.transform.position, Quaternion.Euler(Vector3.right)) as Transform;
    }

    private void GeneratePlayer()
    {
        Cell cell = getClearCeil(false);
        Transform player = Instantiate(plaierPrefab, cell.gameObject.transform.position + Vector3.up / 4, Quaternion.Euler(Vector3.right)) as Transform;
        player.gameObject.AddComponent<Player>();
        Player unit = player.gameObject.GetComponent<Player>();
        GameController.instanse.SetLifeText(100);
        unit.Init(10, 100, cell.pos, player.GetComponent<Animator>());
        GameController.instanse.AddPlayer(unit);
    }

    private void GenerateMob()
    {
        Vector2[] mobPos = new Vector2[3];
        Unit[] units = new Unit[mobPos.Length];
        for (int i = 0; i < mobPos.Length; i++)
        {
            Cell cell = getClearCeil();
            mobPos[i] = cell.pos;
            Transform mob = Instantiate(mobPrefab, cell.gameObject.transform.position+Vector3.up/4, Quaternion.Euler(Vector3.right)) as Transform;
            mob.gameObject.AddComponent<SimpleMonster>();
            SimpleMonster unit = mob.gameObject.GetComponent<SimpleMonster>();
            unit.Init(20, 20, mobPos[i], mob.GetComponent<Animator>());
            units[i] = unit;
        }

        GameController.instanse.AddUnits(units);

    }

    private void GenerateItem()
    {
        Vector2[] itemPos = new Vector2[5];

        for (int i = 0; i < itemPos.Length; i++)
        {
            Cell cell = getClearCeil();
            itemPos[i] = cell.pos;
            Transform[] itemPrefab = Resources.LoadAll<Transform>("Prefabs/Items");
            Transform item = Instantiate(
                itemPrefab[UnityEngine.Random.Range(0, itemPrefab.Length)], 
                cell.gameObject.transform.position,
                Quaternion.Euler(Vector3.right)
                ) as Transform;
        }

    }

    private Cell getClearCeil(bool close = true)
    {
        Debug.Log(cells.Count.ToString());
        Cell cell = cells[UnityEngine.Random.Range(1, 5)];
        cell.SetClose(close);
        cells.Remove(cell);
        return cell;
    }
}
