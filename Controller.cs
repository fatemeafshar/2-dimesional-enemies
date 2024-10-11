using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controller : MonoBehaviour
{

    public Vector3 start, end; 
    public Player playerPrefab, player;
    public GameObject rotationFindingPrefab, target, deadlyPointPrefab;
    public int enemiesNumber;
    
    
    public List<Enemy> enemiePrefabs = new List<Enemy>();  
    //{

    //    for (int i = 0; i < enemiesNumber: i++)
    //    {
    //        //var enmey = Instantiate(malePrefab, new Vector3(i,i,i), Quaternion.identity);
    //        //enmey.transform.Rotate(0.0f, vTheta / 10, 0.0f, Space.World);
    //    }
    //}

    // Update is called once per frame
    void Start()
    {
        //player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        GenerateEnemy();
        
        //plane.gameobject.transform.Rotate(90.0f, 0.0f, 0.0f, Space.World);
        //Debug.Log(plane.normal);
    }
    
    void GenerateEnemy()
    {
        for (int i = 5; i < end.z; i= i + 4)
        {
            int division = 4;
            var position = new Vector3(Random.Range(-end.z / division, end.z / division), Random.Range(-end.z / division, end.z / division), i);
            int whichEnemy = (int)Random.Range(0, enemiePrefabs.Count);
            
            var enemy = Instantiate(enemiePrefabs[whichEnemy], position, Quaternion.identity);

            var rotation = Random.rotation;

            var findingNormal = Instantiate(rotationFindingPrefab, Vector3.zero, Quaternion.identity);
            findingNormal.transform.rotation = rotation;
            var child = findingNormal.transform.Find("Cube");
            child.transform.SetParent(null);
            
            
            enemy.transform.rotation = rotation;
            enemy.SetNormals(child.transform.position);
            Vector3 coefficients = FindLineEquation(start, end);
            enemy.ComputeDeadlyPoint(coefficients, end);
            player.enemiesList.Add(enemy);
            //enemiesList.Add(enemy);

            Destroy(findingNormal);
            Destroy(child.gameObject);
        }
        Destroy(rotationFindingPrefab);
        float highest = 0;
        foreach(var enemy in player.enemiesList)
        {
            if (highest < enemy.interactionPoint.z)
            {
                highest = enemy.interactionPoint.z;
            }
            Debug.Log(enemy.normals);
            //Debug.Log(enemy.interactionPoint);
            var hole = Instantiate(deadlyPointPrefab, enemy.interactionPoint, Quaternion.identity);
            hole.transform.Rotate(90.0f, 0.0f, 0.0f, Space.World);
        }
        target.transform.position = new Vector3(0, 0, highest+2);
        player.endPlace = new Vector3(0, 0, highest+2);
    }
    Vector3 FindLineEquation(Vector3 start, Vector3 end)
    {
        float a, b, c;
        a = end.x - start.x;
        b = end.y - start.y;
        c = end.z - start.z;

        return new Vector3(a, b, c);

    }
   
}
