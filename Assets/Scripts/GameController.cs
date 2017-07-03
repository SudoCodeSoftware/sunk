using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

  private bool isPlayerTwoTurn;
  private bool ballsActive;
  private bool coloursSet;

  private string playerOneColour;
  private string playerTwoColour;

  private int playerOneScore;
  private int playerTwoScore;

  void Start () {
    isPlayerTwoTurn = false;
    ballsActive = false;
    coloursSet = false;
  }

  void Update () {
    UpdateUI();
  }

  public void SinkBall(GameObject ball) {
    BallController ballController = ball.GetComponent<BallController>();
    CountSunkBall(ballController.ballColour);
  }

  private void CountSunkBall(string sunkColour) {
    if (sunkColour == "white") {
      // For white ball

      // Create new White Ball
//       GameObject newWhiteBall = (GameObject)Instantiate(Resources.Load("WhiteBall"));

      // Change Turns
    }
    else if (sunkColour == "black") {
      // For black ball

      // If all other balls have been sunk by current player
        // Set current player as winner
      // Else
        // Set other player as winner

      // End Game
    }
    else {
      // For colour ball
      // Check if the colours have been set
      if (!coloursSet) {
        if (isPlayerTwoTurn) {
          playerTwoColour = sunkColour;
        }
        else {
          playerOneColour = sunkColour;
        }
        coloursSet = true;
      }
      // Score the sunk ball
      AddBallToScore(sunkColour);
    }
  }

  private void AddBallToScore(string sunkColour) {
    if (playerOneColour == sunkColour) {
      playerOneScore++;
    }
    else {
      playerTwoScore++;
    }
  }

  private void UpdateUI() {
    // Update UI elements to show scores and player colours
  }

}