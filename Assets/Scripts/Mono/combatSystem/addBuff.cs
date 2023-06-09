using CombatSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CombatSystem {
    public class addBuff : MonoBehaviour
    {
        public string buffName;
        public Type Type;
        public Buff buff;
        public void Add(HPController hP)
        {
            if (!hP.armor) {
                hP.BuffContainer.addBuff(BuffFactory.CreateBuff(buffName));
            }
        }
    }
}