using UnityEngine;
using System.Collections.Generic;

public class Cell : MonoBehaviour {

    [SerializeField]
    public Vector2 pos;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private GameObject border;

    public Sprite[] tileSprites;
    public bool isClose;

   
    public void Init(int x, int y, GameObject prefab = null, int cornerPos = 0, bool border = false)
    { 
        this.prefab = prefab;
        pos.x = x;
        pos.y = y;
        if (cornerPos > 0 )
            DrawCorner(cornerPos);
        if(border)
            DrawCorner(cornerPos, border);

        GameController.instanse.AddCellToDictionary(new Vector2(x, y), this);

    }

    private void DrawCorner(int corner)
    {
        Sprite[] cornerSprites = cornerSprites = Resources.LoadAll<Sprite>("Flor/Leg");
        border.GetComponent< SpriteRenderer > ().sprite = cornerSprites[corner-1];
        border.SetActive(true);

        if (corner > 2)
            border.transform.localPosition=new Vector3(0.8f, border.transform.localPosition.y, border.transform.localPosition.z);

        isClose = true;
    }

    private void DrawCorner(int corner, bool border)
    {
        Sprite[] cornerSprites = null;
        cornerSprites = Resources.LoadAll<Sprite>("Flor/Border");
    }

    // Use this for initialization
    void Awake () {
        tileSprites = Resources.LoadAll<Sprite>("Flor/Tile");
        GetComponent<SpriteRenderer>().sprite = tileSprites[Random.Range(0, tileSprites.Length)];
    }

}
