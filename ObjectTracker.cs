using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is optional and is what I was using to make the graphs in the simulation
public class ObjectTracker : MonoBehaviour
{
    public GameObject[] foodList;
    public GameObject[] agentList;
    public GameObject[] enemyList;
    List<float> foodData = new List<float>();
    List<float> agentData = new List<float>();
    List<float> enemyData = new List<float>();

    int i = 0;

    float time = 0;


    int lengthOfLineRenderer = 1000000;

    LineRenderer lineRenderer;
    LineRenderer lineRenderer2;
    LineRenderer lineRenderer3;

    GameObject go1;
    GameObject go2;
    GameObject go3;

    Vector3[] originalData = new Vector3[1000000];
    Vector3[] originalData2 = new Vector3[1000000];
    Vector3[] originalData3 = new Vector3[1000000];

    float scalingFactor = 2.2f;
    // Start is called before the first frame update
    void Start()
    {
        go1 = new GameObject();
        go2 = new GameObject();
        go3 = new GameObject();

        

        go1.transform.parent = this.transform;
        go2.transform.parent = this.transform;
        go3.transform.parent = this.transform;

        lineRenderer = go1.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 1f;
        lineRenderer.positionCount = lengthOfLineRenderer;
        lineRenderer.useWorldSpace = false;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.yellow, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;

        lineRenderer2 = go2.AddComponent<LineRenderer>();
        lineRenderer2.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer2.widthMultiplier = 1f;
        lineRenderer2.positionCount = lengthOfLineRenderer;
        lineRenderer2.useWorldSpace = false;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha2 = 1.0f;
        Gradient gradient2 = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.green, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer2.colorGradient = gradient;

        lineRenderer3 = go3.AddComponent<LineRenderer>();
        lineRenderer3.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer3.widthMultiplier = 1f;
        lineRenderer3.positionCount = lengthOfLineRenderer;
        lineRenderer3.useWorldSpace = false;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha3 = 1.0f;
        Gradient gradient3 = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.blue, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer3.colorGradient = gradient;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time+=Time.fixedDeltaTime;
        foodList = GameObject.FindGameObjectsWithTag("Food");
        agentList = GameObject.FindGameObjectsWithTag("Agent");
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");

        //if time save data point
        if(time > 100)
        {
            
            foodData.Add(foodList.Length);
            agentData.Add(agentList.Length);
            enemyData.Add(enemyList.Length);
            UpdateDataPoints(foodList.Length/10,agentList.Length/10,enemyList.Length/10);
            i+=1;
            time = 0;
        }
        this.transform.position = new Vector3(-100,0,100);
    }

    void UpdateDataPoints(float x,float y,float z)
    {

        // GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // sphere.transform.position = new Vector3(i,x, 0);
        // sphere.GetComponent<Renderer>().material.color = new Color(255,0,0);

        lineRenderer.positionCount = i+1;
        originalData[i] = new Vector3((i),z+1, 0);
        lineRenderer.SetPosition( i,new Vector3((i*scalingFactor), x+1, 0));


        // GameObject sphere2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // sphere2.transform.position = new Vector3(i,y, 0);
        // sphere2.GetComponent<Renderer>().material.color = new Color(0,255,0);

        lineRenderer2.positionCount = i+1;
        originalData2[i] = new Vector3((i),z+1, 0);
        lineRenderer2.SetPosition(i, new Vector3((i*scalingFactor),y+1, 0));
        

        // GameObject sphere3 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // sphere3.transform.position = new Vector3(i,z, 0);
        // sphere3.GetComponent<Renderer>().material.color = new Color(0,0,255);

        lineRenderer3.positionCount = i+1;
        originalData3[i] = new Vector3((i),z+1, 0);
        lineRenderer3.SetPosition(i, new Vector3((i*scalingFactor),z+1, 0));

        if(scalingFactor*i > 100)
        {
            scalingFactor = scalingFactor*.9f;
            Vector3[] test = new Vector3[1000000] ; 
            Vector3[] test2 = new Vector3[1000000] ; 
            Vector3[] test3 = new Vector3[1000000] ; 
            lineRenderer.GetPositions(test);
            lineRenderer2.GetPositions(test2);
            lineRenderer3.GetPositions(test3);
            // Debug.Log(test.Length);
            for(int j =0; j < test.Length; j++)
            {

                test[j].x = originalData[j].x*scalingFactor;
                test2[j].x = originalData2[j].x*scalingFactor;
                test3[j].x = originalData3[j].x*scalingFactor;
            }
            lineRenderer.SetPositions(test);
            lineRenderer2.SetPositions(test2);
            lineRenderer3.SetPositions(test3);
        }

    }
}
