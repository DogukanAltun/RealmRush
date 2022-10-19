using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<WayPoint> path = new List<WayPoint>();
    [SerializeField] [Range(0f, 3f)]  float speed = 1f;
    [SerializeField] int heal;
    int StartHeal = 35;
    int Bonus = 5;
    int HitPoint = 1;
    Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    void OnEnable()
    {
        heal = StartHeal;
        FindPath();
        ReturnToStart();
        StartCoroutine(PathNames());
    }

    void FindPath()
    {
        path.Clear();
        GameObject Parent = GameObject.FindGameObjectWithTag("Path");
        foreach (Transform child  in Parent.transform)
        {
           WayPoint point = child.GetComponent<WayPoint>();
           if(point != null)
           {
               path.Add(point);
           }
        }
    }
    void ReturnToStart()
    {
        gameObject.SetActive(true);
        transform.position = path[0].transform.position;
    }
    void OnParticleCollision(GameObject other)
    {
        heal -= HitPoint;
        if(heal <= 0)
        {
            enemy.GoldReward();
            gameObject.SetActive(false);
            StartHeal += Bonus;
        }
    }
    
    IEnumerator PathNames()
    {
        foreach(WayPoint Points in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = Points.transform.position;
            float travelPercent = 0f;
            transform.LookAt(endPosition);
            Debug.Log(Points.name);
            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime* speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        enemy.GoldPenalty();
        gameObject.SetActive(false);
    }
}
