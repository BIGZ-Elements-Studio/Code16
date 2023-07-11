using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CombatSystem {
    public class IndividualProperty : MonoBehaviour
    {
        public CombatColor color;
        public int currentsp;
        public int currentpoise;
        public AttackAttributeController controller;
        public HPContainer Container;
        public string PoiseName;
        public string spName;
        public UnityEvent<string,float> onPoiseChange;
        public UnityEvent<string, float> onSpChange;
        public UnityEvent<int, int> onSpChangeWithMaxSp;
        public UnityEvent<int, int> onColorChangeWithMaxColor;
        public int colorBar;
        public int MaxSp
        {
            get { return controller.MaxSp; }
        }
        public int actualAtk
        {
            get { return controller.Atk; }
        }

        public float moveSpeedFactor
        {
            get { return controller.moveSpeedFactor+ extraMoveFactor; }
        }

        public float extraMoveFactor;
        public int actualCritcRate
        {
            get { return controller.CritcAtkRate; }
        }

        public int actualCritcDamage
        {
            get { return controller.CritcAtkDamage; }
        }
        public int actualDef
        {
            get { return Container.def; }
        }

        public float AtkSpeed;
        private void Start()
        {
            currentsp = 0;
            currentpoise = Container.basePoise;
            onPoiseChange?.Invoke(PoiseName, currentpoise);
            onSpChange.Invoke(spName, currentpoise);
            onSpChangeWithMaxSp.Invoke(controller.MaxSp, currentsp);
            onColorChangeWithMaxColor.Invoke(colorBar,10);
        }
        public void changePoise(int change)
        {
            currentpoise -= change;
            if (currentpoise < 0)
            {
                currentpoise = 0;

            }
            onPoiseChange?.Invoke(PoiseName, currentpoise);
        }
        public void GainSp(int i)
        {
            currentsp += i;
            currentsp = Mathf.Clamp(currentsp, 0, MaxSp);
            onSpChange?.Invoke(spName, currentsp);
            onSpChangeWithMaxSp.Invoke(MaxSp, currentsp);
        }
        void arm(bool b)
        {

        }
        public void gainColor(int amount )
        {
            colorBar+=amount;
            onColorChangeWithMaxColor.Invoke(10, colorBar);
        }
    }
}