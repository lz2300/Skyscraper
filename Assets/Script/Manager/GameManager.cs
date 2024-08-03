using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class GamePanelData 
{
    //层数
    public int intNum;
    //分数
    public int score;
    //新增使用锁的层数记录
    public int userLockLayer;
    //最大的连续combo层数
    public int userMaxCombo;
    public Vector3 vctPos;
    public GamePanelData()
    {
       
    }
    public GamePanelData(Vector3 pos)
    {
        intNum = 0;
        score = 0;
        userLockLayer = 0;
        userMaxCombo = 0;
        vctPos = pos;
    }
}
public enum GameType 
{
    未开始,
    进行中,
    游戏结束,
}


public class GameManager : MonoBehaviour
{
    public SkyscraperItem ins_ObjBox;
    public Transform objParent;
    [SerializeField]
    private AnimationCurve h_curve, v_curve;
    [SerializeField]
    public Transform gamePanelObj;
    [SerializeField]
    private Transform UIParent;
    [SerializeField]
    private TMP_InputField inputSpeed;

    SkyscraperItem obj;
    //TODO：            后续做成回收池
    public List<SkyscraperItem> tempListBox = new List<SkyscraperItem>(); 

    Vector2 size = new Vector2(2, 1);
    Vector2 offset = new Vector2(0, 2.5f);
    public float moveSpeed = 1f;
    public float moveMass = 2f;
    //射线起始结束
    public Transform linderStarTrf,linderEndTrf;
    public LineRenderer linrender;
    //初始数据
    public GamePanelData data = new GamePanelData();
    Vector3 tempVtPos;
    public Transform starBoxPos;
    //游戏状态
    public GameType gameType = GameType.未开始;
    [Header("完美重合变量")]
    public float curDiff_X = 15;
    public bool isCoincident = false;
    public int intCoincident = 0;

    BoxType _type = BoxType.Box;

    public Button fallingBtn;         //下落事件
    public Button GameTypeBtn;        //游戏箱子变化
    public Text time;                 //游戏时间
    public Text Score;                //游戏分数
    public Text PerfectLayerNum;      //完美重合
    public Text PerfectMaxLayerNum;   //最大完美重合数目  
    public Text curLayer;             //当前层数
    public Text lockLayerText;        //首次使用上锁层数
    public TMP_InputField inputLayer; //限制层数
    public Text MaximumUsageSpeed;    //最多使用速度
    public Text MaximumUsageSpeedTime;//最多使用速度时间
    public Button Again;
    public Button Quit;
    private void Start()
    {
        inputSpeed.text = "1";
        inputLayer.text = "40";
        StartCoroutine(swing(linderEndTrf.gameObject));
        linrender.endWidth = 0.1f;
        linrender.startWidth = 0.1f;
        linrender.startColor = Color.red;
        linrender.endColor = Color.red;
        fallingBtn.onClick.AddListener(FallingEvent);
        GameTypeBtn.onClick.AddListener(GameTypeEvent);
        Again.onClick.AddListener(Init);
        Quit.onClick.AddListener(QuitEvent);
        data.vctPos = objParent.localPosition;
    }
    private void Init()
    {
        objParent.transform.localPosition = data.vctPos;
        for (int i = tempListBox.Count - 1; i >= 0; i--)
        {
            Destroy(tempListBox[i].gameObject);
        }
        temoVt = 0;
        tempListBox.Clear();
        gameType = GameType.进行中;
        data = new GamePanelData(data.vctPos);
        Score.text = data.score.ToString();
        curLayer.text = data.intNum.ToString();
        PerfectLayerNum.text = intCoincident.ToString();
        PerfectMaxLayerNum.text = data.userMaxCombo.ToString();
        StartCoroutine(swing(linderEndTrf.gameObject));
        GameCtroller.Ins.timeManager.Init();
    }
    // Update is called once per frame
    void Update()
    {
        if (gameType == GameType.游戏结束)
        {
            return;
        }
        if (moveSpeed != float.Parse(inputSpeed.text))
        {
            moveSpeed = float.Parse(inputSpeed.text);
        }
        linrender.SetPosition(0, (Vector2)linderStarTrf.position);
        linrender.SetPosition(1, (Vector2)linderEndTrf.position);
    }
    float temoVt = 0;
    Vector3 swingTargetPos;
    IEnumerator swing(GameObject swingTarget)
    {
        while (true)
        {
            swingTarget.transform.position = Vector2.right * h_curve.Evaluate(Time.time * (moveSpeed / moveMass)) * size.x +
                                                                   Vector2.up * v_curve.Evaluate(Time.time * (moveSpeed / moveMass)) * size.y + offset;
            swingTarget.transform.localPosition = new Vector3(swingTarget.transform.localPosition.x, swingTarget.transform.localPosition.y, 0);
            yield return null;
        }
    }
    private void FallingEvent()
    {
        if (!gamePanelObj.gameObject.activeSelf)
        {
            return;
        }
        if (gameType == GameType.游戏结束)
        {
            return;
        }
        if (gameType == GameType.未开始)
        {
            gameType = GameType.进行中;
            //增加累计时间 速度计时
        }
        GameCtroller.Ins.timeManager.AddCumulativeTime(moveSpeed);

        ins_ObjBox = Load.Instance.ItemBox(_type);
        obj = Instantiate(ins_ObjBox, objParent);
        obj.transform.position = linderEndTrf.transform.position;
        obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y,0);
        data.intNum++;
        //游戏结束判定
        temoVt = 0;
        for (int i = 0; i < tempListBox.Count; i++)
        {
            temoVt += GetMovePos(tempListBox[i].itemType);
        }
        temoVt = starBoxPos.localPosition.y + temoVt - GetMovePos(_type)/2 - 100;
        obj.endGamePos = temoVt;
        //判定重合
        if (tempListBox.Count > 0)
        {
            float _curDif = Mathf.Abs(tempListBox[tempListBox.Count - 1].transform.localPosition.x - obj.transform.localPosition.x);
            //增加分数判定 重合
            if (curDiff_X > _curDif)
            {
                intCoincident++;
                if (!isCoincident)
                {
                    isCoincident = true;
                }
                //记录最高连击数
                data.userMaxCombo = intCoincident >= data.userMaxCombo ? intCoincident:data.userMaxCombo;
                //完美重合加分
                data.score += intCoincident > 5 ? (int)_type*5 : (int)_type*intCoincident;
                AddTempListBox(obj);
                return; ;
            }
            isCoincident = false;
            intCoincident = 0;
        }
        data.score += (int)_type;
        AddTempListBox(obj);
    }
    void GameTypeEvent()
    {
        _type = (BoxType)UnityEngine.Random.Range(1, 9);
        SpriteRenderer srd = linderEndTrf.GetComponent<SpriteRenderer>();
        srd.sprite = Load.Instance.ItemBoxSprite(_type);
        switch (_type)
        {
            case BoxType.Box:
            case BoxType.Lock:
                linderEndTrf.localScale = new Vector3(290, 290, 1);
                break;
            case BoxType.Elliptical:
            case BoxType.Garden:
            case BoxType.Rectangle:
            case BoxType.Trapezium:
            case BoxType.Trapezium1:
            case BoxType.Trapezium2:
                linderEndTrf.localScale = new Vector3(100, 100, 1);
                break;
        }
    }

    void AddTempListBox(SkyscraperItem _box)
    {
        tempListBox.Add(_box);
        gamePanelObj.gameObject.SetActive(false);
        GameCtroller.Ins.timeManager.AddDalyTimeAction(1f, () =>
        {
            gamePanelObj.gameObject.SetActive(true);
            SpriteRenderer srd = linderEndTrf.GetComponent<SpriteRenderer>();
            _type = RandomBoxType;
            srd.sprite = Load.Instance.ItemBoxSprite(_type);
            switch (_type)
            {
                case BoxType.Box:
                case BoxType.Lock:
                    linderEndTrf.localScale = new Vector3(290, 290, 1);
                    break;
                case BoxType.Elliptical:
                case BoxType.Garden:
                case BoxType.Rectangle:
                case BoxType.Trapezium:
                case BoxType.Trapezium1:
                case BoxType.Trapezium2:
                    linderEndTrf.localScale = new Vector3(100, 100, 1);
                    break;
            }
        });
        Score.text = data.score.ToString();
        curLayer.text = data.intNum.ToString();
        PerfectLayerNum.text = intCoincident.ToString();
        PerfectMaxLayerNum.text = data.userMaxCombo.ToString();
    }

    BoxType RandomBoxType
    {
        get
        {
           return data.intNum > int.Parse(inputLayer.text) ? (BoxType)UnityEngine.Random.Range(1, 9) : BoxType.Box;
        }
    }

    public void AddPosition()
    {
        if (data.intNum > 1)
        {
            tempVtPos = objParent.transform.localPosition - new Vector3(0, GetMovePos(_type), 0);
            objParent.DOLocalMoveY(tempVtPos.y, 0.5f);
        }
    }
   
    float GetMovePos(BoxType tempType)
    {
            float movePos = 0;
            switch (tempType)
            {
                case BoxType.Box:
                    movePos = 300;
                    break;
                case BoxType.Elliptical:
                    movePos = 251;
                    break;
                case BoxType.Garden:
                    movePos = 311;
                    break;
                case BoxType.Rectangle:
                    movePos = 450;
                    break;
                case BoxType.Trapezium:
                    movePos = 251;
                    break;
                case BoxType.Trapezium1:
                    movePos = 265;
                    break;
                case BoxType.Trapezium2:
                    movePos = 265;
                    break;
                case BoxType.Lock:
                    movePos = 300;
                    break;
            }
            return movePos;
    }
 
    private void QuitEvent()
    {
        Application.Quit();
    }
    internal void LockEvent()
    {
        if (data.userLockLayer == 0)
        {
            data.userLockLayer = data.intNum;
            lockLayerText.text = data.userLockLayer.ToString();
        }
        Rigidbody2D rd;
        for (int i = 0; i < tempListBox.Count; i++)
        {
            rd = tempListBox[i].GetComponent<Rigidbody2D>();
            rd.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            rd.freezeRotation = true;
        }
    }
    internal void GameOver()
    {
        gameType = GameType.游戏结束;
        StopAllCoroutines();
        GameObject over =  Instantiate(Load.Instance.UILoad(UIType.UIGameOverPanel), UIParent);
        over.transform.localPosition = Vector3.zero;

    }
}
