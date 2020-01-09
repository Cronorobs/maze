using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour {

    //Variables privadas
    private float moveAmmount = 1f;
    private bool move = false;
    private float trueSpeed;
    private Vector3 endPos;
    private Animator ani;

    //Variables públicas
    public float speed;
    public Maze maze;

    //Propiedades
    public int X { get; set; }
    public int Y { get; set; }

    private void Start()
    {
        endPos = transform.position;
        trueSpeed = speed;
        ani = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (move) { transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime); }
        if (endPos == transform.position) { move = false; }
    }

    private void Update ()
    {

        if (Input.GetKey(KeyCode.LeftShift)) { speed = trueSpeed * 3; }
        if (Input.GetKeyUp(KeyCode.LeftShift)) { speed = trueSpeed; }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !move && maze.map[X - 1, Y] == 1)
        {
            X--;
            endPos = new Vector3(X, 0.5f, -Y);
            move = true;
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !move && maze.map[X + 1, Y] == 1)
        {
            X++;
            endPos = new Vector3(X, 0.5f, -Y);
            move = true;
        }
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && !move && maze.map[X, Y - 1] == 1)
        {
            Y--;
            endPos = new Vector3(X, 0.5f, -Y);
            move = true;
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !move && maze.map[X, Y + 1] == 1)
        {
            Y++;
            endPos = new Vector3(X, 0.5f, -Y);
            move = true;
        }

        if (Input.GetKey(KeyCode.A)) { ani.SetTrigger("Left"); }
        else if (Input.GetKey(KeyCode.D)) { ani.SetTrigger("Right"); }
        else if (Input.GetKey(KeyCode.W)) { ani.SetTrigger("Up"); }
        else if (Input.GetKey(KeyCode.S)) { ani.SetTrigger("Down"); }
    }
}
