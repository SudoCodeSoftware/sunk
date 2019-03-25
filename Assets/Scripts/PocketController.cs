using UnityEngine;

public class PocketController : MonoBehaviour {

  public GameObject gameControllerObject;
  private GameController _gameController;

  private void Start () {
    _gameController = gameControllerObject.GetComponent<GameController>();
  }

  private void OnTriggerEnter2D(Collider2D ballCollider) {
    var ball = ballCollider.gameObject;
    _gameController.SinkBall(ball);
    Destroy(ball);
  }
}