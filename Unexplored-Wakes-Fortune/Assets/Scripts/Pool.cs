using UnityEngine;

public class Pool
{
    GameObject[] gameObjects;
    int m_numObjects = 0;
    int nextBullet = 0;

    Pool bulletPool;

    public Pool(GameObject obj, int numObjects)
    {
        m_numObjects = numObjects;
        gameObjects = new GameObject[numObjects];
        for (int i = 0; i < numObjects; i++)
        {
            gameObjects[i] = GameObject.Instantiate(obj);
            gameObjects[i].SetActive(false);
        }
    }

    public void ActivateNext(Vector3 position, Quaternion rotation)
    {
        gameObjects[nextBullet].transform.position = position;
        gameObjects[nextBullet].transform.rotation = rotation;
        gameObjects[nextBullet].SetActive(true);


        nextBullet = (nextBullet + 1) % gameObjects.Length;

        /*
        // Cycle through all the objects in the pool to find the next inactive object.
        for (int i = 0; i < m_numObjects; i++)
        {
            if (!gameObjects[i].activeSelf)
            {
                // Activate at position and rotation.
                gameObjects[i].SetActive(true);
                gameObjects[i].transform.position = position;
                gameObjects[i].transform.rotation = rotation;

                break; // So that we don't activate all of them...
            }
            else
            {
                // Use the oldest bullet instead.
                gameObjects[i].SetActive(true);
                gameObjects[i].transform.position = position;
                gameObjects[i].transform.rotation = rotation;

                break; // So that we don't activate all of them...
            }
        }
        */
    }
}
