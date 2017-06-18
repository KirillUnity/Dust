using UnityEngine;
using System.Collections.Generic;

public class Cell : MonoBehaviour {

    public Vector2 pos;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private GameObject border;

    public Sprite[] tileSprites;
    private bool isClose;
    private bool isExit;

    public void Init(int x, int y, GameObject prefab = null, int cornerPos = 0, bool border = false)
    { 
        this.prefab = prefab;
        pos.x = x;
        pos.y = y;
        if (cornerPos > 0 )
            DrawCorner(cornerPos);
        if(border)
            DrawCorner(cornerPos, border);

        GameController.instanse.AddCells(this);

    }


    public void SetClose(bool close)
    {
        isClose = close;
    }

    public bool GetClose()
    {
        return isClose;
    }

    public void SetExit()
    {
        isExit = true;
    }

    public bool GetExit()
    {
        return isExit;
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
