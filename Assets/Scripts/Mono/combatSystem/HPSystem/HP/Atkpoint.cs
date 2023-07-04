using Microsoft.Cci;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CombatSystem
{
    public class Atkpoint :MonoBehaviour, DamageTarget
    {
       public HPContainer container;
        [SerializeField]
        CharaBuffContainer charaBuffContainer;
        public IndividualProperty playerIndividualProperty;
        public UnityEvent<string, bool> onHited;
        public UnityEvent<Vector3> AddedForce;
        public Transform center;
        public Transform popupPosition;
        [SerializeField] string hitName;
        public bool armor;
        public void addBuff(CharacterBuff buff)
        {
            charaBuffContainer.addBuff(buff);
        }
        public void addBuff(TeamBuff buff)
        {
            container.addBuff(buff);
        }
        public void addforceOfDirection(Vector3 direction)
        {
            AddedForce.Invoke(direction);
        }

        public bool Damage(DamageObject damage, CombatColor damageColor)
        {
            onHited.Invoke(hitName, true);
                (int,bool) info = container.SelfDamage(damage);
              int i = info.Item1;
            bool b= info.Item2;
               if (container.getType() == TargetType.enemy)
               {
                   if (damage.Critic)
                   {
                       VisualEffectController.DoDamagePopUp(i, VisualEffectController.DamagePopUpType.criticDamage, popupPosition.position);
                   }
                   else
                   {
                       VisualEffectController.DoDamagePopUp(i, VisualEffectController.DamagePopUpType.damage, popupPosition.position);
                   }

               }
              else if (container.getType() == TargetType.player)
               {
                   if (damage.damage > 0)
                   {
                       VisualEffectController.DoDamagePopUp(i, VisualEffectController.DamagePopUpType.damagePlayer, popupPosition.position);
                   }
                   else if (damage.damage < 0)
                   {
                       VisualEffectController.DoDamagePopUp(i, VisualEffectController.DamagePopUpType.cure, popupPosition.position);
                   }
               }
            playerIndividualProperty.changePoise(damage.hardness);
             return b;
        }

        public void DamageByPercent(int percent, CombatColor damageColor)
        {
            onHited.Invoke(hitName, true);
            container.DamageByPercent(percent, damageColor);
        }

        public Vector3 getCenterPosition()
        {
            return center.position;
        }

        public TargetType getType()
        {
            return container.type;
        }

        public void setAremd(string s, bool result)
        {
            container.setAremd(s, result);
        }

        public void addforce(Vector3 originalWorldPosition, float magnitude)
        {
            AddedForce.Invoke((originalWorldPosition-center.position)*magnitude);
        }

        public CombatColor getColor()
        {
            return container.getColor();
        }
    }
}