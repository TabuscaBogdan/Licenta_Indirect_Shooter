using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_behaiviour : OnHit {

    private Vector3 position;
    private Vector3 start_point;
    private int lifetime;

    Blue_behaiviour()
    {

        lifetime = 3000;
        this.damage = 10;
    }

    private void Start()
    {
        start_point = transform.position;
        position = start_point;
    }
    // Update is called once per frame
    void Update()
    {
        position.x += 0.1f;
        position.z += 0.1f;
        transform.position = position;
        lifetime--;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
    }
}
