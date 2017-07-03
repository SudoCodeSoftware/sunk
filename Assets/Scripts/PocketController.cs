using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketController : MonoBehaviour {

  public GameObject gameControllerObject;
  private GameController gameController;

  void Start () {
    gameController = gameControllerObject.GetComponent<GameController>();
  }

  void OnTriggerEnter2D(Collider2D collider) {
    GameObject ball = collider.gameObject;
    gameController.SinkBall(ball);
    Destroy(ball);
  }
}