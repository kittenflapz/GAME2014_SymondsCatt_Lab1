using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treat : MonoBehaviour
{
    public TreatManager treatManager;


    private void Start()
    {
        treatManager = GameObject.Find("TreatManager").GetComponent<TreatManager>();
    }
    // Update is called once per frame
    void Update()
    {
        // if a treat goes offscreen, return it to the pool
        if (transform.position.y <= -5f)
        {
            treatManager.ReturnTreat(this.gameObject);
        }
    }
}
