
using System;
using UnityEngine;

public class Piece : MonoBehaviour
{ 
    // Initializing variables
  public Board board { get; private set; }
  public TetrominoData data { get; private set; }
  public Vector3Int position { get; private set; }
  public Vector3Int[] cells { get; private set; }
  public int rotateDirection { get; private set; }
  
  
  //Initializing the block on the board
  public void Initialize(Board board, Vector3Int position, TetrominoData data)
  {
      this.board = board;
      this.position = position;
      this.data = data;
      this.rotateDirection = 0;
      if (this.cells == null)
      {
        this.cells = new Vector3Int[data.cells.Length];
      }
      for (int i = 0; i < data.cells.Length; i++)
      {
          this.cells[i] = (Vector3Int)data.cells[i];
      }
  }
// Update is called once per frame
  public void Update()
  {
      //clear the block if position at a wrong position
      this.board.CleanBlock(this);

        //control the rotation of the block
      if (Input.GetKeyDown(KeyCode.Z))
      {
          Rotation(-1);
      }else if (Input.GetKeyDown(KeyCode.A))
      {
          Rotation(1);
      }
      
      //check the keys press by the player
      if (Input.GetKeyDown(KeyCode.L))
      {   //Move the block one step to the left if the key L is pressed
          Mouvement(Vector2Int.left);
      }else if (Input.GetKeyDown(KeyCode.R))
      {   //Move the block one step to the right if the key R is pressed
          Mouvement(Vector2Int.right);
      }
      if (Input.GetKeyDown(KeyCode.D))
      {   //Move the block one step down if the key S is pressed
          Mouvement(Vector2Int.down); 
      }
      if (Input.GetKeyDown(KeyCode.Space))
      {   //drop the block
          while (Mouvement(Vector2Int.down))
          {
              continue;
          }
      }
      //set the block on the board at the right position
      this.board.Set(this);
  }
//controlling the movement of the block on the board
  public bool Mouvement(Vector2Int translation)
  {
      Vector3Int thePosition = this.position;
      thePosition.x += translation.x;
      thePosition.y += translation.y;
      //check if the block be set at the next position
      bool allowedPosition = this.board.IsAGoodPosition(this, thePosition);
      if (allowedPosition)
      {   
          this.position = thePosition;
      }
      return allowedPosition;
  }
  //Rotation is called if the player wants to rotate the block
  private void Rotation(int direction)
  {
      this.rotateDirection += direction;
  }
  
  //Wrap handle the rotation direction of the block
  private int Wrap(int input, int min, int max)
  {
      if (input < min) {
          return max - (min - input) % (max - min);
      } else {
          return min + (input - min) % (max - min);
      }
  }
}