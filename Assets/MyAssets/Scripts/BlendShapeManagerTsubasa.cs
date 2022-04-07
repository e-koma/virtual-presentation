using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapeManagerTsubasa : MonoBehaviour
{
    private SkinnedMeshRenderer bShapeFace;
    private enum FaceBlendShapeParames {
        arihara_02_eye_close,
        arihara_03_open_mouth_a,
        arihara_04_open_mouth_o,
        arihara_05_amazing_eye,
        arihara_06_eye_close_right,
        arihara_07_eye_close_left
    };
    private float v_eyeCloseLeft; // BlendShapeの値
    private float v_eyeCloseRight; // BlendShapeの値
    private enum FacialState { eyeOpen, eyeClose };
    private int currentFacialState = (int)FacialState.eyeOpen;
    private int WinkSpeed = 20; // Winkの速度(小さいほど速い)
    private int facialCoolTime = 0; // 初期値固定
    private const int facialCoolTimeIncreaseNum = 200; // CoolTimeフレーム数は何もしない
    float delta_eyeCloseLeft;
    float delta_eyeCloseRight;
    Dictionary<string, float> winkFacialGoal = new Dictionary<string, float>()
    {
        { "g_eyeCloseLeft",  100.0f },
        { "g_eyeCloseRight", 100.0f },
    };

    void Start()
    {
        bShapeFace = this.GetComponent<SkinnedMeshRenderer>();
        delta_eyeCloseLeft = winkFacialGoal["g_eyeCloseLeft"] / WinkSpeed;
        delta_eyeCloseRight = winkFacialGoal["g_eyeCloseRight"] / WinkSpeed;
        getBlendShapes();
    }

    void LateUpdate()
    {
        if (facialCoolTime > 0)
        {
            facialCoolTime--;
        }
        else
        {
            wink();
        }
    }

    private void wink()
    {
        switch (currentFacialState)
        {
            case (int)FacialState.eyeOpen:
                StartCoroutine("CloseEye");
                break;
            case (int)FacialState.eyeClose:
                StopCoroutine("CloseEye");
                OpenEye();
                break;
        }
    }

    private IEnumerator CloseEye()
    {
        for (int i = 0; i <= WinkSpeed; i++)
        {
            v_eyeCloseLeft += delta_eyeCloseLeft;
            v_eyeCloseRight += delta_eyeCloseRight;
            setBlendShapes();

            if (v_eyeCloseLeft > winkFacialGoal["g_eyeCloseLeft"])
            {
                break;
            }

            yield return new WaitForSeconds(0.01f);
        }

        currentFacialState = (int)FacialState.eyeClose;
    }

    private void OpenEye()
    {
        v_eyeCloseLeft = 0.0f;
        v_eyeCloseRight = 0.0f;
        currentFacialState = (int)FacialState.eyeOpen;
        facialCoolTime += facialCoolTimeIncreaseNum;
        setBlendShapes();
    }

    private void setBlendShapes()
    {
        bShapeFace.SetBlendShapeWeight((int)FaceBlendShapeParames.arihara_07_eye_close_left, v_eyeCloseLeft);
        bShapeFace.SetBlendShapeWeight((int)FaceBlendShapeParames.arihara_06_eye_close_right, v_eyeCloseRight);
    }

    private void getBlendShapes()
    {
        v_eyeCloseLeft = bShapeFace.GetBlendShapeWeight((int)FaceBlendShapeParames.arihara_07_eye_close_left);
        v_eyeCloseRight = bShapeFace.GetBlendShapeWeight((int)FaceBlendShapeParames.arihara_06_eye_close_right);
    }
}
