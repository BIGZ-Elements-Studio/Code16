using CombatSystem.shieldSystem;
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
        public UnityEvent ReciveATk ;
        public UnityEvent<Vector3> AddedForce;
        public Transform center;
        public Transform popupPosition;
        [SerializeField]
        private PlayerShieldContainer ShieldController;
        [SerializeField] string hitName;
        public bool armor;

        public void setArm(bool i)
        {
            armor=i;
        }
        public void addBuff(CharacterBuff buff)
        {
            if (!armor) {
                charaBuffContainer.addBuff(buff);
            }
        }
        public void ForceaddBuff(CharacterBuff buff)
        {
            charaBuffContainer.addBuff(buff);
        }
        public void addBuff(TeamBuff buff)
        {
            container.addBuff(buff);
        }
        public void ForceaddBuff(TeamBuff buff)
        {
            container.ForceaddBuff(buff);
        }
        public void addShield(shield shield)
        {
            ShieldController.addShield(shield);
        }
        public void addforceOfDirection(Vector3 direction)
        {
            AddedForce.Invoke(direction);
        }

        public bool Damage(DamageObject damage, CombatColor damageColor)
        {

            ReciveATk?.Invoke();
            if (!armor) {
                playerIndividualProperty.changePoise(damage.hardness);
                int actualDamage = DamageUtility.calculateDamage(playerIndividualProperty.actualDef, damage.damage, 0);
                (int, bool) info = container.SelfDamage(actualDamage);
                int CalculatedDamage = info.Item1;
                bool Successful = info.Item2;
                if (Successful) {
                    onHited.Invoke(hitName, true);
                }

                VisualEffectController.DoDamagePopUp(CalculatedDamage, container.getType(), damage.Critic, popupPosition.position);
                   

                return Successful;
            }
            return false;   
        }

        public void DamageByPercent(int percent, CombatColor damageColor)
        {
            ReciveATk?.Invoke();
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