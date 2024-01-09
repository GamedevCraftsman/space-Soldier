using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public float speed;

    GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("JoystickBg"))
        {
            Player().checkStaying = false;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("JoystickBg"))
        {
            StopSpeed();
            Player().Anim().SetTrigger("Idle");
            Player().checkStaying = true;
        }
    }

    PlayerController Player()
    {
        return player.GetComponent<PlayerController>();
    }

    float StopSpeed()
    {
        return speed = 0;
    }
}
