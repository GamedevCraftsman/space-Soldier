using System.Collections;
using UnityEngine;
public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject flash;
    [SerializeField] Transform spawn;
    [SerializeField] float distance;

    const float totalDistance = 15;
    bool isPressed;
    void PlayerShootButton()
    {
        StartCoroutine(Flash());
        Ray ray = new Ray(spawn.position, spawn.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            distance = hit.distance;
        }
        else
        {
            distance = totalDistance;
        }
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, 1); // delete in future.
    }

    IEnumerator Flash()
    {
        if (!flash.activeInHierarchy)
        {
            flash.SetActive(true);
            yield return null;
            flash.SetActive(false);
        }
    }

    public void OnUpdateSelected()
    {
        if (isPressed)
        {
            PlayerShootButton();
        }
    }

    public void OnPointerDown()
    {
        isPressed = true;
    }

    public void OnPointerUp()
    {
        isPressed = false;
    }
}
