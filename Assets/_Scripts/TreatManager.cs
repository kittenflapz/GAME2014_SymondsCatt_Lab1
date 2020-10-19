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

    public GameObject GetTreat(Vector3 position)
    {
        var newTreat = treatPool.Dequeue();
        newTreat.SetActive(true);
        newTreat.transform.position = position;
        return newTreat;
    }

    public bool HasTreats()
    {
        return treatPool.Count > 0;
    }

    public void ReturnTreat(GameObject returnedTreat)
    {
        returnedTreat.SetActive(false);
        treatPool.Enqueue(returnedTreat);
    }
}
