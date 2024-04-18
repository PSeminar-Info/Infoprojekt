using UnityEngine;

public class ConGUI : MonoBehaviour
{
    public Transform mainCamera;
    public Transform cameraTrs;
    public int rotSpeed = 20;
    public GameObject[] effectObj;
    public GameObject[] effectObProj;
    private int arrayNo;
    private int cameraRotCon = 1;
    private readonly string[] cameraState = { "Camera move", "Camera stop" };

    private bool haveProFlg;
    private Vector3 initPos;
    private GameObject nonProFX;

    private GameObject nowEffectObj;

    private float num;
    private float numBck;

    private Vector3 tmpPos;

    private void Start()
    {
        tmpPos = initPos = mainCamera.localPosition;

        haveProFlg = visibleBt();
    }

    private void Update()
    {
        if (cameraRotCon == 1) cameraTrs.Rotate(0, rotSpeed * Time.deltaTime, 0);

        if (num > numBck)
        {
            numBck = num;
            tmpPos.y += 0.05f;
            tmpPos.z -= 0.3f;
        }
        else if (num < numBck)
        {
            numBck = num;
            tmpPos.y -= 0.05f;
            tmpPos.z += 0.3f;
        }
        else if (num == 0)
        {
            tmpPos.y = initPos.y;
            tmpPos.z = initPos.z;
        }

        if (tmpPos.y < initPos.y) tmpPos.y = initPos.y;
        if (tmpPos.z > initPos.z) tmpPos.z = initPos.z;

        mainCamera.localPosition = tmpPos;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 0, 30, 30), "←"))
        {
            //return
            arrayNo--;
            if (arrayNo < 0) arrayNo = effectObj.Length - 1;
            effectOn();

            haveProFlg = visibleBt();
        }

        if (GUI.Button(new Rect(50, 0, 200, 30), effectObj[arrayNo].name)) effectOn();

        if (GUI.Button(new Rect(250, 0, 30, 30), "→"))
        {
            //next
            arrayNo++;
            if (arrayNo >= effectObj.Length) arrayNo = 0;
            effectOn();

            haveProFlg = visibleBt();
        }

        if (haveProFlg)
            if (GUI.Button(new Rect(50, 30, 300, 70), "+Distorsion"))
            {
                if (nowEffectObj != null) Destroy(nowEffectObj);
                nowEffectObj = Instantiate(nonProFX);
            }


        if (GUI.Button(new Rect(300, 0, 200, 30), cameraState[cameraRotCon]))
        {
            if (cameraRotCon == 1)
                cameraRotCon = 0;
            else
                cameraRotCon = 1;
        }

        num = GUI.VerticalSlider(new Rect(30, 100, 20, 200), num, 0, 20);
    }

    private bool visibleBt()
    {
        foreach (var tmpObj in effectObProj)
            if (effectObj[arrayNo].name == tmpObj.name)
            {
                nonProFX = tmpObj;
                return true;
            }

        return false;
    }

    private void effectOn()
    {
        if (nowEffectObj != null) Destroy(nowEffectObj);
        nowEffectObj = Instantiate(effectObj[arrayNo]);
    }
}