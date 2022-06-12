

using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    // Initializing variables
    public Tilemap tilemap { get; private set; }
    public TetrominoData[] tetrominoes;
    public Piece currentPiece { get; private set; }
    public Vector3Int piecePosition = new Vector3Int(-1, 8, 0);
    public Vector2Int boxBorder = new Vector2Int(10, 20);

    public RectInt Borders
    {
        get
        {
            Vector2Int position = new Vector2Int(-this.boxBorder.x / 2, -this.boxBorder.y / 2);
            return new RectInt(position, this.boxBorder);
        }
    }
    //Awake is called once the script has being loaded
    private void Awake()
    {   
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.currentPiece = GetComponentInChildren<Piece>();
        for (int i = 0; i < this.tetrominoes.Length; i++)
        {
            this.tetrominoes[i].Initialize();
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        GetPiece();
    }
    //GetPiece is called once the game has started, this method generate a random block
    public void GetPiece()
    {
        int pieceRandom = Random.Range(0, this.tetrominoes.Length);
        TetrominoData data = this.tetrominoes[pieceRandom];
        this.currentPiece.Initialize(this, this.piecePosition,data);
        //setting the generated block on the the frame
        Set(this.currentPiece);
    }
    //Set is called once the block has being generated
    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int blockPosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(blockPosition, piece.data.block);
        }  
    }
    public void CleanBlock(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int blockPosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(blockPosition, null);
        }  
    }
    public bool IsAGoodPosition(Piece piece, Vector3Int position)
    {
        RectInt borders = this.Borders;
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int thisPosition = piece.cells[i] + position;
            if (!Borders.Contains((Vector2Int)thisPosition))
            {
                return false;
            }
            if (this.tilemap.HasTile(thisPosition))
            {
                return false;
            }
        }
        return true;
    }
}
