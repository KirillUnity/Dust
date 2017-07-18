using UnityEngine;

public class DustView : MonoBehaviour {

    private Vector2 mapSize;
    private Transform tilePrefab;
    private Transform center;

    private const string pathPrefab = "Prefabs/Tile/floor";

    const float outlinePercent = 0.3f;

    public DustView(Vector2 _size, Transform _center) {
        mapSize = _size;
        center = _center;
        tilePrefab = Resources.Load<Transform>(pathPrefab);
        GenerateCell();
    }

    private void GenerateCell()
    {
        string holderName = "Generated Map";

        if (center.FindChild(holderName))
            DestroyImmediate(transform.FindChild(holderName).gameObject);

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = center;

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3 tilePosition = new Vector3(-mapSize.x / 2 + .5f + x, -mapSize.y / 2 + 0.5f + y, 0);
                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right)) as Transform;
                newTile.localScale = Vector3.one * (1 - outlinePercent);
                newTile.parent = mapHolder;
                Cell cell = newTile.GetComponent<Cell>();
                if (new Vector2(x, y) == Vector2.zero)
                    cell.Init(x, y, null, 1);

                else if (new Vector2(x, y) == new Vector2(0, mapSize.y - 1))
                    cell.Init(x, y, null, 2);

                else if (new Vector2(x, y) == new Vector2(mapSize.x - 1, 0))
                    cell.Init(x, y, null, 3);

                else if (new Vector2(x, y) == new Vector2(mapSize.x - 1, mapSize.y - 1))
                    cell.Init(x, y, null, 4);

                else
                {
                    cell.Init(x, y);
                    GameController.instanse.AddCells(cell);
                }
            }
        }
    }

}
