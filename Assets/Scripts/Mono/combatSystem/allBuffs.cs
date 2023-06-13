using BehaviorControlling;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace CombatSystem {
    public class sampleBuffDeduceVelocity : Buff
    {
        public static int i;
        public sampleBuffDeduceVelocity()
        {
            i += 1;
        }
        BuffContainer Controller;
        AttackAttributeController c;
        Coroutine Coroutine;
        int IconId;
        public void initiate(BuffContainer target)
        {
            Controller = target;
            c= target.gameObject.GetComponent<AttackAttributeController>();
            if (c!=null)
            {
                c.moveSpeedFactor -= 0.5f;
               Coroutine= Controller.StartCoroutine(Wait());
            }
            IconId= Controller.AddBuffIcon(BuffTable.icons[0]);
        }
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(5);
            c.moveSpeedFactor += 0.5f;
            Controller.removeBuff(this);
            Controller.RemoveBuffIcon(IconId);
        }
        public void overlayed()
        {

            if (Coroutine!=null)
            {
                Controller.StopCoroutine(Coroutine);
            }
            c.moveSpeedFactor += 0.5f;
            Controller.removeBuff(this);
            Controller.RemoveBuffIcon(IconId);
        }

        public void overlying(Buff overlayedBuff, BuffContainer target)
        {
            DamageObject dam= new DamageObject();
            dam.damage = 100;
            target.GetComponent<HPController>().Damage(dam);
            target.removeBuff(this);
            
        }

        public void Add(HPController hP)
        {
            if (!hP.armor)
            {
                hP.BuffContainer.addBuff(this);
            }
        }
    }
    public class BuffFactory
    {
        public static Buff CreateBuff(string buffType)
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