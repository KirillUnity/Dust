using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using Data;

public class DustApplication : MonoBehaviour {

    private DustModel model;
    private DustView view;
    private GameController controller;

    [SerializeField]
    private Transform center;

    [SerializeField]
    private int level = 1;

    private const string path = "Resources/Json/level.json";


    // Entry point
    void Awake () {
        if (GameController.instanse != null)
            Destroy(GameController.instanse.gameObject);

        var fields = JsonConvert.DeserializeObject<List<FieldContainer>>(FileWriter.Read(path, Application.dataPath));
        controller = new GameController(fields.Find(m => m.Field.Id == level));

        view = new DustView(new Vector3(controller.conteiner.Field.Width, controller.conteiner.Field.Height), center);
    }

}
