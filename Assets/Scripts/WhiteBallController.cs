using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBallController : MonoBehaviour {

    public float power;
    private Rigidbody2D rb;
    private Transform transform;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
    }

    void Update() {
        if (Input.GetKeyDown("space")) {
            Hit();
        }

        if (Input.GetMouseButtonDown(0)) {
            var worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.x, transform.position.z));
            var direction = transform.position - worldMousePosition;
			direction.Normalize();

			rb.AddForce(direction * 1000);
		}
    }

    void Hit() {
    	Vector2 move = (new Vector2(100, 0)) * power;
        rb.AddForce(move);
    }
}
