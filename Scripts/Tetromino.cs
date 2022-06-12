
using UnityEngine.Tilemaps;
using UnityEngine;

//initializiing the tetrominos has viewed on wikipedia
public enum Tetromino
{
   I,
   O,
   T,
   J,
   L,
   S,
   Z,
}
[System.Serializable]
public struct TetrominoData
{
    public Tetromino tetromino;
    public Tile block;
    public Vector2Int[] cells { get; private set; }
    
    public void Initialize()
    {
        this.cells =Data.Cells[this.tetromino];
    }
}



