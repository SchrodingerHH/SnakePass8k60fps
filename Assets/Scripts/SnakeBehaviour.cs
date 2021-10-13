using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBehaviour : MonoBehaviour
{
    [SerializeField] GameObject SnakeTailPrefab;
    public List<Vector3> prevCoords;
    public List<GameObject> snakeTails;
    
    public enum Move
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3
    }

    private void Start()
    {
        prevCoords = new List<Vector3>();
        snakeTails = new List<GameObject>();
        StartCoroutine(SnakeMovement());
    }


    private void Update()
    {
        //Vector3 rotateDir = new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"),0).normalized;
        //transform.Rotate()
        if (Input.GetKeyDown(KeyCode.W))
        {
            RotateHead(Move.Up);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            RotateHead(Move.Left);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            RotateHead(Move.Down);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            RotateHead(Move.Right);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddTail();
        }
    }

    IEnumerator SnakeMovement()
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(1);

            transform.Translate(1,0,0);

            if (snakeTails.Count+1>prevCoords.Count)
            {
                prevCoords.Insert(0,transform.position);
            }
            else
            {
                prevCoords.Insert(0, transform.position);
                prevCoords.RemoveAt(prevCoords.Count - 1);
            }

            for (int i = 0; i < snakeTails.Count; i++)
            {
                snakeTails[i].transform.position = prevCoords[i + 1];
            }
        }
    }

    void RotateHead(Move move)
    {
        switch (move)
        {
            case Move.Up:
                if (transform.rotation.z != 270)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                }
                break;
            case Move.Down:
                if (transform.rotation.z != 90)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 270);
                }
                break;
            case Move.Left:
                if (transform.rotation.z != 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                }
                break;
            case Move.Right:
                if (transform.rotation.z != 180)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "fruit")
        {
            AddTail();
        }
        if (collision.tag == "tail")
        {
            Destroy(transform.parent.gameObject);
        }
    } 

    void AddTail()
    {
        GameObject tail = Instantiate(SnakeTailPrefab,transform.parent);
        snakeTails.Add(tail);
    }


    // Добавить экран проигрыша
    // Добавить звуки 
    // Добавить ускорение змеи от количества съеденых фруктов (5)

}
