using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "AI" || collision.gameObject.tag == "target") { Destroy(gameObject); }
    }
}
