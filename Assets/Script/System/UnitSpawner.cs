using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public GameObject[] BasicSpawn,SpecialSpawn;
    public gameData g_Data;
    public AnimationCurve h_curve, v_curve;
    public Vector2 size;
    public float curveScale = 1;
    public Vector2 offset;
    public GameObject insObj;
    int specialBase = 0;
    int specialStep = 10;
    LineRenderer lineRenderer;
    private void Awake()
    {
        //SpawnerFuntion();
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        if (insObj)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, insObj.transform.position);
            lineRenderer.SetPosition(1, (Vector2)Camera.main.transform.position + Vector2.up * 6);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
    public void SpawnerFuntion()
    {
        if (!insObj)
        {
            g_Data.UnitsCount++;
            if (g_Data.UnitsCount-specialBase>=specialStep)
            {
                specialBase = g_Data.UnitsCount;
                specialStep++;
                insObj = Instantiate(SpecialSpawn[0]);
            }
            else
            {
                insObj = Instantiate(BasicSpawn[Random.Range(0,BasicSpawn.Length)]);
            }
            insObj.name = "Unit" + g_Data.UnitsCount;
            insObj.GetComponent<unitBase>().id = g_Data.UnitsCount;
            StartCoroutine(swing(insObj));
        }
    }
    public void ReliceHolding()
    {
        insObj.GetComponent<Rigidbody2D>().isKinematic = false;
        insObj = null;
        StopAllCoroutines();
    }
    IEnumerator swing(GameObject swingTarget)
    {
        Rigidbody2D rig2d = swingTarget.GetComponent<Rigidbody2D>();
        rig2d.isKinematic = true;
        while (true)
        {
            if (g_Data.GameMode != gameData.gameMode.playing) yield break;
            swingTarget.transform.position = Vector2.right * h_curve.Evaluate(Time.time*(1.0f/rig2d.mass))*size.x+
                                                                   Vector2.up * v_curve.Evaluate(Time.time * (1.0f / rig2d.mass))  * size.y+offset+(Vector2)Camera.main.transform.position;
            yield return null;
        }
    }
}
