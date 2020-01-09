using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Maze : MonoBehaviour {

    //Variables públicas
    public int x, y, roomNumber, minR, maxR;
    public GameObject mazeWallPrefab;
    public GameObject mazeTilePrefab;
    public GameObject playerPrefab;
    public CameraFollow follow;
    public int[,] map;

    //Variables privadas
    private List<Room> rooms;

    private void Start()
    {
        //Inicializamos el mapa
        map = new int[x, y];

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++) { map[i, j] = 0; }
        }

        rooms = new List<Room>();

        //Repartimos habitaciones
        for(int n = 0; n < roomNumber; n++) { NewRoom(); }

        for(int i = 1; i < rooms.Count; i++)
        {
            int x1 = Mathf.RoundToInt(rooms[i - 1].Center.x);
            int x2 = Mathf.RoundToInt(rooms[i].Center.x);
            int y = Mathf.RoundToInt(rooms[i].Center.y);
            HorizontalCorridor(x1, x2, y);

            int y1 = Mathf.RoundToInt(rooms[i - 1].Center.y);
            int y2 = Mathf.RoundToInt(rooms[i].Center.y);
            int x = Mathf.RoundToInt(rooms[i - 1].Center.x);
            VerticalCorridor(y1, y2, x);
        }

        GameObject player = Instantiate(playerPrefab);
        player.transform.position = new Vector3(rooms[0].Center.x, 0.5f, rooms[0].Center.y);
        player.GetComponent<Movement>().maze = this;
        player.GetComponent<Movement>().X = Mathf.RoundToInt(rooms[0].Center.x);
        player.GetComponent<Movement>().Y = -Mathf.RoundToInt(rooms[0].Center.y);
        follow.Target = player.transform;

        //Dibujamos el mapa
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if(map[i, j] == 0)
                {
                    GameObject newTile = Instantiate(mazeWallPrefab);
                    newTile.transform.position = new Vector3(i, 0, -j);
                    newTile.name = "Tile " + i + ", " + j;
                }
                else
                {
                    GameObject newTile = Instantiate(mazeTilePrefab);
                    newTile.transform.position = new Vector3(i, 0, -j);
                    newTile.name = "Tile " + i + ", " + j;
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space)) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    }

    private void NewRoom()
    {
        int randW = Random.Range(minR, maxR);
        int randX = Random.Range(1, x - 1 - randW);
        int randH = Random.Range(minR, maxR);
        int randY = Random.Range(1, y - 1 - randH);
        Room r = new Room(randX, randY, randW, randH);

        bool fail = false;

        foreach (Room ro in rooms)
        {
            if (r.Intersect(ro))
            {
                fail = true;
                break;
            }
        }

        if (!fail)
        {
            rooms.Add(r);
            for (int i = r.X1; i < r.X2; i++)
            {
                for (int j = r.Y1; j < r.Y2; j++) { map[i, j] = 1; }
            }
        }
        else { NewRoom(); }
    }

    private void HorizontalCorridor(int x1, int x2, int y)
    {
        if (x2 > x1)
        {
            for (int i = x1; i <= x2; i++) { map[i, -y] = 1; }
        }
        else
        {
            for (int i = x1; i >= x2; i--) { map[i, -y] = 1; }
        }
        
    }

    private void VerticalCorridor(int y1, int y2, int x)
    {
        if(y2 > y1)
        {
            for (int i = y1; i <= y2; i++) { map[x, -i] = 1; }
        }
        else
        {
            for (int i = y1; i >= y2; i--) { map[x, -i] = 1; }
        }      
    }
}
