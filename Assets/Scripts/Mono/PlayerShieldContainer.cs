using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

namespace CombatSystem.shieldSystem
{
    public class PlayerShieldContainer : MonoBehaviour
    {
        public shield currentShield;
        [SerializeField]
        HPContainer TargetContainner;
        [SerializeField]
        playerTeamController playerTeamController;
        Coroutine RemoveProcess;
        public void addShield(shield s)
        {
            if (RemoveProcess != null)
            {
                StopCoroutine(RemoveProcess);
            }
            currentShield?.shieldReplaced?.Invoke();
            currentShield = s;
            RemoveProcess = StartCoroutine(ShieldProcess(s.duration, s.shieldAmount));
            TargetContainner.ShieldAmount=s.shieldAmount;
        }

        private void Awake()
        {
            playerTeamController.CharacterChanged.AddListener(OnCharacterChange);
            TargetContainner.shieldBreak .AddListener(ShieldBreak);
        }

        private void OnCharacterChange(int index)
        {
            if (currentShield !=null&& !currentShield.forTeam)
            {
                ShieldBreak();
            }
        }
        private void ShieldBreak()
        {
            if (currentShield!=null) 
            currentShield.shieldBreak?.Invoke();
            currentShield = null;
        }
        private void ShieldOutTime()
        {
            currentShield.shieldBreak?.Invoke();
            currentShield = null;
        }
        IEnumerator ShieldProcess(float time, int amount)
        {
            TargetContainner.ShieldAmount = amount;
            yield return new WaitForSecondsRealtime(time);
            ShieldOutTime();
        }
    }
    public class shield
    {
        public UnityEvent shieldBreak;
        public UnityEvent shieldReplaced;
        public int shieldAmount;
        public bool forTeam;
        public float duration;
        public shield( int shieldAmount, bool forTeam, float duration)
        {
            this.shieldAmount = shieldAmount;
            this.forTeam = forTeam;
            this.duration = duration;
            shieldBreak=new UnityEvent();
            shieldReplaced=new UnityEvent();
        }
        public shield()
        {
            shieldBreak = new UnityEvent();
            shieldReplaced = new UnityEvent();
        }
    }
}