using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

  public Text playerTurnText;
  public Text playerOneScoreText;
  public Text playerTwoScoreText;
  public Text playerWinText;

  private bool isPlayerTwoTurn;
  private bool ballsActive;
  private bool coloursSet;
  private bool penaltyTurn;
  private bool playerWon;

  private string winningPlayer;
  private string playerOneColour;
  private string playerTwoColour;

  private int playerOneScore;
  private int playerTwoScore;

  void Start () {
    isPlayerTwoTurn = false;
    ballsActive = false;
    coloursSet = false;
    penaltyTurn = false;
    playerWon = false;
  }

  void Update () {
    UpdateUI();
  }

  public void SwapTurns() {
    if (penaltyTurn) {
      penaltyTurn = false;
    }
    else {
      isPlayerTwoTurn = isPlayerTwoTurn ? false : true;
    }
  }

  public static bool AllStopped() {
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
    // For white ball
    if (sunkColour == "white") {
      // Give the current player an extra turn
      penaltyTurn = true;
      // Create new White Ball
      GameObject newBall = (GameObject)Instantiate(Resources.Load("WhiteBall"), Vector3.zero, Quaternion.identity);
      newBall.GetComponent<PlayerController>().SetGameController(this);
    }
    // For black ball
    else if (sunkColour == "black") {
      // If all other balls have been sunk by current player
      string currentPlayerBallColour = GetCurrentPlayerBallColour();
      if (coloursSet) {
        if (CountUnsunkBalls(currentPlayerBallColour) <= 0) {
          // Set current player as winner
          winningPlayer = isPlayerTwoTurn ? "Player Two" : "Player One";
          playerWon = true;
        }
        else {
          winningPlayer = isPlayerTwoTurn ? "Player One" : "Player Two";
          playerWon = true;
        }
      }
      else {
        winningPlayer = isPlayerTwoTurn ? "Player Two" : "Player One";
        playerWon = true;
      }
      // TODO
      // End Game
    }
    // For colour ball
    else {
      // Check if the colours have been set
      if (!coloursSet) {
        if (isPlayerTwoTurn) {
          playerTwoColour = sunkColour;
          playerOneColour = sunkColour == "red" ? "yellow" : "red";
        }
        else {
          playerOneColour = sunkColour;
          playerTwoColour = sunkColour == "red" ? "yellow" : "red";
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

  private string GetCurrentPlayerBallColour() {
    if (isPlayerTwoTurn) {
      return playerTwoColour;
    }
    else {
      return playerOneColour;
    }
  }

  private int CountUnsunkBalls(string ballColour) {
    Object[] balls = GameObject.FindGameObjectsWithTag(ballColour);
    return balls.Length;
  }

  private void UpdateUI() {
    string playerNumber = isPlayerTwoTurn ? "Two" : "One";
    playerTurnText.text = "Player " + playerNumber + " Turn";
    playerOneScoreText.text = "Player One: " + playerOneScore;
    playerTwoScoreText.text = "Player Two: " + playerTwoScore;
    if (playerWon) {
      // Show win text
      playerWinText.text = winningPlayer + " Won!";
    }
  }
}