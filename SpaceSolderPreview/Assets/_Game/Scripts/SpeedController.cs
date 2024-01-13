using System.Collections;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    [SerializeField] float totalSpeed;
    [SerializeField] float waitingSeconds;

    public float speed;

    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("User");
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("JoystickBg"))
        {
            Player().checkStaying = false;
            StartCoroutine(MiddleSpeed());
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
        while (speed < totalSpeed)
        {
            if (player != null && Player().checkStaying == false)
            {
                Player().Anim().speed = speed / totalSpeed;
                yield return new WaitForSeconds(waitingSeconds);
                speed++;
            }
            else
            {
                break;
            }
        }
    }

    int Stop()
    {
        return 0;
    }
}
