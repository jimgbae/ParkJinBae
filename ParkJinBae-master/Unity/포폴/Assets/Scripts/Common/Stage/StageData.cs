using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataInfo;

namespace StageData
{
    [System.Serializable]
    public class Stage1Data
    {
        public int maxEnemy = 10;
        public Transform[] points;
    }

    [System.Serializable]
    public class Stage2Data
    {
        public int maxEnemy = 20;
        public Transform[] points;
    }
}
