using CombatSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;

namespace CombatSystem.team
{
    public class playerTeamController : MonoBehaviour
    {
        public List<GameObject> prefab = new List<GameObject>();
        public List<PlayerInTeam> playerInTeams = new List<PlayerInTeam>();
        public PlayerInTeamTwoD Chara2d;
        public UnityEvent<int> CharacterChanged;
        [SerializeField]
        private int CurrentCharacterIndex;
        public bool allowChange = true;
        public float TwoDZ;
        public PlayerInTeam CurrentCharacter { get { if (GameModeController.Is2d) { return null; } return playerInTeams[CurrentCharacterIndex]; } }
        public Transform CurrentCharacterActualPosition { get { if (GameModeController.Is2d) { return Chara2d.ActualTransform; } return playerInTeams[CurrentCharacterIndex].ActualTransform; } }
        public Transform CharacterActualPosition2D { get { return Chara2d.ActualTransform; } }
        public UnityEvent CurrentCharacterStateChange;
        public sampleCharacterCoroutineTwoD TwoDScript;
        public List<characterState> characterStates { get { return playerInTeams[CurrentCharacterIndex].characterStates; } }
        public UnityEvent<List<characterState>> OnCharacterStateChange;
        #region Ui
        public UnityEvent<int, int> onSpChangeWithMaxSp;
        public UnityEvent<int, int> onColorChangeWithMaxColor;
        public UnityEvent<CombatColor> onChangeColor;
        public UnityEvent<int, int> onHPChangeWithMaxHP;
       public ParticleSystem changePeopleParticle;
        #endregion
        public bool swtichCharacter(int index)
        {
            if (!allowChange)
            {
                return false;
            }
            if (CurrentCharacterIndex== index)
            {
                return false;
            }
            changePeopleParticle.gameObject.transform.position = playerInTeams[CurrentCharacterIndex].position;
            changePeopleParticle.Play();    
            playerInTeams[CurrentCharacterIndex].ActiveCharacter(false, playerInTeams[CurrentCharacterIndex].position, playerInTeams[CurrentCharacterIndex].faceright);
            playerInTeams[CurrentCharacterIndex].properties.onSpChangeWithMaxSp.RemoveListener(invokeChangeSp);
            playerInTeams[CurrentCharacterIndex].properties.onColorChangeWithMaxColor.RemoveListener(invokeChangeColor);
            playerInTeams[CurrentCharacterIndex].controller.statechange.RemoveListener(ChangeStateEvent);

            playerInTeams[index].ActiveCharacter(true, playerInTeams[CurrentCharacterIndex].position, playerInTeams[CurrentCharacterIndex].faceright);
            playerInTeams[index].properties.onSpChangeWithMaxSp.AddListener(invokeChangeSp);
            playerInTeams[index].properties.onColorChangeWithMaxColor.AddListener(invokeChangeColor);
            playerInTeams[CurrentCharacterIndex].controller.statechange.AddListener(ChangeStateEvent);

            onChangeColor.Invoke(playerInTeams[index].properties.color);
            onSpChangeWithMaxSp?.Invoke(playerInTeams[index].properties.MaxSp, playerInTeams[index].properties.currentsp);
            onColorChangeWithMaxColor.Invoke(10, playerInTeams[index].properties.colorBar);
            CurrentCharacterIndex = index;
            CharacterChanged?.Invoke(index);
            return true;

        }
        public void ChangeStateEvent(List<characterState> states)
        {
            OnCharacterStateChange?.Invoke(states);
        }
        void invokeChangeSp(int maxSp, int current)
        {
            onSpChangeWithMaxSp?.Invoke(maxSp, current);
        }

        void invokeChangeColor(int maxSp, int current)
        {
            onColorChangeWithMaxColor?.Invoke(maxSp, current);
        }

        private void Start()
        {
            foreach (PlayerInTeam team in playerInTeams)
            {
                team.ActiveCharacter(false, team.position, true);
            }
            swtichCharacter(0);
            GameModeController.ModeChangediFTo2D += changeMode;
            input = new PlayerInput();
            input.Enable();
            input.In3d.run.performed += ctx => { if (isActiveAndEnabled) { direction = (ctx.ReadValue<Vector2>()).normalized; } };
        }
        Vector2 direction;
        public PlayerInput input;
        void changeMode(bool To2d)
        {
            if (To2d)
            {
                playerInTeams[CurrentCharacterIndex].ActiveCharacter(false, playerInTeams[CurrentCharacterIndex].position, playerInTeams[CurrentCharacterIndex].faceright);
                Chara2d.ActiveCharacter(true, new Vector3(playerInTeams[CurrentCharacterIndex].position.x, playerInTeams[CurrentCharacterIndex].position.y,TwoDZ), playerInTeams[CurrentCharacterIndex].faceright);
            }
            else
            {
                Chara2d.ActiveCharacter(false, playerInTeams[CurrentCharacterIndex].position, Chara2d.faceright);
                playerInTeams[CurrentCharacterIndex].ActiveCharacter(true, Chara2d.position, Chara2d.faceright);
                playerInTeams[CurrentCharacterIndex].properties.onSpChangeWithMaxSp.AddListener(invokeChangeSp);
                playerInTeams[CurrentCharacterIndex].properties.onColorChangeWithMaxColor.AddListener(invokeChangeColor);
                playerInTeams[CurrentCharacterIndex].controller.statechange.AddListener(ChangeStateEvent);

                onChangeColor.Invoke(playerInTeams[CurrentCharacterIndex].properties.color);
                onSpChangeWithMaxSp?.Invoke(playerInTeams[CurrentCharacterIndex].properties.MaxSp, playerInTeams[CurrentCharacterIndex].properties.currentsp);
                onColorChangeWithMaxColor.Invoke(10, playerInTeams[CurrentCharacterIndex].properties.colorBar);
                CharacterChanged?.Invoke(CurrentCharacterIndex);
            }
        }
        void loadTeam()
        {

        }
        void distoryCurrent()
        {

        }

        public void lockTarget()
        {
            combatController.FindLockEnemy(CurrentCharacterActualPosition.position, direction);
        }
    }
}