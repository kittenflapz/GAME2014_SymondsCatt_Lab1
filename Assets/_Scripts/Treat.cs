/* 
 Filename:Treat.cs
 Author: Catt Symonds
 Student Number: 101209214
 Date Last Modified: 17/10/2020
 Description: For destroying treats when they are off screen and spawning more from the object pool
 Revision History: 
 19/10/2020: File created
 */

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
            // A treat has been destroyed, so spawn a new one at least two screen's height away from this treat
            Vector2 newTreatPos = new Vector2(Random.Range(-2f, 2f), Random.Range(transform.position.y + 8f, transform.position.y + 14f));
            GameObject newTreat = treatManager.GetTreat(newTreatPos);
        }
    }
}
