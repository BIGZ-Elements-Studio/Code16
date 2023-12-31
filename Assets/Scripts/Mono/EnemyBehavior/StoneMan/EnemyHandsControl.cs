using BehaviorControlling;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace CombatSystem.boss.stoneperson
{
    public class EnemyHandsControl : MonoBehaviour
    {
       public stoneHandController controller1;
        public stoneHandController controller2;
        [SerializeField]
        public SkeletonAnimation skeletonAnimation;
        public EnemyShieldContainner ShieldContainner;
        [SpineAnimation]
        public string changeAnimation;
        [SpineAnimation]
        public string idle1;
        [SpineAnimation]
        public string idle2;
      //  [SpineAnimation]
      //  public string strongHit;
        public GameObject vineBullet;
        public EnemyHandEffect enemyHandEffect1;
        public EnemyHandEffect enemyHandEffect2;
        public Color firstColor;
        public List<Transform> S2_ex_Position;
        public List<GameObject> S2_ex_Vine;
        public EnemyAtkPoint Hand1point;
        public UnityEvent Die;
        public void SetAnimation(string s)
        {
            spineAnimationState.SetAnimation(0, s, true);
        }
        public void SetAnimationNoRepeate(string s)
        {
            spineAnimationState.SetAnimation(0, s, false);
        }
        void state1()
        {
            SetAnimation(idle1);
            
        }

        public void Reset()
        {
            two = false;
            state1();

        }
        private void Awake()
        {
            state1();
            combatController.OnCharaDie.AddListener(Stop);
        }
        bool two;
        public void Stop()
        {
            Debug.Log("!!!!!!!!!");
            StopAllCoroutines();
            controller1.StopAllCoroutines();
            controller2.StopAllCoroutines();
            controller1.Dofade();
            controller2.Dofade();
            StartCoroutine(changeSceneLightColor(1f, Color.white));
        }
        public void dead()
        {
            Stop();
            Die?.Invoke ();
        }

        public void changeBlood(float hp)
        {
            if (hp < 15000/2 && !two)
            {

                changeState();
            }
            else if (hp <= 0)
            {

                dead();
            }

            }
            void changeState()
        {
            two=true;
            SetAnimationNoRepeate(changeAnimation);
            spineAnimationState.AddAnimation(0,idle2,true,0);
        }
        Spine.AnimationState spineAnimationState { get { return skeletonAnimation.AnimationState; } }
        private void Start()
        {
           // addshield();
          //  ShieldContainner.ShieldBreak.AddListener(delegate { StartCoroutine(OnShieldBreak()); });

        }
        public IEnumerator Phaseoverall()
        {
            while (true)
            {
                yield return S1_1();
                if (two)
                {
                    break;
                }
                yield return S1_2(); 
                if (two)
                {
                    break;
                }
                yield return S1_3();
                if (two)
                {
                    break;
                }
                yield return S1_ex();
                if (two)
                {
                    break;
                }
            }

            while (true)
            {
                yield return S2_1();
                yield return S2_2();
                yield return S2_3();
                yield return S2_ex();
            }
        }
        #region phase1

        public IEnumerator PhaseOneCycle()
        {
            while (true)
            {
                yield return S1_1();
                yield return S1_2();
                yield return S1_3();
                yield return S1_ex();
            }
        }
        private IEnumerator S1_1()
        {
            float time1 = 5;
            StartCoroutine( controller1.crashGround_1());
            StartCoroutine(controller2.HandOnGround(controller2.S1_1Position.transform.position));
            yield return new WaitForSeconds(time1);


            controller2.Dofade();
       //     enemyHandEffect2.fadeIn();
      //      enemyHandEffect1.fadeIn();
      //      enemyHandEffect2.Move(controller2.gameObject.transform.position, controller2.crashGround_1Position,1.7f);
       //     enemyHandEffect1.Move(controller1.gameObject.transform.position, controller1.S1_1Position.transform.position, 1.7f);
            float time=0;
            while (time < 2)
            {
          //      enemyHandEffect1.endTransform = controller1.S1_1Position.transform.position;
                time += Time.fixedDeltaTime;
            }
            enemyHandEffect2.fadeOut();
         //   enemyHandEffect1.fadeOut();



            StartCoroutine(controller2.crashGround_1());
            StartCoroutine(controller1.HandOnGround(controller1.S1_1Position.transform.position));
            yield return new WaitForSeconds(time1);

            controller1.Dofade();
        //    enemyHandEffect2.fadeIn();
        //    enemyHandEffect1.fadeIn();
       //     enemyHandEffect2.Move(controller2.gameObject.transform.position, controller2.S1_1Position.transform.position, 1.7f);
          //  enemyHandEffect1.Move(controller1.gameObject.transform.position, controller1.crashGround_1Position, 1.7f);
            time = 0;
            while (time < 2)
            {
                enemyHandEffect2.endTransform = controller2.S1_1Position.transform.position;
                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
          //  yield return new WaitForSeconds(2f);
         //   enemyHandEffect2.fadeOut();
          //  enemyHandEffect1.fadeOut();


            StartCoroutine(controller1.crashGround_1());
            StartCoroutine(controller2.HandOnGround(controller2.S1_1Position.transform.position));
            yield return new WaitForSeconds(time1);

           
            controller1.Dofade();
            controller2.Dofade(); 
         //   enemyHandEffect2.fadeOut();
         //   enemyHandEffect1.fadeOut();
            
        }
        float HitBackCount = 0;
        public void hit()
        {
           // spineAnimationState.SetAnimation(0, strongHit, false);
           // spineAnimationState.AddAnimation(0, idle1, true, 1);
        }
        void add()
        {
            HitBackCount += 1;
        }
        private IEnumerator S1_2()
        {
            List<float> a = new List<float>();
           a.Add( Random.value);
            a.Add(Random.value);
            
            yield return new WaitForSeconds(2f);
            StartCoroutine(changeSceneLightMagnitude(2f, 0.7f));
            for (int i=0;i<2; i++) {
                HitBackCount = 0;
                stoneThrowStone.HitBack.AddListener(add);
                if (combatController.Player.transform.position.x < -25)
                {
                    controller1.StartCoroutine(controller1.ThrowStone());

                }
                else if (combatController.Player.transform.position.x > 25)
                {
                    controller2.StartCoroutine(controller2.ThrowStone());
                }
                else
                {
                    if (a[i] < 0.5f)
                    {
                        controller2.StartCoroutine(controller2.ThrowStone());

                    }
                    else
                    {
                        controller1.StartCoroutine(controller1.ThrowStone());
                    }
                }
                yield return new WaitForSeconds(5f);
               
            }
            StartCoroutine(changeSceneLightMagnitude(0.3f, 1f));
            stoneThrowStone.HitBack.RemoveListener(add);
            controller1.Dofade();
            controller2.Dofade();
            yield return new WaitForSeconds(0.3f);
            yield return new WaitForSeconds(0.3f);
            if (HitBackCount>=2) {
                
                StartCoroutine(controller2.HandOnGround(new Vector3(combatController.Player.transform.position.x - 10, 0, (combatController.Player.transform.position.z))));
                StartCoroutine(controller1.HandOnGround(new Vector3(combatController.Player.transform.position.x + 10, 0, (combatController.Player.transform.position.z))));
                yield return new WaitForSeconds(8f);
            }
           
            controller1.Dofade();
            controller2.Dofade();
            yield return new WaitForSeconds(4f);
            HitBackCount = 0;
        }

        private IEnumerator S1_3()
        {
            StartCoroutine(changeSceneLightColor(5f,firstColor));
            controller1.StartCoroutine(controller1.SpawnVine());
            controller2.StartCoroutine(controller2.SpawnVine());
            yield return new WaitForSeconds(1f);
            float time = 0;
            while (time<8)
            {
                GameObject g = Instantiate(vineBullet);
                g.transform.position =new Vector3( combatController.Player.transform.position.x,0, combatController.Player.transform.position.z);
                g.SetActive(true);
                time += 1.5f;
                yield return new WaitForSeconds(1.5f);
            }
            StartCoroutine(changeSceneLightColor(4f, Color.white));
            controller1.Dofade();
            controller2.Dofade();
            yield return new WaitForSeconds(2f);
        }

        public GameObject CrashLeft;
        public GameObject CrashRight;
        private IEnumerator S1_ex()
        {
            StartCoroutine(changeSceneLightMagnitude(4f,0.2f));
            yield return new WaitForSeconds(2f);
            StartCoroutine(controller1.crashGround_2(new Vector3(combatController.Player.transform.position.x-10,0, (combatController.Player.transform.position.z+3))));
            StartCoroutine(controller2.crashGround_2(new Vector3(combatController.Player.transform.position.x+10, 0, (combatController.Player.transform.position.z+3))));
           
            yield return new WaitForSeconds(1f);
            StartCoroutine(changeSceneLightMagnitude(7f, 1f));
            yield return new WaitForSeconds(5f);
        }
        IEnumerator changeSceneLightMagnitude(float duration,float target)
        {
            float time = 0;
            while (time < duration)
            {
                time += Time.deltaTime;
                yield return null;
                SceneEffectController.SceneMainLight.intensity = Mathf.Lerp(SceneEffectController.SceneMainLight.intensity, target,time/duration);
            }
        }
       public IEnumerator changeSceneLightColor(float duration, Color target)
        {
            float time = 0;
            while (time < duration)
            {
                time += Time.deltaTime;
                yield return null;
                SceneEffectController.SceneMainLight.color = Color.Lerp(SceneEffectController.SceneMainLight.color, target, time / duration);
            }
        }
        #endregion

        #region phase2
        public IEnumerator PhaseTwoCycle()
        {
            while (true)
            {
                yield return S2_1();
                yield return S2_2();
                yield return S2_3();
                yield return S2_ex();
            }
        }
        IEnumerator S2_1()
        {

            yield return new WaitForSeconds(2f);
            StartCoroutine(changeSceneLightMagnitude(2f, 0.7f));
            for (int i = 0; i < 5; i++)
            {
                HitBackCount = 0;
                stoneThrowStone.HitBack.AddListener(add);
                if (combatController.Player.transform.position.x < -25)
                {
                    controller1.StartCoroutine(controller1.ThrowStone());

                }
                else if (combatController.Player.transform.position.x > 25)
                {
                    controller2.StartCoroutine(controller2.ThrowStone());
                }
                else
                {
                    if (Random.value < 0.5f)
                    {
                        controller2.StartCoroutine(controller2.ThrowStone());

                    }
                    else
                    {
                        controller1.StartCoroutine(controller1.ThrowStone());
                    }
                }
                yield return new WaitForSeconds(5f);

            }
            StartCoroutine(changeSceneLightMagnitude(0.3f, 1f));
            stoneThrowStone.HitBack.RemoveListener(add);
            controller1.Dofade();
            controller2.Dofade();
            yield return new WaitForSeconds(0.3f);
            Debug.Log(HitBackCount);
            if (HitBackCount >= 5)
            {

                StartCoroutine(controller2.HandOnGround(new Vector3(combatController.Player.transform.position.x - 10, 0, (combatController.Player.transform.position.z))));
                StartCoroutine(controller1.HandOnGround(new Vector3(combatController.Player.transform.position.x + 10, 0, (combatController.Player.transform.position.z))));
                yield return new WaitForSeconds(8f);
            }

            controller1.Dofade();
            controller2.Dofade();
            yield return new WaitForSeconds(4f);
            HitBackCount = 0;
        }
        IEnumerator S2_2()
        {
            controller1.ThrowPlayerSuccessful.AddListener(setS2Successful);
            controller2.ThrowPlayerSuccessful.AddListener(setS2Successful);
            for (int i = 0; i <= 1; i++)
            {
                StartCoroutine(controller1.ThrowPlayer_1());
                yield return new WaitForSeconds(3);
                if (S2Successful)
                {
                    yield return new WaitForSeconds(4);
                }
                S2Successful = false;
                StartCoroutine(controller2.ThrowPlayer_1());
                yield return new WaitForSeconds(3);
                if (S2Successful)
                {
                    yield return new WaitForSeconds(4);
                }
                S2Successful = false;
            }

        }
        IEnumerator S2_3()
        {
            // controller1.ThrowPlayerSuccessful.AddListener(setS2Successful);
            //controller2.ThrowPlayerSuccessful.AddListener(setS2Successful);
            //  for()
            StartCoroutine(changeSceneLightColor(5f, firstColor));
            StartCoroutine(S2_3Vine());
            StartCoroutine(S2_3Hand());
            S2Successful = false;
            yield return new WaitForSeconds(10f);

        }

        IEnumerator S2_3Vine()
        {
            float time = 0;
            while (time < 8)
            {
                GameObject g = Instantiate(vineBullet);
                g.transform.position = new Vector3(combatController.Player.transform.position.x, 0, combatController.Player.transform.position.z);
                g.SetActive(true);
                time += 1.5f;
                yield return new WaitForSeconds(1.5f);
            }
        }
        IEnumerator S2_3Hand()
        {
           // for (int i = 0; i <= 2; i++)
            {
                StartCoroutine(controller1.ThrowPlayer_1());
                yield return new WaitForSeconds(7);
                if (S2Successful)
                {
                    yield return new WaitForSeconds(4);
                }
                S2Successful = false;
                StartCoroutine(controller2.ThrowPlayer_1());
                yield return new WaitForSeconds(3);
                if (S2Successful)
                {
                    yield return new WaitForSeconds(4);
                }
                S2Successful = false;
            }
        }
        bool S2Successful;
        void setS2Successful(bool i)
        {
            Debug.Log(i);
            S2Successful = i;
        }

        IEnumerator S2_ex()
        {
            s2exHit = false;
            controller1.reciveAtk.AddListener(S2exHit);
            foreach(GameObject g in S2_ex_Vine)
            {yield return new WaitForSeconds(0.2f);
                g.SetActive(true);
                
            }
            controller1.StartCoroutine(controller1.HandOnGround(S2_ex_Position[0].position));
            
            float time = 0;
            float totalTime = 15;
            float refreshRate=0.1f;
            s2exHit = false;
            bool fail = false;
            while (time < totalTime)
            {
                time+=refreshRate;
                yield return new WaitForSeconds(refreshRate);
                if (s2exHit)
                {
                    DamageObject damage=new DamageObject();
                    damage.damage = 2000;

                    Hand1point.Damage(damage,CombatColor.empty);
                    Debug.Log("end"); break;
                }
                if (combatController.CharacterStates.Contains(characterState.fly))
                {
                    fail = true;
                    Debug.Log("end2");break;
                }Debug.Log("on"+ time);
            }
            foreach (GameObject g in S2_ex_Vine)
            {
               
                g.SetActive(false); yield return new WaitForSeconds(0.2f);

            }
            if (!fail) {
                yield return new WaitForSeconds(5f);
            }
            controller1.Dofade();
            controller1.reciveAtk.RemoveListener(S2exHit);
            StartCoroutine(changeSceneLightColor(4f, Color.white));
        }
        bool s2exHit;
        void S2exHit()
        {
            s2exHit = true;
        }
            #endregion
        #region test
            public void skill1()
        {
           StartCoroutine(Phaseoverall());
        }
        public void skill2()
        {
            StartCoroutine(PhaseTwoCycle());
        }
        public void skill3()
        {
            StartCoroutine(S2_ex());
        }
        public void skill4()
        {
             StartCoroutine(S1_ex());
        }
        public IEnumerator trySkill2()
        {
            yield return new WaitForSeconds(2f);
            controller1.Dofade();
            controller2.Dofade();
            while (true)
            {
                controller2.StartCoroutine(controller2.ThrowStone());
                yield return new WaitForSeconds(7f);
                controller1.StartCoroutine(controller1.ThrowStone());
                yield return new WaitForSeconds(7f);
            }
        }
        public IEnumerator trySkill1()
        {
            yield return new WaitForSeconds(2f);
            controller1.Dofade();
            controller2.Dofade();
            while (true)
            {
                StartCoroutine(S1_1());
                yield return new WaitForSeconds(15f);

            }
        }

        public IEnumerator trySkill3()
        {
            yield return new WaitForSeconds(2f);
            controller1.Dofade();
            controller2.Dofade();
            while (true)
            {
                controller2.StartCoroutine(controller2.ThrowPlayer_1());
                yield return new WaitForSeconds(7f);
                controller1.StartCoroutine(controller1.ThrowPlayer_1());
                yield return new WaitForSeconds(7f);
            }

        }
        public IEnumerator trySkill4()
        {
            yield return new WaitForSeconds(2f);
            controller1.StartCoroutine(controller1.SpawnVine());
                controller2.StartCoroutine(controller2.SpawnVine());
            yield return new WaitForSeconds(1f);
            while (true)
            {
               GameObject g= Instantiate(vineBullet);
                g.transform.position = combatController.Player.transform.position;
                g.SetActive(true);
                
                yield return new WaitForSeconds(3f);
            }

        }

#endregion
        public IEnumerator OnShieldBreak()
        {
            yield return new WaitForSeconds(3f);
            addshield();
        }

        void addshield()
        {
            ShieldContainner.setShield(CombatColor.blue,100);
        }
        public Transform origion;
        public float range;
        float MaxDistance=40;
        public void shootColotballToPlayer(CombatColor c)
        {
            float randomAngle = Random.Range(0f, Mathf.PI * 2f);
            Vector3 offset = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle))* range;
            GameObject g = Instantiate((combatColorController.GetColorBall(c)));
            g.transform.position = origion.position;
            Vector3 distanceBetween = (combatController.PlayerActualPosition - origion.position) + offset;
            Vector3 targetAngle = distanceBetween.normalized;
            float distance = distanceBetween.magnitude;
            float distancePercentage = distance / 60;
            float actualSpeed = MaxDistance * distancePercentage;
            float hight = 10;
            g.GetComponent<Rigidbody>().velocity = new Vector3(targetAngle.x * actualSpeed, hight, targetAngle.z * actualSpeed);
        }
    }
}