using UnityEngine;
using System.Collections;

public class Player : Unit {
    [SerializeField]
    public Vector2 pos;
	// Use this for initialization
	public void Init (int damage, int hp, Vector2 position ) {
        base.Init(damage, hp, position);
        pos = position;
    }

}
