using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used to control the speed of the simulation put it on an empty game object in the scene to use it
//It has some issues but it works for the most part
public class SimSpeedController : MonoBehaviour
{
    float fpsAvg = 0;
    private float timeSum;
    public bool autoAdjust;
    public float gameSpeed = 1;




    void Update() {
        //if the space bar is pressed, change autoAdjust to the opposite of what it currently is
        if (Input.GetKeyDown(KeyCode.Space))
        {
            autoAdjust = !autoAdjust;
        }

        //calculate fps for update not fixed update
        float fps = 1.0f / Time.unscaledDeltaTime;

        //get moving average of fps
        fpsAvg = (fps + fpsAvg * 2) / 3;

        timeSum += Time.deltaTime;

        if (timeSum > 1)
        {
            timeSum = 0;
            if(autoAdjust)
            {
                AdjustGameSpeed();
            }
            else
            {
                Time.timeScale = gameSpeed;
            }
                
        }
    }

    void AdjustGameSpeed() {
        if (fpsAvg < 60)
        {
            gameSpeed = gameSpeed * .9f;
        }
        else if (fpsAvg > 60 && gameSpeed < 100)
        {
            gameSpeed = gameSpeed * 1.1f;
        }


        //keeps the game speed between 0.1 and 100 to prevent the game from freezing or crashing
        if(gameSpeed < 0.1f){
            gameSpeed = 0.1f;
        }
        else if(gameSpeed > 100)
        {
            gameSpeed = 100;
        }

        Time.timeScale = gameSpeed;
    }
}
