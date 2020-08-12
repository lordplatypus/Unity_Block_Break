using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialController : MonoBehaviour
{
    Game game;
    ParticleManager particleManager;
    Toggle hs;
    Toggle vs;
    Toggle areas;
    int x;
    int y;
    int count;
    int slowDownTime;
    GameObject specialSelection;
    int startLeft;
    int startRight;
    int startUp;
    int startDown;
    // Start is called before the first frame update
    void Start()
    {
        game = GetComponent<Game>();
        particleManager = GetComponent<ParticleManager>();
        hs = GameObject.Find("Horizontal Special Toggle").GetComponent<Toggle>();
        hs.interactable = false;
        vs = GameObject.Find("Vertical Special Toggle").GetComponent<Toggle>();
        vs.interactable = false;
        areas = GameObject.Find("Area Special Toggle").GetComponent<Toggle>();
        areas.interactable = false;
        x = 0;
        y = 0;
        count = 0;
        slowDownTime = 0;
    }

    // Update is called once per frame
    void Update()
    {//deleating the boxes in the Update() as it looks cooler and so not all boxes are destroyed at once
        if (GameState.state != GameState.State.Normal && Input.GetMouseButtonUp(0)) 
        {
            Vector3 temp = GetComponent<TouchControl>().TapPosition();
            x = (int)temp.x;
            y = (int)temp.y;
        }

        if (count == 5)
        {//all specials end with count = 5
            count = 0; //reset counter
            game.UpdateBoxes(); //update boxes a frame after the special finishes calculating
            GameState.inputState = GameState.InputState.Enable; //allow input
        }

        if (GameState.state == GameState.State.Horizontal && x < 10 && y < 20 && slowDownTime % 3 == 0)
        {
            if (count == 0) 
            {//first frame of the horizontal special
                particleManager.Slash(new Vector3(3, y, 0)); //slash special effect
                particleManager.Slash(new Vector3(7, y, 0)); //used 2 to fill the entire row
                startRight = game.CheckHorizontalRight(0, y); //start by checking the left most box
                GameState.inputState = GameState.InputState.Disable; //disable input
                count ++; 
            }
            else if (count < 4) count++; //adds a delay
            else
            {
                if (startRight < Options.width)
                {//destroy boxes to the right of the mouse click
                    game.DeleteBoxesAtLocation(startRight, y);
                    startRight = game.CheckHorizontalRight(startRight + 1, y);
                }
            }
            if (startRight > Options.width) 
            {//reset
                GameState.state = GameState.State.Normal;
                hs.isOn = false;
                hs.interactable = false;
                Destroy(specialSelection);
                count++; //count = 2 - next frame this causes the boxes to update
            }
        }

        if (GameState.state == GameState.State.Vertical && x < 10 && y < 20 && slowDownTime % 3 == 0)
        {
            if (count == 0) 
            {
                particleManager.SlashV(new Vector3(x, 5, 0));
                particleManager.SlashV(new Vector3(x, 10, 0));
                particleManager.SlashV(new Vector3(x, 15, 0));
                startDown = game.CheckVerticalDown(x, Options.height - 1);
                GameState.inputState = GameState.InputState.Disable;
                count++;
            }
            else if (count < 4) count++;
            else
            {
                if (startDown >= 0)
                {//destroy boxes below the mouse click
                    game.DeleteBoxesAtLocation(x, startDown);
                    startDown = game.CheckVerticalDown(x, startDown - 1);
                }
            }
            if (startDown < 0) 
            {//reset
                GameState.state = GameState.State.Normal;
                vs.isOn = false;
                vs.interactable = false;
                Destroy(specialSelection);
                count++;
            }
        }

        if (GameState.state == GameState.State.Area && x < 10 && y < 20)
        {
            if (count == 0)
            {//had to use the counter because it wouldn't delete the boxs and update them in the same frame
                GameState.inputState = GameState.InputState.Disable;
                for (int x2 = x - 1; x2 <= x + 1; x2++)
                {
                    for (int y2 = y - 1; y2 <= y + 1; y2++)
                    {
                        if (x2 >= 0 && x2 < Options.width &&
                            y2 >= 0 && y2 < Options.height) game.DeleteBoxesAtLocation(x2, y2);
                    }
                }
                count++;
            }
            else
            {
                GameState.state = GameState.State.Normal;
                areas.isOn = false;
                areas.interactable = false;
                Destroy(specialSelection);
                count = 5;
            }
        }
        
        //if the state is anything but 'Normal' increment the 'slowDownTime' counter
        if (GameState.state != GameState.State.Normal) slowDownTime++;
    }

    public bool ActivateHorizontal()
    {//Game calls these functions to make to the special buttons availible to be clicked
        if (hs.interactable) return true; //if already active return true
        else
        {//if in-active
            hs.interactable = true; //activate
            return false; //and return false
        }
    }

    public bool ActivateVertical()
    {
        if (vs.interactable) return true;
        else
        {
            vs.interactable = true; 
            return false;
        }
    }

    public void ActivateArea()
    {
        areas.interactable = true;
    }

    public void ClickedHorizontal()
    {//Special buttons call these functions so the player can actually use the ability
        if (hs.isOn) 
        {
            GameState.state = GameState.State.Horizontal;
            specialSelection = Instantiate(GameObject.Find("Game").GetComponent<ObjectReference>().Selection, new Vector3(10, 17, 0), Quaternion.identity);
        }
        else 
        {
            GameState.state = GameState.State.Normal;
            Destroy(specialSelection);
        }       
    }

    public void ClickedVertical()
    {
        if (vs.isOn) 
        {
            GameState.state = GameState.State.Vertical;
            specialSelection = Instantiate(GameObject.Find("Game").GetComponent<ObjectReference>().Selection, new Vector3(10, 15, 0), Quaternion.identity);
        }
        else 
        {
            GameState.state = GameState.State.Normal;
            Destroy(specialSelection);
        } 
    }

    public void ClickedArea()
    {       
        if (areas.isOn) 
        {
            GameState.state = GameState.State.Area;
            specialSelection = Instantiate(GameObject.Find("Game").GetComponent<ObjectReference>().Selection, new Vector3(10, 19, 0), Quaternion.identity);
        }
        else 
        {
            GameState.state = GameState.State.Normal;
            Destroy(specialSelection);
        } 
    }

    public void ResetSpecials()
    {//when the game is reset
        hs.interactable = false;
        vs.interactable = false; 
        areas.interactable = false;
    }
}
