using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	// PRIVATE INSTANCE VARIABLES +++++++++++++++++++++++++++
	private Transform _transform;
	private Rigidbody2D _rigidbody;
	private float _move;
	private float _jump;
	private bool _isFacingRight;
	private bool _isGrounded;

	// PUBLIC INSTANCE VARIABLE (FOR TESTING) +++++++++++++++++
	public float Velocity = 10f;
	public float JumpForce = 100f;
	public Camera camera;
	public Transform SpawnPoint;


	// Use this for initialization
	void Start () {
		this._initialize ();
	}
	
	// Update is called once per frame (Physics)
	void FixedUpdate () {
		if (this._isGrounded) {

			// check if input is present for movement
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

			// check if input is present for jumping
			if (Input.GetKeyDown (KeyCode.Space)) {
				this._jump = 1f;
			} else {
				this._jump = 0f;
			}
			Debug.Log (this._jump);


			this._rigidbody.AddForce ((
				new Vector2 (this._move * this.Velocity, 
							 this._jump * this.JumpForce)), 
								ForceMode2D.Force);

		}

		this.camera.transform.position = new Vector3 (this._transform.position.x * 0.8f, this._transform.position.y * 0.8f, -10f);

	}

	/**
	 * This method initializes several variables
	 */
	private void _initialize() {
		this._transform = GetComponent<Transform> ();
		this._rigidbody = GetComponent<Rigidbody2D> ();
		this._isFacingRight = true;
		this._isGrounded = false;
		this._move = 0f;
		this._jump = 0f;
		
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

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.CompareTag("DeathPlane")) {
			this._transform.position = this.SpawnPoint.position;
		}
	}

	private void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.CompareTag ("Platform")) {
			this._isGrounded = true;
		}
	}

	private void OnCollisionExit2D(Collision2D other) {
		this._isGrounded = false;
	}
}
