using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

  public Text playerTurnText;

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

  public static bool allStopped() {
    bool everythingStopped = true;
    Object[] allRigidBodies = GameObject.FindObjectsOfType(typeof(Rigidbody2D));
    
    foreach (Rigidbody2D obj in allRigidBodies) {
      if (obj.velocity.magnitude != 0) {
        everythingStopped = false;
        break;
      }
    }

        return everythingStopped;
  }

  public void SinkBall(GameObject ball) {
    BallController ballController = ball.GetComponent<BallController>();
    CountSunkBall(ballController.ballColour);
  }

  private void CountSunkBall(string sunkColour) {
    if (sunkColour == "white") {
      // For white ball
      // Switch turns
      SwapTurns();
      // Create new White Ball
      Instantiate(Resources.Load("WhiteBall"), Vector3.zero, Quaternion.identity);

      // TODO Change Turns
    }
    else if (sunkColour == "black") {
      // For black ball

      // TODO
      // If all other balls have been sunk by current player
        // Set current player as winner
      // Else
        // Set other player as winner

      // TODO
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
    // TODO Update UI elements to show scores and player colours
    string playerNumber = isPlayerTwoTurn ? "2" : "1";
    playerTurnText.text = "Player " + playerNumber + " Turn";
  }

  private void SwapTurns() {
    isPlayerTwoTurn = isPlayerTwoTurn ? false : true;
  }

}