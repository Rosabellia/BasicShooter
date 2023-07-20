using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<GameObject> roomPrefabs;
    public int rows = 4;
    public int colums = 4;
    public float roomWidth = 200f;
    public float roomHeight = 200f;
    private Room[,] grid;
    public int mapSeed = 45;
    public enum RandomType {  Seeded, Random, MapOfTheDay}
    public RandomType randomType = RandomType.Seeded;

    private void Start()
    {
        //GenerateMap();
    }

    public GameObject RandomRoomPrefab()
    {
       return roomPrefabs[UnityEngine.Random.Range(0, roomPrefabs.Count)];
    }

    public int DateToInt(DateTime dateToUse)
    {
        // Add our data up and return it
        return (dateToUse.Month * 1000000)  + (dateToUse.Day * 10000) + dateToUse.Year;
    }
    public int DateAndTimeToInt(DateTime dateToUse)
    {
        return DateToInt(dateToUse) + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }

    public void GenerateMap()
    {
        switch (randomType)
        {
            case RandomType.Seeded:

                break;
            case RandomType.Random:
                mapSeed = DateAndTimeToInt(DateTime.Now);
                break;
            case RandomType.MapOfTheDay:
                mapSeed = DateToInt(DateTime.Now.Date);
                break;
            default:
                Debug.LogWarning("Unreconized type of map");
                break;
        }

        UnityEngine.Random.InitState(mapSeed);

        // Clear out the grid - "column" is out X, "row" is our Y
        grid = new Room[colums, rows];

        // For each row
        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            // Each colume
            for (int currentColumn = 0; currentColumn < colums; currentColumn++)
            {
                // Figure out the location
                float xposition = roomWidth * currentColumn;
                float zposition = roomHeight * currentRow;
                Vector3 newPosition = new Vector3 (xposition, 0f, zposition);

                // Create a new grid at the appropriate location
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;

                // Set its parent
                tempRoomObj.transform.parent = this.transform;

                // Give it a meaningful name
                tempRoomObj.name = "Room_" + currentColumn + "," + currentRow;

                // Get the room object
                Room tempRoom = tempRoomObj.GetComponent<Room>();

                // Save it to the grid array
                grid[currentColumn, currentRow] = tempRoom;

                if (currentRow == 0) // Bottom
                {
                    grid[currentColumn, currentRow].doorSouth.SetActive (true);
                }
                if(currentRow == rows-1)
                {
                    grid[currentColumn, currentRow].doorNorth.SetActive(true);
                }
                if (currentColumn == 0) // Bottom
                {
                    grid[currentColumn, currentRow].doorWest.SetActive(true);
                }
                if (currentColumn == colums - 1)
                {
                    grid[currentColumn, currentRow].doorEast.SetActive(true);
                }
            }
        }
    }

}
