/* 
 Filename:TreatManager.cs
 Author: Catt Symonds
 Student Number: 101209214
 Date Last Modified: 19/10/2020
 Description: For managing an object pool of treats
 Revision History: 
 19/10/2020: File created
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatManager : MonoBehaviour
{
    public int maxTreats;
    public GameObject treatPrefab;
    private Queue<GameObject> treatPool;

    // Start is called before the first frame update
    void Start()
    {
        BuildTreatPool();
    }

    // Creates an object pool of treats
    private void BuildTreatPool()
    {
        // create empty Queue structure
        treatPool = new Queue<GameObject>();

        for (int count = 0; count < maxTreats; count++)
        {
            var tempTreat = Instantiate(treatPrefab);
            tempTreat.SetActive(false);
            tempTreat.transform.parent = transform;
            treatPool.Enqueue(tempTreat);
        }
    }

    // Returns a treat from the pool
    public GameObject GetTreat(Vector3 position)
    {
        var newTreat = treatPool.Dequeue();
        newTreat.SetActive(true);
        newTreat.transform.position = position;
        return newTreat;
    }


    // Check if there are treats in the pool
    public bool HasTreats()
    {
        return treatPool.Count > 0;
    }

    // Returns a treat to the pool
    public void ReturnTreat(GameObject returnedTreat)
    {
        returnedTreat.SetActive(false);
        treatPool.Enqueue(returnedTreat);
    }
}
