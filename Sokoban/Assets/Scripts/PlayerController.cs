using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour 
{

	// Use this for initialization
	private float speed = 2.0f;
	private Vector3 pos;
	private Transform tr;
	private Rigidbody2D myRigidbody;
	private Animator myAnimator;
	private int key;
	private int numSteps;
	public Text stepText;
 
	void Start() 
	{
		pos = transform.position;
		tr = transform;
		myRigidbody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		key = 0;
		numSteps = 0;
		stepText.text = "Steps: " + numSteps;

	}
 
	void Update() 
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene("Level1");
		}
 
		if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))&& tr.position == pos)
		{
			key = 1;
			myAnimator.Play("dude_right");
			pos += Vector3.right;
			numSteps++;
			stepText.text = "Steps: " + numSteps;
			
		}
		
		else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && tr.position == pos)
		{
			key = 2;
			myAnimator.Play("dude_left");
			pos += Vector3.left;
			numSteps++;
			stepText.text = "Steps: " + numSteps;
		}
		
		else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && tr.position == pos)
		{
			key = 3;
			myAnimator.Play("dude_back");
			pos += Vector3.up;
			numSteps++;
			stepText.text = "Steps: " + numSteps;
		}
		
		else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && tr.position == pos)
		{
			key = 4;
			myAnimator.Play("dude_front");
			pos += Vector3.down;
			numSteps++;
			stepText.text = "Steps: " + numSteps;
		}
     
		transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
	}

	void OnTriggerEnter2D(Collider2D info)
	{
		if (info.gameObject.tag.Equals("wall") || info.gameObject.tag.Equals("box"))
		{
			if (key == 1)
			{
				transform.position = Vector3.MoveTowards(transform.position, pos+= Vector3.left, Time.deltaTime * speed);
			}
			
			if (key == 2)
			{
				transform.position = Vector3.MoveTowards(transform.position, pos+= Vector3.right, Time.deltaTime * speed);
			}
			
			if (key == 3)
			{
				transform.position = Vector3.MoveTowards(transform.position, pos+= Vector3.down, Time.deltaTime * speed);
			}
			
			if (key == 4)
			{
				transform.position = Vector3.MoveTowards(transform.position, pos+= Vector3.up, Time.deltaTime * speed);
			}

			key = 0;

		}
	}
}
