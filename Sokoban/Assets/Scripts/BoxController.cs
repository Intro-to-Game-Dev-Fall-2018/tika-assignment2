using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxController : MonoBehaviour
{

	private float speed = 2.0f;
	private Vector3 pos;
	private Transform tr;
	private Rigidbody2D myRigidbody;
	public GameObject box1;
	public GameObject box2;
	public Text gameText;

	private int key = 0;
	private int moveKey = 0;
	private int goalSum = 0;

	// Use this for initialization
	void Start()
	{
		pos = transform.position;
		tr = transform;
		myRigidbody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if (goalSum + box1.gameObject.GetComponent<BoxController>().GetGoalSum() + box2.gameObject.GetComponent<BoxController>().GetGoalSum() == 1)
		{
			gameText.text = "You won!";
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag.Equals("goal1") && other.gameObject.GetComponent<Collider2D>().bounds.Contains(pos))
		{
			goalSum = 1;
		}
		
	}

	public int GetGoalSum()
	{
		return goalSum;
	}

	public Vector3 GetPos()
	{
		return pos;
	}
	
}
