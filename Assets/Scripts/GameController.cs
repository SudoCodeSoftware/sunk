using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

  private bool playerTurn;
  private bool ballsActive;

  private int playerOneScore;
  private int playerTwoScore;

  void Start () {
    playerTurn = false;
    ballsActive = false;
  }

  void Update () {
    UpdateUI();
  }

  public void DropBall(GameObject ball) {
    BallController ballController = ball.GetComponent<BallController>();
    CountSunkBall(ballController.ballPlayer);
  }

  private void CountSunkBall(bool player) {
    if (player) {
      playerTwoScore++;
    }
    else {
      playerOneScore++;
    }
  }

  private void UpdateUI() {
    
  }

}
