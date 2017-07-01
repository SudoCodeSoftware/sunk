using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBallController : MonoBehaviour {

  private Rigidbody2D rb;
  private Transform transform;

  public float amplification;

  private float power;
  private bool mousePressed;
  private Vector2 mouseStartPosition;
  private Vector2 mouseEndPosition;

  private Vector2 heading;
  private float distance;
  private Vector2 direction;

  void Start() {
      rb = GetComponent<Rigidbody2D>();
      transform = GetComponent<Transform>();
  }

  void Update() {
    if (Input.GetKeyDown("space")) {
      Hit(new Vector2(100, 0));
    }

    if (Input.GetMouseButtonDown(0)) {
      mousePressed = true;

      Ray rayStart = Camera.main.ScreenPointToRay(Input.mousePosition);

      mouseStartPosition = rayStart.origin;
    }

    if (Input.GetMouseButtonUp(0)){

      if (mousePressed) {

        Ray rayEnd = Camera.main.ScreenPointToRay(Input.mousePosition);

        mouseEndPosition = rayEnd.origin;

        heading = mouseEndPosition - mouseStartPosition;
        distance = heading.magnitude;
        direction = heading/distance;

        Vector2 move = new Vector2(direction.x, direction.y);
        power = distance;

        Hit(move);

        mousePressed = false;
      }

    }
  }

  void Hit(Vector2 move) {
    rb.AddForce(move * power * amplification);
  }
}
