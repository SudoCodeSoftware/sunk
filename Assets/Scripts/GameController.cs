using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
  
  // Unity Assigned Fields
  public Text playerTurnText;
  public Text playerOneScoreText;
  public Text playerTwoScoreText;
  public Text playerWinText;
  
  // Private Fields
  private bool _isPlayerTwoTurn;
  private bool _coloursSet;
  private bool _penaltyTurn;
  private bool _playerWon;

  private string _winningPlayer;
  private string _playerOneColour;
  private string _playerTwoColour;

  private int _playerOneScore;
  private int _playerTwoScore;

  private void Start () {
    _isPlayerTwoTurn = false;
    _coloursSet = false;
    _penaltyTurn = false;
    _playerWon = false;
  }

  private void Update () {
    UpdateUi();
  }

  public void SwapTurns() {
    if (_penaltyTurn) {
      _penaltyTurn = false;
    }
    else {
      _isPlayerTwoTurn = !_isPlayerTwoTurn;
    }
  }

  public static bool AllStopped() {
    var allRigidBodies = FindObjectsOfType(typeof(Rigidbody2D));

    return allRigidBodies.Cast<Rigidbody2D>().All(rigidBody2D => rigidBody2D.velocity.magnitude.Equals(0));
  }

  public void SinkBall(GameObject ball) {
    BallController ballController = ball.GetComponent<BallController>();
    CountSunkBall(ballController.ballColour);
  }

  private void CountSunkBall(string sunkColour) {
    // For white ball
    if (sunkColour == "white") {
      // Give the current player an extra turn
      _penaltyTurn = true;
      // Create new White Ball
      GameObject newBall = (GameObject)Instantiate(Resources.Load("WhiteBall"), Vector3.zero, Quaternion.identity);
      newBall.GetComponent<PlayerController>().SetGameController(this);
    }
    // For black ball
    else if (sunkColour == "black") {
      // If all other balls have been sunk by current player
      string currentPlayerBallColour = GetCurrentPlayerBallColour();
      if (_coloursSet) {
        if (CountUnSunkBalls(currentPlayerBallColour) <= 0) {
          // Set current player as winner
          _winningPlayer = _isPlayerTwoTurn ? "Player Two" : "Player One";
          _playerWon = true;
        }
        else {
          _winningPlayer = _isPlayerTwoTurn ? "Player One" : "Player Two";
          _playerWon = true;
        }
      }
      else {
        _winningPlayer = _isPlayerTwoTurn ? "Player Two" : "Player One";
        _playerWon = true;
      }
      // TODO
      // End Game
    }
    // For colour ball
    else {
      // Check if the colours have been set
      if (!_coloursSet) {
        if (_isPlayerTwoTurn) {
          _playerTwoColour = sunkColour;
          _playerOneColour = sunkColour == "red" ? "yellow" : "red";
        }
        else {
          _playerOneColour = sunkColour;
          _playerTwoColour = sunkColour == "red" ? "yellow" : "red";
        }
        _coloursSet = true;
      }
      // Score the sunk ball
      AddBallToScore(sunkColour);
    }
  }

  private void AddBallToScore(string sunkColour) {
    if (_playerOneColour == sunkColour) {
      _playerOneScore++;
    }
    else {
      _playerTwoScore++;
    }
  }

  private string GetCurrentPlayerBallColour() {
    if (_isPlayerTwoTurn) {
      return _playerTwoColour;
    }
    else {
      return _playerOneColour;
    }
  }

  private static int CountUnSunkBalls(string ballColour) {
    var balls = GameObject.FindGameObjectsWithTag(ballColour);
    return balls.Length;
  }

  private void UpdateUi() {
    var playerNumber = _isPlayerTwoTurn ? "Two" : "One";
    playerTurnText.text = "Player " + playerNumber + " Turn";
    playerOneScoreText.text = "Player One: " + _playerOneScore;
    playerTwoScoreText.text = "Player Two: " + _playerTwoScore;
    if (_playerWon) {
      // Show win text
      playerWinText.text = _winningPlayer + " Won!";
    }
  }
}