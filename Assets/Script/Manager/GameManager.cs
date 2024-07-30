using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanelData 
{
    public Vector3 vctPos;
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SkyscraperItem ins_ObjBox;
    [SerializeField]
    private Transform objParent;
    [SerializeField]
    private AnimationCurve h_curve, v_curve;
    [SerializeField]
    public Transform gamePanelObj;
    SkyscraperItem obj;
    Vector2 size = new Vector2(2, 1);
    Vector2 offset = new Vector2(0, 2.5f);
    //射线起始结束
    public float moveSpeed = 1f;
    public float moveMass = 2f;
    public Transform linderStarTrf,linderEndTrf;
    public LineRenderer linrender;

    private GamePanelData data;
    private void Start()
    {
        StartCoroutine(swing(linderEndTrf.gameObject));
        linrender.endWidth = 0.1f;
        linrender.startWidth = 0.1f;
        linrender.startColor = Color.red;
        linrender.endColor = Color.red;

        if (data == null)
        {
            data.vctPos = objParent.transform.position;
        }
    }
    private void Init()
    {
        objParent.transform.position = data.vctPos;
    }
    // Update is called once per frame
    void Update()
    {
        linrender.SetPosition(0, (Vector2)linderStarTrf.position);
        linrender.SetPosition(1, (Vector2)linderEndTrf.position);

        if (Input.GetMouseButtonUp(0) && gamePanelObj.gameObject.activeSelf)
        {
            obj = Instantiate(ins_ObjBox, objParent);
            obj.transform.position = new Vector3(linderEndTrf.transform.position.x, linderEndTrf.transform.position.y,0);
            gamePanelObj.gameObject.SetActive(false);
            TimeManager.AddDalyTimeAction(1f, () =>
            {
                gamePanelObj.gameObject.SetActive(true);
            });
        }
    }
    Vector3 temoVt;
    IEnumerator swing(GameObject swingTarget)
    {
     
        while (true)
        {
            swingTarget.transform.position = Vector2.right * h_curve.Evaluate(Time.time * (moveSpeed/ moveMass)) * size.x +
                                                                   Vector2.up * v_curve.Evaluate(Time.time * (moveSpeed/ moveMass)) * size.y + offset;
            yield return null;
        }
    }
}
