using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class used to control the behaviour of the WhiteBall
public class PlayerController : MonoBehaviour {

  // Public Fields
  public float amplification;
  public GameController gameController;
   
  // GameObject Fields
  private Rigidbody2D rb;
  private Transform transform;

  // Shot Calculation Fields
  private bool mousePressed;
  private Vector2 mouseStartPosition;
  private Vector2 mouseEndPosition;

  private LineRenderer forceVector;
  private const float forceVectorScale = 0.5F;
  private const float thresholdVelocity = 0.1F;	//Below this, velocities will be cut

  void Start() {
    // Get the GameObject components
    rb = GetComponent<Rigidbody2D>();
    transform = GetComponent<Transform>();
        forceVector = rb.gameObject.AddComponent<LineRenderer>();
        forceVector.material = new Material(Shader.Find("Particles/Additive"));
        forceVector.SetWidth(0.05f, 0.05f);
        forceVector.SetVertexCount(2);
    }
    

  void Update() {
    // Get the initial position of drag
    if (Input.GetMouseButtonDown(0) && rb.velocity.magnitude == 0) {
      if (GameController.AllStopped()) {
        mousePressed = true;
        mouseStartPosition = GetMousePosition();
        forceVector.enabled = true;
      }
      
    }

    // Get the final position of drag and execute hit
    if (Input.GetMouseButtonUp(0) && mousePressed) {
      mouseEndPosition = GetMousePosition();

      forceVector.enabled = false;

      Vector2 heading = mouseEndPosition - mouseStartPosition;
      float distance = heading.magnitude;
      Vector2 direction = heading/distance;
      Hit(new Vector2(-direction.x, -direction.y), distance);

      gameController.SwapTurns();
      mousePressed = false;
    }

    if (Input.GetMouseButton(0)) {
      Vector2 ballPos = rb.gameObject.transform.position;
      forceVector.SetPosition(0, ballPos);
      forceVector.SetPosition(1, ballPos + Vector2.Scale((GetMousePosition() - ballPos), (new Vector2(-forceVectorScale, -forceVectorScale))));
    }
	
	Object[] allRigidBodies = GameObject.FindObjectsOfType(typeof(Rigidbody2D));
    
    foreach (Rigidbody2D obj in allRigidBodies) {
      if (obj.velocity.magnitude < thresholdVelocity) {
        obj.velocity = new Vector2(0, 0);
      }
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