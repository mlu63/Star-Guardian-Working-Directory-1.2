using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour
{

    public Transform center;
    public float degreesPerSecond = -15.0f;

    private Vector3 v;

    void Start()
    {
        v = transform.position - center.position;
    }

    void Update()
    {
        v = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.forward) * v;
        transform.position = center.position + v;
    }
}