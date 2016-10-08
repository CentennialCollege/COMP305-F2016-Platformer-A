using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	// PRIVATE INSTANCE VARIABLES +++++++++++++++++++++++++++
	private Transform _transform;
	private Rigidbody2D _rigidbody;
	private float _move;
	private bool _isFacingRight;

	// PUBLIC INSTANCE VARIABLE (FOR TESTING) +++++++++++++++++
	public float Velocity = 10f;
	public Camera camera;


	// Use this for initialization
	void Start () {
		this._initialize ();
	}
	
	// Update is called once per frame (Physics)
	void FixedUpdate () {
		this._move = Input.GetAxis ("Horizontal");
		if (this._move > 0f) {
			this._move = 1f;
			this._isFacingRight = true;
			this._flip ();
		} else if (this._move < 0f) {
			this._move = -1f;
			this._isFacingRight = false;
			this._flip ();
		} else {
			this._move = 0f;
		}

		this._rigidbody.AddForce((new Vector2 (this._move * this.Velocity, 0f)), ForceMode2D.Force);

		this.camera.transform.position = new Vector3 (this._transform.position.x, this._transform.position.y, -10f);
	}

	/**
	 * This method initializes several variables
	 */
	private void _initialize() {
		this._transform = GetComponent<Transform> ();
		this._rigidbody = GetComponent<Rigidbody2D> ();
		this._isFacingRight = true;
		
	}

	/**
	 * This method flips the character's texture (sprite) across the x-axis
	 */
	private void _flip() {
		if (this._isFacingRight) {
			this._transform.localScale = new Vector2 (1f, 1f);
		} else {
			this._transform.localScale = new Vector2 (-1f, 1f);
		}
	}
}
