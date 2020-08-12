using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchControl : MonoBehaviour
{
    Vector3 position;
    Vector3Int positionInt;
    Vector3Int prevPosition;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.touchCount > 0)
        // {
        //     Touch touch = Input.GetTouch(0);
        //     if (touch.phase == TouchPhase.Ended)
        //     {
        //         position = Camera.main.ScreenToWorldPoint(touch.position);
        //         GameObject.Find("Sound Effects").GetComponent<SEController>().PlayClip();
        //         Debug.Log(position);

        //         if (GetComponent<SceneControl>().currentScene == GetComponent<SceneControl>().game)
        //         {
        //             if (position.y < 20 && position.x < 10 && position.x >= 0 &&
        //                 GameState.state == GameState.State.Normal) 
        //             {
        //                 GetComponent<Game>().DeleteBoxes((int)position.x, (int)position.y);
        //             }
        //         }
        //     }
        // }

        if (GameState.inputState == GameState.InputState.Enable)
        {
            if (Input.GetMouseButton(0)) 
            {//when holding the left mouse button down
                //this makes it so the mouse position uses world position (follows unitys grid)
                position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                positionInt = new Vector3Int((int)position.x, (int)position.y, 0);

                if (GetComponent<SceneControl>().currentScene == GetComponent<SceneControl>().game && prevPosition != positionInt)
                {//code runs only if the mouse moves to a different cell
                    if (GameState.state == GameState.State.Normal)
                    {//selects boxes as normal
                        GetComponent<Game>().SelectBoxes((int)position.x, (int)position.y);                   
                    }
                    else if (GameState.state == GameState.State.Area)
                    {//selects boxes in a 3 x 3 pattern centered around the mouse
                        GetComponent<Game>().SelectArea((int)position.x, (int)position.y);
                    }
                    else if (GameState.state == GameState.State.Horizontal)
                    {//selects a row of boxes based on the mouses x
                        GetComponent<Game>().SelectHorizontal((int)position.x, (int)position.y);
                    }
                    else if (GameState.state == GameState.State.Vertical)
                    {//selects a collumn of boxes based on the mouses y
                        GetComponent<Game>().SelectVertical((int)position.x, (int)position.y);
                    }
                }

                if (GetComponent<SceneControl>().currentScene == GetComponent<SceneControl>().game && GameState.state == GameState.State.Normal)
                {//controlls the num that appears above the mouse - counts boxes
                    if (position.y >= 20 || position.x >= 10 || position.x < 0)
                    {//reset text to nothing when mouse outside play area
                        GameObject.Find("Selected Box Count").GetComponent<Text>().text = "";
                    }
                    else
                    {
                        //changes position of the num text box to the same system as the mouse position
                        Camera.main.ScreenToWorldPoint(GameObject.Find("Selected Box Count").GetComponent<RectTransform>().position);
                        //move text box above the mouse pointer
                        GameObject.Find("Selected Box Count").GetComponent<RectTransform>().position = position + new Vector3(0, Options.boxCountNumHeight, 50);
                        //display the number of selected boxes
                        GameObject.Find("Selected Box Count").GetComponent<Text>().text = GetComponent<Game>().NumOfSelectedBoxes().ToString();
                    }
                }
                //star particle effects
                GetComponent<ParticleManager>().Star(position);

                prevPosition = positionInt;
            }

            

            if (Input.GetMouseButtonUp(0))
            {//when the left mouse button is let go
                //play sound clip
                GameObject.Find("Sound Effects").GetComponent<SEController>().PlayClip();
                //GetComponent<ParticleManager>().Slash(position);

                if (GetComponent<SceneControl>().currentScene == GetComponent<SceneControl>().game)
                {//if in the game
                    if (position.y < 20 && position.x < 10 && position.x >= 0) 
                    {
                        if (GameState.state == GameState.State.Normal)
                        {
                            //make text box above mouse pointer 'dissapear'
                            GameObject.Find("Selected Box Count").GetComponent<Text>().text = "";
                            //break selected boxes
                            GetComponent<Game>().BreakBoxes((int)position.x, (int)position.y);
                        }
                        //reset prevposition, so player can click the same cell twice
                        prevPosition = new Vector3Int(0, -1, 0);
                    }
                    //deselect boxes if player clicked off of any boxes
                    GetComponent<Game>().UnselectBoxes();
                }
            }
        }
    }

    public Vector3 TapPosition()
    {
        return position;
    }
}
