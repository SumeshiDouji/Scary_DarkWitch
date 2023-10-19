using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    GameObject terrain;
    [SerializeField]
    GameObject terrain2;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private float speed = 100;
    // Update is called once per frame
    void Update()
    {
        terrain.transform.position -= new Vector3(Time.deltaTime * speed, 0, 0);
        terrain2.transform.position -= new Vector3(Time.deltaTime * speed, 0, 0);

        if (terrain.transform.position.x <= -1200f)
        {
            terrain.transform.position = new Vector3(400, -50, -30);
        }
        if (terrain2.transform.position.x <= -1200f)
        {
            terrain2.transform.position = new Vector3(400, -50, -30);
        }
    }
}
