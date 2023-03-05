using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Vector3 IsInZone(Vector3 playerPosition) => playerPosition - gameObject.transform.position;
}