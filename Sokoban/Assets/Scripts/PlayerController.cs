using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour 
{

	// Use this for initialization
	private float speed = 2.0f;
	private Vector3 pos;
	private Transform tr;
	private Rigidbody2D myRigidbody;
	private Animator myAnimator;
 
	void Start() 
	{
		pos = transform.position;
		tr = transform;
		myRigidbody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
	}
 
	void Update() {
 
		if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))&& tr.position == pos)
		{
			myAnimator.Play("dude_right");
			pos += Vector3.right;
		}
		
		else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && tr.position == pos) 
		{
			myAnimator.Play("dude_left");
			pos += Vector3.left;
		}
		
		else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && tr.position == pos) 
		{
			myAnimator.Play("dude_back");
			pos += Vector3.up;
		}
		
		else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && tr.position == pos) 
		{
			myAnimator.Play("dude_front");
			pos += Vector3.down;
		}
     
		transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
	}

	void OnTriggerEnter2D(Collider2D info)
	{
		if (info.gameObject.tag.Equals("wall"))
		{
			SceneManager.LoadScene("Level1");
		}
	}
}
