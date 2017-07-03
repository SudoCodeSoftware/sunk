using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class used to control the behaviour of the WhiteBall
public class PlayerController : MonoBehaviour {

  // Public Fields
  public float amplification;
   
  // GameObject Fields
  private Rigidbody2D rb;
  private Transform transform;

  // Shot Calculation Fields
  private bool mousePressed;
  private Vector2 mouseStartPosition;
  private Vector2 mouseEndPosition;
   

  void Start() {
    // Get the GameObject components
    rb = GetComponent<Rigidbody2D>();
    transform = GetComponent<Transform>();
  }
    

  void Update() {
    // Get the initial position of drag
    if (Input.GetMouseButtonDown(0)) {
      mousePressed = true;
      mouseStartPosition = GetMousePosition();
    }

    // Get the final position of drag and execute hit
    if (Input.GetMouseButtonUp(0) && mousePressed) {
        mouseEndPosition = GetMousePosition();

        Vector2 heading = mouseEndPosition - mouseStartPosition;
        float distance = heading.magnitude;
        Vector2 direction = heading/distance;

        Hit(new Vector2(-direction.x, -direction.y), distance);

        mousePressed = false;
    }
  }

  // Applies shot force to the ball
  private void Hit(Vector2 move, float power) {
    // Check if the Vector2 contains non-values
    if (!(float.IsNaN(move.x) && float.IsNaN(move.y))) {
      rb.AddForce(move * power * amplification);
    }
  }

  // Helper method that returns the current MousePosition as a Vector2
  private Vector2 GetMousePosition() {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    return ray.origin;
  }
}