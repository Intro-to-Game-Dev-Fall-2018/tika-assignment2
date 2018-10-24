using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public GameObject on;
    public GameObject off;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Goal")
        {
            on.SetActive(true);
            off.SetActive(false);
        }
        else if (other.tag == "Empty")
        {
            on.SetActive(false);
            off.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Goal")
        {
            on.SetActive(false);
            off.SetActive(true);
        }
    }
}