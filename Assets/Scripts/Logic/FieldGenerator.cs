﻿using UnityEngine;
using System;
using System.Collections.Generic;

public class FieldGenerator : MonoBehaviour {

    public Transform tilePrefab;
    public Transform mobPrefab;
    public Transform plaierPrefab;
    public Transform exitPrefab;

    [SerializeField]
    public Vector2 size;

    public Unit unit;
    List<Cell> cells = new List<Cell>();

    // Use this for initialization
    void Start () {
        size.y = GameController.instanse.conteiner.Field.Height;
        size.x = GameController.instanse.conteiner.Field.Width;
        GenerateMap();
    }

    // Update is called once per frame
    void Update () {
	
	}

    [Range(0, 1)]
    public float outlinePercent;

    private void GenerateMap()
    {
        GenerateCell();
        GenerateMob();
        GeneratePlayer();
        GenerateExit();
        GenerateItem();
    }

    public void GenerateExit()
    {
        Cell cell = getClearCeil();
        Transform exit = Instantiate(exitPrefab, cell.gameObject.transform.position, Quaternion.Euler(Vector3.right)) as Transform;
    }

    private void GenerateCell()
    {
        string holderName = "Generated Map";
        if (transform.FindChild(holderName))
        {
            DestroyImmediate(transform.FindChild(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 tilePosition = new Vector3(-size.x / 2 + .5f + x, -size.y / 2 + 0.5f + y, 0);
                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right)) as Transform;
                newTile.localScale = Vector3.one * (1 - outlinePercent);
                newTile.parent = mapHolder;
                Cell cell = newTile.GetComponent<Cell>();
                if (new Vector2(x, y) == Vector2.zero)
                    cell.Init(x, y, null, 1);

                else if (new Vector2(x, y) == new Vector2(0, size.y - 1))
                    cell.Init(x, y, null, 2);

                else if (new Vector2(x, y) == new Vector2(size.x - 1, 0))
                    cell.Init(x, y, null, 3);

                else if (new Vector2(x, y) == new Vector2(size.x - 1, size.y - 1))
                    cell.Init(x, y, null, 4);

                else
                {
                    cell.Init(x, y);
                    cells.Add(cell);
                }

            }

        }
    }
    private void GeneratePlayer()
    {
        Cell cell = getClearCeil();
        Transform player = Instantiate(plaierPrefab, cell.gameObject.transform.position + Vector3.up / 4, Quaternion.Euler(Vector3.right)) as Transform;

        player.gameObject.AddComponent<Player>();
        Player unit = player.gameObject.GetComponent<Player>();
        unit.Init(10, 20, cell.pos);
    }

    private void GenerateMob()
    {
        Vector2[] mobPos = new Vector2[GameController.instanse.conteiner.currentMob];

        for (int i = 0; i < mobPos.Length; i++)
        {
            Cell cell = getClearCeil();
            mobPos[i] = cell.pos;
            Transform mob = Instantiate(mobPrefab, cell.gameObject.transform.position+Vector3.up/4, Quaternion.Euler(Vector3.right)) as Transform;
            mob.gameObject.AddComponent<SimpleMonster>();
            SimpleMonster unit = mob.gameObject.GetComponent<SimpleMonster>();
            unit.Init(10, 20, mobPos[i]);
        }

    }
    private void GenerateItem()
    {
        Vector2[] itemPos = new Vector2[GameController.instanse.conteiner.currentItem];

        for (int i = 0; i < itemPos.Length; i++)
        {
            Cell cell = getClearCeil();
            itemPos[i] = cell.pos;
            Transform[] itemPrefab = Resources.LoadAll<Transform>("Prefabs/Items");
            Transform item = Instantiate(itemPrefab[UnityEngine.Random.Range(0, itemPrefab.Length)], cell.gameObject.transform.position, Quaternion.Euler(Vector3.right)) as Transform;
          //  mob.gameObject.AddComponent<SimpleMonster>();
            //SimpleMonster unit = mob.gameObject.GetComponent<SimpleMonster>();
         //   unit.Init(10, 20, itemPos[i]);
        }

    }

    private Cell getClearCeil()
    {
        Cell cell = cells[UnityEngine.Random.Range(1, cells.Count)];
        cell.isClose = true;
        cells.Remove(cell);
        return cell;
    }
}