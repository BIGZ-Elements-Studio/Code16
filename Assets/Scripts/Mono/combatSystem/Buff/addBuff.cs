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
        public CharacterBuff buff;
        public void Add(DamageTarget hP)
        {
                hP.addBuff(BuffFactory.CreateBuff(buffName));
        }
    }
}