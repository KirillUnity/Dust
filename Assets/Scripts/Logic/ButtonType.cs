using UnityEngine;

public class ButtonType : MonoBehaviour
{
    [SerializeField]
    private GameState state;
}

public enum GameState
{
    Pause,
    Play,
    GameOver,
    Win
}