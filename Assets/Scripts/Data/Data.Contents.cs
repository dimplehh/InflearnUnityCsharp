using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
#region Stat
    [Serializable]//메모리에서 들고있는 것을 파일로 변환 가능(공식처럼 이해)
    public class Stat
    {
        public int level;
        public int maxhp;
        public int attack;
        public int totalExp;
    }
    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
            foreach (Stat stat in stats)
                dict.Add(stat.level, stat);
            return dict;
        }
    }

    #endregion
}