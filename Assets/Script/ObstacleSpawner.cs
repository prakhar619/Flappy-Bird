using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float spawnTime = 3f;
    public float backSpeed = 4f;
    public float killIndexX = -7f;
    List<GameObject> screen = new List<GameObject>();

    private Dictionary<int, List<GameObject>> Obstacles = new Dictionary<int, List<GameObject>>();
    private List<double> Prob = new List<double>{0.15, 0.25, 0.65, 0.75, 0.85, 1};
    private Dictionary<int, List<GameObject>> AboveObstacles = new Dictionary<int, List<GameObject>>();
    private List<double> AboveProb = new List<double>{0.1, 0.2, 0.3, 0.65, 1};
    // Start is called before the first frame update
    void Start()
    {
        Obstacles[1] = new List<GameObject>();
        Obstacles[2] = new List<GameObject>();
        Obstacles[3] = new List<GameObject>();
        Obstacles[4] = new List<GameObject>();
        Obstacles[5] = new List<GameObject>();

        Obstacles[1].Add(GameObject.Find("SmallBox"));
        Obstacles[1].Add(GameObject.Find("Cart"));

        Obstacles[2].Add(GameObject.Find("Post"));
        Obstacles[2].Add(GameObject.Find("BigBox"));
        Obstacles[2].Add(GameObject.Find("Banner"));
        Obstacles[2].Add(GameObject.Find("Log"));
        Obstacles[2].Add(GameObject.Find("T"));
        Obstacles[2].Add(GameObject.Find("BigStairs"));

        Obstacles[3].Add(GameObject.Find("Statue"));
        Obstacles[3].Add(GameObject.Find("Well"));

        Obstacles[4].Add(GameObject.Find("StreetLightLit"));
        Obstacles[4].Add(GameObject.Find("Tree1"));

        Obstacles[5].Add(GameObject.Find("Tree2"));

        AboveObstacles[1] = new List<GameObject>();
        AboveObstacles[2] = new List<GameObject>();
        AboveObstacles[3] = new List<GameObject>();
        AboveObstacles[4] = new List<GameObject>();
        AboveObstacles[1].Add(GameObject.Find("BigFlag"));
        AboveObstacles[2].Add(GameObject.Find("SmallFlag"));
        AboveObstacles[3].Add(GameObject.Find("Ladder"));
        AboveObstacles[4].Add(GameObject.Find("Pole1"));
        AboveObstacles[4].Add(GameObject.Find("Pole2"));

    }

    System.Tuple<GameObject,GameObject> Spawner()
    {
        GameObject below = null; int below_size = 0;
        GameObject above = null;

        System.Random rnd = new System.Random();
        double p = rnd.NextDouble();
        if(p > Prob[0])
        for(int i = 1; i < Prob.Count; i++)
        {
            if(p <= Prob[i])
            {
                int n = rnd.Next(0, Obstacles[i].Count);
                // Debug.Log(i + " " + n);
                below = Instantiate(Obstacles[i][n], transform.position, Quaternion.identity);
                below_size = i;
                break;
            }
        }

        p = rnd.NextDouble();
        if(p > AboveProb[0] && below_size < 5)
        for(int i = 1; i < AboveProb.Count; i++)
        {
            if(p <= AboveProb[i])
            {
                int n = rnd.Next(0, AboveObstacles[i].Count);
                //Debug.Log(i + " " + n);

                // below_size + 1 = transform.position + new Vector3(0,5,0) - new Vector(0,i,0)
                Vector3 shift = new Vector3(0f,0f,0f);
                if(i > 2 && below_size != 0)
                {
                    shift = new Vector3(0f, i + below_size + 1 - 5, 0f);
                }
                above = Instantiate(AboveObstacles[i][n], transform.position + new Vector3(0f,5.5f,0f) + shift, Quaternion.identity);
                break;
            }
        }
        return new System.Tuple<GameObject, GameObject>(below,above);
    }

    // Update is called once per frame
    private float currTime = 0f;
    void FixedUpdate()
    {
        currTime += Time.deltaTime;
        if(currTime > spawnTime)
        {
            currTime = 0f;
            System.Tuple<GameObject,GameObject>  clone = Spawner();
            if(clone.Item1 != null)
                screen.Add(clone.Item1);
            if(clone.Item2 != null)
                screen.Add(clone.Item2);
            //Debug.Log("Spawned");
        }

        List<GameObject> kill = new List<GameObject>();
        foreach(GameObject go in screen)
        {
            Vector3 position = go.transform.position;
            Vector3 decrement = new Vector3(backSpeed*Time.deltaTime, 0, 0);
            if(position.x > -2f && position.x - decrement.x < -2f)
            {
                // Debug.Log(ScoreManager.CurrentScore);
                ScoreManager.CurrentScore += 1;
            }
            position = position - decrement;

            if(position.x < killIndexX)
            {
                Destroy(go);
                kill.Add(go);
                //Debug.Log("Killed");
            }

            go.transform.position = position;
        }

        foreach(GameObject go in kill)
        {
            screen.Remove(go);
        }
    }
}
