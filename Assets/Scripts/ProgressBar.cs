using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour
{
    public float spawnRate = 1.0F;
    private float nextSpawn = 0.0F;
    private float lastUpdate = 1;

    // Use this for initialization
    void Start ()
    {

	}

    // Update is called once per frame
    void Update()
    {
        //Progresses the progression person across the progression bar from left to right
        if (Time.time - lastUpdate > 1)
        {
            nextSpawn += spawnRate;
            //60 seconds to complete the level
            transform.Translate(Vector2.right * 4f * Time.deltaTime);
            lastUpdate = Time.time;
        }
    }
}
