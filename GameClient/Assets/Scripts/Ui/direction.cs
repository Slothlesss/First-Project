using UnityEngine;

public class direction : MonoBehaviour
{
    public static bool directionLeft = false;
    public static bool directionRight = false;
    public static bool directionTop = false;
    public static bool directionDown = false;
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "thumb")
        {
            if (this.name == "directionLeft") directionLeft = true;
            if (this.name == "directionRight") directionRight = true;
            if (this.name == "directionTop") directionTop = true;
            if (this.name == "directionDown") directionDown = true;
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
            directionLeft = false;
            directionRight = false;
            directionTop = false;
            directionDown = false;
    }
}
