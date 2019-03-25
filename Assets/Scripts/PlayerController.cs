using UnityEngine;

// A class used to control the behaviour of the WhiteBall
public class PlayerController : MonoBehaviour {

  // Unity Fields
  public float amplification;
  public GameController gameController;
   
  // GameObject Fields
  private Rigidbody2D _rb;

  // Shot Calculation Fields
  private bool _mousePressed;
  private Vector2 _mouseStartPosition;
  private Vector2 _mouseEndPosition;

  private LineRenderer _forceVector;
  private const float ForceVectorScale = 0.5F;
  private const float ThresholdVelocity = 0.1F;	//Below this, velocities will be cut

  void Start() {
    // Get the GameObject components
    _rb = GetComponent<Rigidbody2D>();
    
    GetComponent<Transform>();
        _forceVector = _rb.gameObject.AddComponent<LineRenderer>();
        _forceVector.material = new Material(Shader.Find("Particles/Additive"));
        _forceVector.startWidth = 0.05f;
        _forceVector.endWidth = 0.05f;
        _forceVector.positionCount = 2;
    }
    

  void Update() {
    // Get the initial position of drag
    if (Input.GetMouseButtonDown(0) && _rb.velocity.magnitude.Equals(0)) {
      if (GameController.AllStopped()) {
        _mousePressed = true;
        _mouseStartPosition = GetMousePosition();
        _forceVector.enabled = true;
      }
      
    }

    // Get the final position of drag and execute hit
    if (Input.GetMouseButtonUp(0) && _mousePressed) {
      _mouseEndPosition = GetMousePosition();

      _forceVector.enabled = false;

      Vector2 heading = _mouseEndPosition - _mouseStartPosition;
      float distance = heading.magnitude;
      Vector2 direction = heading/distance;
      Hit(new Vector2(-direction.x, -direction.y), distance);

      gameController.SwapTurns();
      _mousePressed = false;
    }

    if (Input.GetMouseButton(0)) {
      Vector2 ballPos = _rb.gameObject.transform.position;
      _forceVector.SetPosition(0, ballPos);
      _forceVector.SetPosition(1, ballPos + Vector2.Scale(
                                   (GetMousePosition() - ballPos), 
                                   (new Vector2(-ForceVectorScale, -ForceVectorScale))));
    }
	
	  var allRigidBodies = FindObjectsOfType(typeof(Rigidbody2D));
    
    foreach (var obj in allRigidBodies) {
      var rigidBody2D = (Rigidbody2D) obj;
      if (rigidBody2D.velocity.magnitude < ThresholdVelocity) {
        rigidBody2D.velocity = new Vector2(0, 0);
      }
    }
  }

  public void SetGameController(GameController newGameController) {
    gameController = newGameController;
  }

  // Applies shot force to the ball
  private void Hit(Vector2 move, float power) {
    // Check if the Vector2 contains non-values
    if (!(float.IsNaN(move.x) && float.IsNaN(move.y))) {
      _rb.AddForce(move * power * amplification);
    }
  }

  // Helper method that returns the current MousePosition as a Vector2
  private static Vector2 GetMousePosition() {
    var camera = Camera.main;
    if (camera != null && camera.Equals(null)) return null;
    var ray = camera.ScreenPointToRay(Input.mousePosition);
    return ray.origin;
  }
}