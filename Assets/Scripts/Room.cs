using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Room {

    //Propiedades
    public Vector3 Center { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
	public int X1 { get; set; }
    public int X2 { get; set; }
    public int Y1 { get; set; }
    public int Y2 { get; set; }

    public Room(int x, int y, int w, int h)
    {
        X1 = x;
        X2 = x + w;
        Y1 = y;
        Y2 = y + h;
        Center = new Vector3(Mathf.RoundToInt((X1 + X2) / 2), -Mathf.RoundToInt((Y1 + Y2) / 2), 0);
    }

    public bool Intersect(Room r)
    {
        return (X1 <= r.X2 && X2 >= r.X1 && Y1 <= r.Y2 && Y2 >= r.Y1);
    }
}
