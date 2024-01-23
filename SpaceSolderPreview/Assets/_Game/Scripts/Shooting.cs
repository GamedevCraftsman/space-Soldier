using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.HID;
public class Shooting : MonoBehaviour
{
    [SerializeField] int maxCountOfDecals;   
    [SerializeField] float distance;
    [SerializeField] float secondsToRemoveFire;   
    [SerializeField] GameObject shootingFireParts;
    [SerializeField] GameObject bulletHole;
    [SerializeField] GameObject flash;
    [SerializeField] Transform spawn;

    List<GameObject> decals = new List<GameObject>();
    List<GameObject> fires = new List<GameObject> ();

    int countOfDecals = 0;
    const float totalDistance = 15;
    bool isPressed;
    GameObject shootingFire;
    GameObject decal;   
    void PlayerShootButton()
    {
        StartCoroutine(Flash());
        Ray ray = new Ray(spawn.position, spawn.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            Hit(hit);
        }
        else
        {
            distance = totalDistance;
        }
    }

    void Hit(RaycastHit hit)
    {
        distance = hit.distance;
        if (IsDecalsAlot())
        {
            Destroy(decals[0]);
            decals.RemoveAt(0);
        }
        StartCoroutine(SpawnBulletParts(hit));
        decal = Decal(hit);
        decals.Add(decal);
        countOfDecals++;
    }

    bool IsDecalsAlot()
    {
        return countOfDecals > maxCountOfDecals;
    }

    IEnumerator SpawnBulletParts(RaycastHit hit)
    {
        shootingFire = Instantiate(shootingFireParts, hit.point + (hit.normal * 0.01f), Quaternion.identity);
        fires.Add(shootingFire);
        yield return new WaitForSeconds(secondsToRemoveFire);
        Destroy(fires[0]);
        fires.RemoveAt(0);
    }

    GameObject Decal(RaycastHit hit)
    {
        return Instantiate(bulletHole, hit.point + (hit.normal * 0.01f), Quaternion.FromToRotation(Vector3.up, hit.normal));
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
