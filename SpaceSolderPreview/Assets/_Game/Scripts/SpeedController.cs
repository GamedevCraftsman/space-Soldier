using System.Collections;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    [SerializeField] float totalSpeed;
    [SerializeField] float waitingSeconds;

    public float speed = 0;

    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("User");
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("JoystickBg"))
        {
            StartCoroutine(MiddleSpeed());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("JoystickBg"))
        {
            StopSpeed();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("JoystickBg"))
        {
            StopSpeed();
        }
    }

    void StopSpeed()
    {
        speed = Stop();
        Player().Anim().SetTrigger("Idle");
        Player().checkStaying = true;
    }

    PlayerController Player()
    {
        if (player != null)
        {
            return player.GetComponent<PlayerController>();
        }
        else
        {
            return null;
        }
    }

    IEnumerator MiddleSpeed()
    {
        while (speed <= totalSpeed)
        {
            if (player != null)
            {
                Player().checkStaying = false;
                Player().Anim().speed = speed / totalSpeed;
            }
            yield return new WaitForSeconds(waitingSeconds);
            speed++;
        }
    }

    int Stop()
    {
        return 0;
    }
}
