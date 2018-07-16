using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Transform pointPrefab;
    public int iterations;
    Transform[] points;
    Vector3 scale;
    Vector3 position;
    public GraphFunctionName functionNames;
    static readonly GraphFunction[] functions = { SineFunction, MultiSineFunction };


    private void Awake()
    {

        points = new Transform[iterations];

        float step = 2f / iterations;
        scale = Vector3.one * step;
        position.y = 0f;
        position.z = 0f;
        for(int i = 0; i < iterations; i++)
        {
            Transform point = Instantiate(pointPrefab);

            position.x = (i + 0.5f) * step - 1f;
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
       
   }

    private void Update()
    {
        float time = Time.time;
        GraphFunction f = functions[(int)functionNames];
        for(int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = f(position.x, time);
          //  Debug.LogFormat("X: " + position.x.ToString());
           // Debug.LogFormat("Y: " + position.y.ToString());
            point.localPosition = position;
        }
    }

   static float SineFunction(float x, float t)
    {

        return Mathf.Sin(Mathf.PI * (x + t));
    }

   static float MultiSineFunction(float x, float t)
    {
        float y;

        y = Mathf.Sin(Mathf.PI * (x + t));
        y += Mathf.Sin(2 * Mathf.PI * (x + t));
        Debug.LogFormat("from MultiSin float y: " + y.ToString());
        y *= 0.5f;
        Debug.LogFormat("from MultiSin float y /2 : " + y.ToString());
        y *= 2f / 3f;
        Debug.LogFormat("from MultiSin float y / 1.5 : " + y.ToString());

        return y;
    }


}
