using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {

	private bool directionR = true;
	public float speed = 2.0f;
	public float max;
	public float min;

	public int enemyNum;

	public bool movementHorizontal;

	public Rigidbody2D rb;

	void Start()
	{
		if (this.gameObject.tag == "enemy") {
			switch (enemyNum) {
			case 1:
				transform.position = GameManager.manager.enemy1;
				break;
			case 2:
				transform.position = GameManager.manager.enemy2;
				break;
			case 3:
				transform.position = GameManager.manager.enemy3;
				break;
			case 4:
				transform.position = GameManager.manager.enemy4;
				break;
			default:
				break;
			}
		}
		if (this.gameObject.tag == "ground") {
			switch (enemyNum) {
			case 1:
				transform.position = GameManager.manager.platform;
				break;
			case 2:
				transform.position = GameManager.manager.platform2;
				break;
			case 3:
				transform.position = GameManager.manager.platform3;
				break;
			default:
				break;
			}

			if (movementHorizontal) {
				rb = this.GetComponent<Rigidbody2D> ();
			}
		}
	}

	void Update(){
		if (movementHorizontal && this.gameObject.tag == "enemy") {
			HorizontalMove ();
		}
		else if(movementHorizontal != true)
		{
			VerticalMove ();
		}
	}

	void FixedUpdate()
	{
		if (movementHorizontal && this.gameObject.tag == "ground") {
			SpecialHorzMove ();
		}
	}

	void OnDisable()
	{
		if (this.gameObject.tag == "enemy") {
			switch (enemyNum) {
			case 1:
				GameManager.manager.enemy1 = transform.position;
				break;
			case 2:
				GameManager.manager.enemy2 = transform.position;
				break;
			case 3:
				GameManager.manager.enemy3 = transform.position;
				break;
			case 4:
				GameManager.manager.enemy4 = transform.position;
				break;
			default:
				break;
			}
		}
		if (this.gameObject.tag == "platform") {
			switch (enemyNum) {
			case 1:
				GameManager.manager.platform = transform.position;
				break;
			case 2:
				GameManager.manager.platform2 = transform.position;
				break;
			case 3:
				GameManager.manager.platform3 = transform.position;
				break;
			default:
				break;
			}
		}
	}

	void FlipX()
	{
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void HorizontalMove()
	{
		if (directionR) {
			transform.Translate (Vector2.right * speed * Time.deltaTime);
		} else {
			transform.Translate (-Vector2.right * speed * Time.deltaTime);
		}

		if (transform.position.x >= max) {
			directionR = false;
			FlipX ();
		}
		if (transform.position.x <= min) {
			directionR = true;
			FlipX ();
		}
	}

	void SpecialHorzMove()
	{
		if (directionR) {
			rb.MovePosition (transform.position + transform.right * Time.deltaTime);
		} else {
			rb.MovePosition (transform.position - (transform.right * Time.deltaTime));
		}
		if (transform.position.x >= max) {
			directionR = false;
		}
		if (transform.position.x <= min) {
			directionR = true;
		}
	}

	void VerticalMove()
	{
		if (directionR) {
			transform.Translate (Vector2.up * speed * Time.deltaTime);
		} else {
			transform.Translate (Vector2.down * speed * Time.deltaTime);
		}

		if (transform.position.y >= max) {
			directionR = false;
		}
		if (transform.position.y <= min) {
			directionR = true;
		}
	}
		
}