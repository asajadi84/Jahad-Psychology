using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPrefabStatus
{
    public bool oralStatus;
    public BallSituation questionStatus;
    public BallSituation answerStatus;
    public float lifeSpanStatus;
    public float delayStatus;

    public BallPrefabStatus(bool oralStatus, BallSituation questionStatus, BallSituation answerStatus, float lifeSpanStatus, float delayStatus)
    {
        this.oralStatus = oralStatus;
        this.questionStatus = questionStatus;
        this.answerStatus = answerStatus;
        this.lifeSpanStatus = lifeSpanStatus;
        this.delayStatus = delayStatus;
    }
}
