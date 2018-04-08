using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
    public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;


	public bool grounded = false;
	private Animator anim;
	private Rigidbody2D rb;

	private AudioSource audios;
	public AudioClip die;
	public AudioClip boop;
	public AudioClip ting;

	public GameObject instructions;

	// Use this for initialization
	void Awake () 
	{
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		audios = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () 
	{
		Debug.Log (jump);
		if (grounded)
		{
			jump = true;
			Debug.Log ("beeP");
		}

		if (grounded && Input.GetButtonDown("Jump"))
		{
			rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
			audios.PlayOneShot (boop);
			jump = false;
		}
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal");

		if (h != 0) {
			anim.Play ("walk");
		} else {
			anim.Play ("idle");
		}

		if (h * rb.velocity.x < maxSpeed) {
			rb.AddForce (Vector2.right * h * moveForce);
		}

		if (Mathf.Abs (rb.velocity.x) > maxSpeed) {
			rb.velocity = new Vector2 (Mathf.Sign (rb.velocity.x) * maxSpeed, rb.velocity.y);
		}
		if (h > 0 && !facingRight) {
			Flip ();
		} else if (h < 0 && facingRight) {
			Flip ();
		}

	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		Debug.Log ("COLLIDE");
		if (coll.gameObject.tag == "ground") {
			grounded = true;
		}

		if (coll.gameObject.tag == "watch") {
			Destroy (coll.gameObject);
			audios.PlayOneShot (ting);
			instructions.gameObject.SetActive (true);
		}

		if (coll.gameObject.tag == "button") {
			if(GameManager.manager.checkIfLoaded("Dystopia"))
				{
					SceneManager.UnloadSceneAsync ("Dystopia");
					SceneManager.LoadScene ("Utopia", LoadSceneMode.Additive);
				}
			GameManager.manager.win.gameObject.SetActive(true);
		}

		if (coll.gameObject.tag == "platform") {
			transform.parent = coll.transform;
		}
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "ground") {
			grounded = false;
		}
		jump = false;

	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "checkpoint") {
			GameManager.manager.spawn = coll.transform.position;
		}

		if (coll.gameObject.tag == "enemy") {
			transform.position = GameManager.manager.spawn;
			audios.PlayOneShot (die);
		}
	}
}
