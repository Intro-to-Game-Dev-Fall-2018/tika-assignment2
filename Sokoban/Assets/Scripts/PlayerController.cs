using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MoveController
{
    public LayerMask pushingLayers;
    
    AudioSource audioSource;
    Text stepText;
    private int numSteps = 0;

    Animator myAnimator;

    protected override void Start()
    {
        myAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        stepText = GameObject.Find("StepText").GetComponent<Text>();
        base.Start();
    }

    bool MoveBox(float xDir, float yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        collider.enabled = false;
        hit = Physics2D.Linecast(start, end, pushingLayers);
        collider.enabled = true;

        if (hit.transform == null)
        {
            return true;
        }

        return hit.transform.GetComponent<MoveController>().Move(xDir, yDir, out hit);
    }

    public void Update()
    {
        if (moving)
        {
            return;
        }


        float hor = Input.GetAxisRaw("Horizontal") / GameManager.scale;
        float ver = Input.GetAxisRaw("Vertical") / GameManager.scale;

        if (hor!= 0)
        {
            ver = 0;
        }

        myAnimator.SetBool("walkLeft", false);
        myAnimator.SetBool("walkRight", false);
        myAnimator.SetBool("walkUp", false);
        myAnimator.SetBool("walkDown", false);

        if (hor != 0 || ver != 0)
        {
            if (hor > 0)
            {
                myAnimator.SetTrigger("walkRight");
                audioSource.Play();
                numSteps++;
                stepText.text = numSteps.ToString();

            }
            else if (hor < 0)
            {
                myAnimator.SetTrigger("walkLeft");
                audioSource.Play();
                numSteps++;
                stepText.text = numSteps.ToString();
            }
            else if (ver > 0)
            {
                myAnimator.SetTrigger("walkUp");
                audioSource.Play();
                numSteps++;
                stepText.text = numSteps.ToString();
            }
            else if (ver < 0)
            {
                myAnimator.SetTrigger("walkDown");
                audioSource.Play();
                numSteps++;
                stepText.text = numSteps.ToString();
            }

            RaycastHit2D hit = new RaycastHit2D();
            if (MoveBox(hor, ver, out hit))
            {
                  Move(hor, ver, out hit);
            }
        }
    }
}