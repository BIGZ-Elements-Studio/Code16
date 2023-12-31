using Spine.Unity;

using UnityEngine;
namespace CombatSystem.boss.stoneperson
{
    public class VineBullet : MonoBehaviour
    {
        [SerializeField]
        SkeletonAnimation effect;
        Spine.AnimationState spineAnimationState { get { return effect.AnimationState; } }
        [SerializeField]
        GameObject bullet;
        [SerializeField]
        BoxCollider Box;
        public float time;
        private void Start()
        {
            effect.timeScale = 2 / time;
            Invoke("active", time - 0.1f);
            Invoke("activeBox", time);
        }
        public void activeBox()
        {
            Box.enabled = true;
        }
        public void active()
        {
            bullet.SetActive(true);
        }

    }
}