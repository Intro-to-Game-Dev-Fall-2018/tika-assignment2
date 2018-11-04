using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MoveController
{
    public LayerMask pushingLayers;
    
    AudioSource audioSource;
    Animator myAnimator;

    protected override void Start()
    {
        myAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
            }
            else if (hor < 0)
            {
                myAnimator.SetTrigger("walkLeft");
                audioSource.Play();
            }
            else if (ver > 0)
            {
                myAnimator.SetTrigger("walkUp");
                audioSource.Play();
            }
            else if (ver < 0)
            {
                myAnimator.SetTrigger("walkDown");
                audioSource.Play();
            }

            RaycastHit2D hit = new RaycastHit2D();
            if (MoveBox(hor, ver, out hit))
            {
                  Move(hor, ver, out hit);
            }
        }
    }
}