using UnityEngine;
using System;
using System.Collections.Generic;
using Data;
using Newtonsoft.Json;

public class GameController : MonoBehaviour {

    public static GameController instanse;

    [SerializeField]
    private long level = 1;

    public int unitCount;
    public int itemCount;

    public FieldContainer conteiner;
    private String path = "Resources/Json/level.json";
    private Dictionary<Vector2, Cell> AllCells = new Dictionary<Vector2, Cell>();

    // Use this for initialization
    void Awake () {
        instanse = this;
        var fields = JsonConvert.DeserializeObject<List<FieldContainer>>(FileWriter.Read(path));
        conteiner = fields.Find(m => m.Field.Id == level);

        itemCount = conteiner.currentItem;
        unitCount = conteiner.currentItem;
    }

    public void AddCellToDictionary(Vector2 vector, Cell cell)
    {
        AllCells.Add(vector, cell);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
