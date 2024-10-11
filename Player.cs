using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public List<Enemy> enemiesList = new List<Enemy>();
    //public GameObject targettingObject, targettingObjectPrefab;
    public Vector3 endPlace;
    private Vector3 targetVector;
    public LineRenderer lineRenderer;
    public TMP_Text text, Xtext, Ytext, Ztext, remainedDeadlyPoints;
    public List<GameObject> hearts = new List<GameObject>();
    private int lives;
    private bool move;
    private int status_check;
    

    void Start () {
        targetVector = Vector3.zero;
        lineRenderer.positionCount = 2;
        lives = 3;
        move = true;
        status_check = 0;
    }
    void Update()
    {
        Xtext.text = ((int)(targetVector.x)).ToString();
        Ytext.text = ((int)(targetVector.y)).ToString();
        Ztext.text = ((int)(targetVector.z)).ToString();
        remainedDeadlyPoints.text = enemiesList.Count.ToString();

        if (lives > 0)
        {
            //Debug.Log(lives);
            status_check --;
            if (status_check < 0)
            {
                text.text = "";

            }
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, targetVector/50);

            //Debug.DrawRay(transform.position, targetVector, Color.red);
            if (Input.GetKey("up"))
            {
                //print("up arrow key is held down");
                targetVector.y += 0.1f;// ++;
            }

            if (Input.GetKey("down"))
            {
                targetVector.y -= 0.1f;//--;
                //print("down arrow key is held down");
            }
            if (Input.GetKey("right"))
            {
                targetVector.x += 0.1f;//++;
                //print("up arrow key is held down");
            }

            if (Input.GetKey("left"))
            {
                targetVector.x -= 0.1f; //--;
                //print("down arrow key is held down");
            }
            if (Input.GetKey("w"))
            {
                targetVector.z += 0.1f;//++;
                //print("w arrow key is held down");
            }

            if (Input.GetKey("s"))
            {
                targetVector.z -= 0.1f; //--;
                //print("s arrow key is held down");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                status_check = 15;
                targetVector.Normalize();
                Debug.Log(targetVector);
                foreach (var enemy in enemiesList)
                {
                    //Debug.Log(enemy.normals);
                    //Debug.Log(targetVector);
                    if (((int)targetVector.x * 10 == (int)enemy.normals.x * 10 && (int)targetVector.y * 10 == (int)enemy.normals.y * 10 && (int)targetVector.z * 100 == (int)enemy.normals.z * 10) ||
                        ((int)targetVector.x * 10 == -(int)enemy.normals.x * 10 && (int)targetVector.y * 10 == -(int)enemy.normals.y * 10 && (int)targetVector.z * 100 == -(int)enemy.normals.z * 10))
                    //if ((targetVector.x == enemy.normals.x && targetVector.y == enemy.normals.y && targetVector.z == enemy.normals.z) ||
                    //    (targetVector.x == -enemy.normals.x && targetVector.y == -enemy.normals.y && targetVector.z == -enemy.normals.z))
                    {
                        int index = enemiesList.IndexOf(enemy);
                        enemiesList.RemoveAt(index);
                        Destroy(enemy.gameObject);
                        Debug.Log("hit");
                        text.text = "HIT";
                        break;
                    }
                    else
                    {
                        text.text = "MISS";
                        Debug.Log("miss");
                    }
                }
                targetVector = Vector3.forward;
                //print("up arrow key is held down");
            }
            if (Input.GetKey("p"))
            {
                move = !move;
            }
            //Debug.Log("in player");
            foreach (var enemy in enemiesList)
            {
                //Debug.Log((int)enemy.interactionPoint.z);
                //Debug.Log((int)transform.position.z);
                if ((int)enemy.interactionPoint.z == (int)transform.position.z)
                {
                    Debug.Log("loose");
                    //Debug.Log(lives);
                    //text.text = "GAME OVER";
                    int index = enemiesList.IndexOf(enemy);
                    enemiesList.RemoveAt(index);
                    Destroy(enemy.gameObject);
                    lives--;
                    hearts[lives].SetActive(false);
                    break;
                    
                    //Application.Quit();
                }
            }
            if (move)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime);

            }
            if (transform.position == endPlace)
            {
                Debug.Log("win");
                text.text = "WIN";
                lives = -1;
            }
        }
        else
        {
            text.text = "GAME OVER";
        }
    }
}
