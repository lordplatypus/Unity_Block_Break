using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    ObjectReference box;
    TouchControl tc;
    SpecialController sc;
    System.Random rand = new System.Random();
    GameObject[,] map;
    GameObject[,] randomMap;
    GameObject[] selectedBoxes;

    GameObject[] selections;
    int prevH = -1;
    int prevV = -1;
    GameObject prevSelectedBox = null;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<ObjectReference>();
        tc = GetComponent<TouchControl>();
        sc = GetComponent<SpecialController>();
        randomMap = MakeArray(Options.width, Options.height);
        selectedBoxes = new GameObject[Options.width * Options.height];

        selections = new GameObject[Options.width * Options.height];
        map = new GameObject[Options.width, Options.height];
        SetBoxes();
        SetSelection();
    }

    public void Reset()
    {
        for (int x = 0; x < Options.width; x++)
        {
            for (int y = 0; y < Options.height; y++)
            {
                if (map[x, y] != null) map[x, y].GetComponent<BoxControl>().Break();
            }
        }
        randomMap = MakeArray(Options.width, Options.height);
        map = new GameObject[Options.width, Options.height];
        SetBoxes();
        sc.ResetSpecials();
        GameObject.Find("Winning Text").GetComponent<WinningMessage>().ResetText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject[,] MakeArray(int width, int height)
    {
        GameObject[,] randomMap = new GameObject[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int num = rand.Next(0, Options.numColor);
                if (num == 0) randomMap[x, y] = box.Blue;
                if (num == 1) randomMap[x, y] = box.Cyan;
                if (num == 2) randomMap[x, y] = box.Green;
                if (num == 3) randomMap[x, y] = box.Orange;
                if (num == 4) randomMap[x, y] = box.Purple;
                if (num == 5) randomMap[x, y] = box.Red;
                if (num == 6) randomMap[x, y] = box.White;
                if (num == 7) randomMap[x, y] = box.Yellow;
            }
        }
        return randomMap;
    }

    void SetBoxes()
    {
        for (int x = 0; x < Options.width; x++)
        {
            for (int y = 0; y < Options.height; y++)
            {
                map[x, y] = Instantiate(randomMap[x, y], new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }

    void SetSelection()
    {
        for (int i = 0; i < Options.width * Options.height; i++)
        {
            selections[i] = Instantiate(box.Selection, new Vector3(0, -1, 0), Quaternion.identity);
        }
        //selections = GameObject.FindGameObjectsWithTag("Selection");
    }

    public void SelectBoxes(int tapX, int tapY)
    {
        GameObject meh = box.Selection;

        //if the mouse position is not within the 'game play' area, return
        if (tapX < 0 || tapX > 9 || tapY > 19) 
        {
            prevSelectedBox = null;
            UnselectBoxes();
            return;
        }
        if (map[tapX, tapY] == null) 
        {//if the mouse position is on a null box
            //this fixes clicking a box, moving to a null box, and then trying to select the box again (it failed)
            prevSelectedBox = null;
            //unselects boxes if the player clicks on a box then moves to a null box before unclicking
            UnselectBoxes();
            return;
        }
        if (prevSelectedBox != null && prevSelectedBox.tag == map[tapX, tapY].tag)
        {
            return;
        }
        //if the mouse has not moved from the current box, return. Saves the computer from running the below code every frame
        //if (map[tapX, tapY] == prevSelectedBox) return;
        //reset selected boxes if mouse pointer moved to a diff box
        //else UnselectBoxes();
        UnselectBoxes();
        
        bool notDone = true;
        int selectedBoxArrayCounter = 1;
        selectedBoxes[0] = map[tapX, tapY];
        prevSelectedBox = selectedBoxes[0];
        //Instantiate(meh, new Vector3(tapX, tapY, 0), Quaternion.identity);
        selections[0].GetComponent<Transform>().position = selectedBoxes[0].GetComponent<Transform>().position;
        string selectedBoxTag = selectedBoxes[0].tag;
        selectedBoxes[0].GetComponent<BoxControl>().IsSelected();

        while(notDone)
        {
            notDone = false;
            foreach (GameObject box in selectedBoxes)
            {
                if (box == null) continue;
                int currentBoxX = (int)box.GetComponent<Transform>().position.x;
                int currentBoxY = (int)box.GetComponent<Transform>().position.y;
                if (currentBoxX != Options.width - 1 && map[currentBoxX + 1, currentBoxY] != null && map[currentBoxX + 1, currentBoxY].tag == selectedBoxTag && 
                    !map[currentBoxX + 1, currentBoxY].GetComponent<BoxControl>().isSelected)
                {//check right
                    selectedBoxes[selectedBoxArrayCounter] = map[currentBoxX + 1, currentBoxY];
                    selectedBoxes[selectedBoxArrayCounter].GetComponent<BoxControl>().IsSelected();
                    //Instantiate(meh, new Vector3(currentBoxX + 1, currentBoxY, 0), Quaternion.identity);
                    selections[selectedBoxArrayCounter].GetComponent<Transform>().position = selectedBoxes[selectedBoxArrayCounter].GetComponent<Transform>().position;
                    selectedBoxArrayCounter++;
                    notDone = true;
                }
                if (currentBoxX != 0 && map[currentBoxX - 1, currentBoxY] != null && map[currentBoxX - 1, currentBoxY].tag == selectedBoxTag &&
                    !map[currentBoxX - 1, currentBoxY].GetComponent<BoxControl>().isSelected)
                {//check left
                    selectedBoxes[selectedBoxArrayCounter] = map[currentBoxX - 1, currentBoxY];
                    selectedBoxes[selectedBoxArrayCounter].GetComponent<BoxControl>().IsSelected();
                    //Instantiate(meh, new Vector3(currentBoxX - 1, currentBoxY, 0), Quaternion.identity);
                    selections[selectedBoxArrayCounter].GetComponent<Transform>().position = selectedBoxes[selectedBoxArrayCounter].GetComponent<Transform>().position;
                    selectedBoxArrayCounter++;
                    notDone = true;
                }
                if (currentBoxY != Options.height - 1 && map[currentBoxX, currentBoxY + 1] != null && map[currentBoxX, currentBoxY + 1].tag == selectedBoxTag && 
                    !map[currentBoxX, currentBoxY + 1].GetComponent<BoxControl>().isSelected)
                {//check above
                    selectedBoxes[selectedBoxArrayCounter] = map[currentBoxX, currentBoxY + 1];
                    selectedBoxes[selectedBoxArrayCounter].GetComponent<BoxControl>().IsSelected();
                    //Instantiate(meh, new Vector3(currentBoxX, currentBoxY + 1, 0), Quaternion.identity);
                    selections[selectedBoxArrayCounter].GetComponent<Transform>().position = selectedBoxes[selectedBoxArrayCounter].GetComponent<Transform>().position;
                    selectedBoxArrayCounter++;
                    notDone = true;
                }
                if (currentBoxY != 0 && map[currentBoxX, currentBoxY - 1] != null && map[currentBoxX, currentBoxY - 1].tag == selectedBoxTag && 
                    !map[currentBoxX, currentBoxY - 1].GetComponent<BoxControl>().isSelected)
                {//check below
                    selectedBoxes[selectedBoxArrayCounter] = map[currentBoxX, currentBoxY - 1];
                    selectedBoxes[selectedBoxArrayCounter].GetComponent<BoxControl>().IsSelected();
                    //Instantiate(meh, new Vector3(currentBoxX, currentBoxY - 1, 0), Quaternion.identity);
                    selections[selectedBoxArrayCounter].GetComponent<Transform>().position = selectedBoxes[selectedBoxArrayCounter].GetComponent<Transform>().position;
                    selectedBoxArrayCounter++;
                    notDone = true;
                }
            }
        }
    }

    public void SelectArea(int tapX, int tapY)
    {
        int countX = tapX - 1;
        int countY = tapY - 1;
        for (int i = 0; i < 9; i++)
        {
            if (countX < 0 || countX >= Options.width || countY < 0 || countY >= Options.height || map[countX, countY] == null)
            {
                selections[i].GetComponent<Transform>().position = new Vector3(0, -1, 0);
            }
            else selections[i].GetComponent<Transform>().position = map[countX, countY].GetComponent<Transform>().position;
            countY++;
            if (countY == tapY + 2)
            {
                countX++;
                countY = tapY - 1;
            }
        }
    }

    public void SelectHorizontal(int tapX, int tapY)
    {
        if (tapY == prevH) return;

        for (int x = 0; x < Options.width; x++)
        {
            if (tapX < 0 || tapX >= Options.width || tapY < 0 || tapY >= Options.height || map[x, tapY] == null)
            {
                selections[x].GetComponent<Transform>().position = new Vector3(0, -1, 0);
            }
            else selections[x].GetComponent<Transform>().position = map[x, tapY].GetComponent<Transform>().position;
        }   

        prevH = tapY;
    }

    public void SelectVertical(int tapX, int tapY)
    {
        if (tapX == prevV) return;

        for (int y = 0; y < Options.height; y++)
        {
            if (tapX < 0 || tapX >= Options.width || tapY < 0 || tapY >= Options.height || map[tapX, y] == null)
            {
                selections[y].GetComponent<Transform>().position = new Vector3(0, -1, 0);
            }
            else selections[y].GetComponent<Transform>().position = map[tapX, y].GetComponent<Transform>().position;
        }

        prevV = tapX;
    }

    public void UnselectBoxes()
    {//this function unselects boxes
        for (int x = 0; x < Options.width; x++)
        {
            for (int y = 0; y < Options.height; y++)
            {
                if (map[x, y] == null) continue;
                if (map[x, y].GetComponent<BoxControl>().isSelected)
                {
                    map[x, y].GetComponent<BoxControl>().IsSelected();
                }
            }
        }

        //DeleteSelection();

        foreach(GameObject s in selections)
        {
            if (s.GetComponent<Transform>().position != new Vector3(0, -1, 0))
            {
                s.GetComponent<Transform>().position = new Vector3(0, -1, 0);
            }
        }

        //remake array
        selectedBoxes = new GameObject[Options.width * Options.height];
    }

    void DeleteSelection()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Selection");

        foreach(GameObject selection in temp)
        {
            int x = (int)selection.GetComponent<Transform>().position.x;
            int y = (int)selection.GetComponent<Transform>().position.y;
            if (x >= 0 && x < 10 && y < 20)
            {
                selection.GetComponent<SelectionController>().Kill();
            }
        }
    }

    public int NumOfSelectedBoxes()
    {//returns the num of currently selected boxes
        int count = 0;
        foreach (GameObject box in selectedBoxes)
        {
            if (box == null) continue;
            count++;
        }
        return count;
    }

    public void BreakBoxes(int x, int y)
    {
        if (map[x, y] == null) return;
        int count = NumOfSelectedBoxes();
        if (count == 1) 
        {
            //fixes a bug where the player selects a box and then moves the mouse diagnaly to a box of the same color
            //(must have no like colors nearby) and then unclicks. If that happens then will no longer be able to select
            //boxes of that color until they select a diff color
            prevSelectedBox = null;
            return;
        }

        foreach (GameObject box in selectedBoxes)
        {
            if (box == null) continue;
            box.GetComponent<BoxControl>().Break();
            map[(int)box.GetComponent<Transform>().position.x, (int)box.GetComponent<Transform>().position.y] = null;
        }

        //This is where specials become interactable
        bool alreadyActive = false; //check to see if the special is already active
        if (count >= 12) 
        {
            alreadyActive = sc.ActivateVertical();
            //if vertical is active, activate horizontal instead
            if (alreadyActive) alreadyActive = sc.ActivateHorizontal();
            //if both vertical and horizontal are active, activate area instead
            if (alreadyActive) sc.ActivateArea();
        }
        else if (count >= 10) 
        {
            alreadyActive =  sc.ActivateHorizontal();
            if (alreadyActive) sc.ActivateArea();
        }
        else if (count >= 8) sc.ActivateArea();

        UpdateBoxes();
    }

    public int CheckHorizontalRight(int x, int y)
    {
        for (int i = x; i < Options.width; i++)
        {
            if (map[i, y] == null) continue;
            else return i;
        }
        return Options.width + 1;
    }

    public int CheckVerticalDown(int x, int y)
    {
        for (int i = y; i >= 0; i--)
        {
            if (map[x, i] == null) continue;
            else return i;
        }
        return -2;
    }

    public void DeleteBoxesAtLocation(int x, int y)
    {
        if (map[x, y] == null) return;
        map[x, y].GetComponent<BoxControl>().Break();
    }

    public void UpdateBoxes()
    {
        DropBoxes();
        MoveBoxesLeft();
        CheckWinCondition();
        Debug.Log("meh");
    }

    void DropBoxes()
    {
        for (int x = 0; x < Options.width; x++)
        {//moves along the grid on the x-axis
            for (int y = 0; y < Options.height; y++)
            {//moves along the grid on the y-axis
                if (map[x, y] == null)
                {//if there is an empty box...
                    bool foundBox = false;
                    for (int y2 = y + 1; y2 < Options.height; y2++)
                    {//moves along the grid on the y-axis from the position a null box was found
                        if (map[x, y2] != null)
                        {//once a non-null box is found move it down to where the null box was
                            map[x, y2].GetComponent<Transform>().transform.position = new Vector2(x, y);
                            map[x, y] = map[x, y2];
                            map[x, y2] = null;
                            foundBox = true;
                        }
                        if (foundBox) break; //a box was found so there is no longer to continue the search
                    }
                }
            }
        }
    }

    void MoveBoxesLeft()
    {
        for (int x = 0; x < Options.width; x++)
        {//moves along the grid on the x-axis
            bool notDone = true;
            if (map[x, 0] == null)
            {//because all boxes where moved down, if map[x, 0] = null that means that the colume is empty
                for (int x2 = x + 1; x2 < Options.width; x2++)
                {//moves along the grid on the x-axis from where a null box was found
                    if (map[x2, 0] != null)
                    {//if a box is found start moving the colume of boxes left to where the null box was found
                        for (int y = 0; y < Options.height; y++)
                        {//moves along the grid on the y-axis
                            if (map[x2, y] != null)
                            {//if a non-null box is found move the box left to where the empty colume is
                                map[x2, y].GetComponent<Transform>().transform.position = new Vector2(x, y);
                                map[x, y] = map[x2, y];
                                map[x2, y] = null;
                            }                  
                        }
                        notDone = false;
                    }
                    //if all boxes to right of the null box are also null, stop the function here
                    else if (x2 == Options.width - 1) return;
                    //a colume was found so there is no longer to continue the search
                    if (!notDone) break;
                }
            }
        }
    }

    void CheckWinCondition()
    {
        if (map[0, 0] == null) GameObject.Find("Winning Text").GetComponent<WinningMessage>().DisplayWinningText();
    }
}
