using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public GameObject[] objects;
    public int[] number;

    public List<GameObject>[] pool;

	void Start ()
    {
        instantiate();
    }

    public GameObject activate(int id)
    {
        for(int count = 0; count<pool[id].Count; count++)
        {
            if (!pool[id][count].activeSelf)
            {
                pool[id][count].SetActive(true);
                return pool[id][count];
            }
        }
        pool[id].Add((GameObject)Instantiate(objects[id]));
        pool[id][pool[id].Count - 1].transform.parent = this.transform;

        return pool[id][pool[id].Count - 1];
    }

    public GameObject activate(int id, Vector3 position, Quaternion rotation)
    {
        for (int count = 0; count < pool[id].Count; count++)
        {
            if (!pool[id][count].activeSelf)
            {
                pool[id][count].SetActive(true);
                pool[id][count].transform.position = position;
                pool[id][count].transform.rotation = rotation;
                return pool[id][count];
            }
        }
        pool[id].Add((GameObject)Instantiate(objects[id]));
        pool[id][pool[id].Count - 1].transform.position = position;
        pool[id][pool[id].Count - 1].transform.rotation = rotation;
        pool[id][pool[id].Count - 1].transform.parent = this.transform;

        return pool[id][pool[id].Count - 1];
    }

    public void deactivate(GameObject deactiveObject)
    {
        deactiveObject.SetActive(false);

    }

    void instantiate()
    {
        GameObject temp;

        pool = new List<GameObject>[objects.Length];
        for (int count = 0; count<objects.Length; count++)
        {
            pool[count] = new List<GameObject>();
            for(int num = 0; num<number[count]; num++)
            {
                temp = (GameObject)Instantiate(objects[count]);
                temp.transform.parent = this.transform;
                pool[count].Add(temp);
            }

        }
    }
}
