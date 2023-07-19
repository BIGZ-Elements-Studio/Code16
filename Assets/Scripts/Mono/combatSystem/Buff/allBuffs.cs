using BehaviorControlling;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace CombatSystem {
    public class sampleBuffDeduceVelocity : CharacterBuff
    {
        CharaBuffContainer Controller;
        IndividualProperty Property;
        Coroutine Coroutine;
        int IconId;
        int buffNum;
        public void initiate(CharaBuffContainer target)
        {
            Controller = target;
            Property = target.Property;
            if (Property != null)
            {
                Property.extraMoveFactor -= 0.5f;
               Coroutine= Controller.StartCoroutine(Wait());
            }
            buffNum = 1;
            BuffIconDisplay.DisplayInfo displayInfo = new BuffIconDisplay.DisplayInfo();
            displayInfo.icon = BuffTable.icons[0];
            displayInfo.totalTime = 5;
            displayInfo.nowTime = 0;
            displayInfo.buffNumber = buffNum;
            IconId = Controller.AddBuffIcon(displayInfo);
        }
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(5);
            Property.extraMoveFactor += 0.5f;
            Controller.removeBuff(this);
            Controller.RemoveBuffIcon(IconId);
        }
        public void overlayed()
        {

            if (Coroutine!=null)
            {
                Controller.StopCoroutine(Coroutine);
            }
            Controller.removeBuff(this);
            Controller.RemoveBuffIcon(IconId);
        }

        public void overlying(CharacterBuff overlayedBuff, CharaBuffContainer target)
        {
            Controller=target;
            Property = target.Property;
            buffNum += (overlayedBuff as sampleBuffDeduceVelocity).buffNum+1;
            Coroutine = target.StartCoroutine(Wait());
            BuffIconDisplay.DisplayInfo displayInfo = new BuffIconDisplay.DisplayInfo();
            displayInfo.icon = BuffTable.icons[0];
            displayInfo.totalTime = 5;
            displayInfo.nowTime = 0;
            displayInfo.buffNumber = buffNum;
            IconId = Controller.AddBuffIcon(displayInfo);

        }


        public void Add(Atkpoint hP)
        {
            if (!hP.armor)
            {
                hP.addBuff(this);
            }
        }
    }

   
    public class BuffFactory
    {
        public static CharacterBuff CreateBuff(string buffType)
        {
            
            switch (buffType)
            {
                case "Buff1":
                    return new sampleBuffDeduceVelocity();
                default:
                    Debug.LogError("Invalid buff type!");
                    return null;
            }
        }
    }
}