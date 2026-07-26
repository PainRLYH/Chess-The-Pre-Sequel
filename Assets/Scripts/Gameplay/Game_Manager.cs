using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    Board board;
    private void Start()
    {
        board = GetComponent<Board>();
        board.CreateBoard();
        board.ArrangePieces();
    }
}
