using codeTesting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace oct.generatedBehavior
{

    public class sampleCharacterIEnumeratorclass : CharacterControlCoroutine
    {

        [SerializeField]
        float a;
        [SerializeField]
        GameObject d;
        [SerializeField]
        int j;
        [SerializeField]
        bool v;
        [SerializeField]
        PlayerController PlayerControllerr;

        [SpineAnimation]
      public  string runa;
        [SpineAnimation]
        public string idleb;
        [SpineAnimation]
        public string atkc;


        public IEnumerator jump()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);

            }

        }

        public IEnumerator idle()
        {
            PlayerControllerr.speed = 0;
            yield return null;
            PlayerControllerr.SetAnimation(idleb); 
        }

        public IEnumerator run()
        {
            PlayerControllerr.speed = 8;
            PlayerControllerr.SetAnimation(runa);
            lockState(true);
                yield return new WaitForSeconds(0.5f);
            lockState(false);
        }

        public IEnumerator hit()
        {
            
            PlayerControllerr.speed = 0;
            PlayerControllerr.SetAnimation(atkc);
            lockState(true);
                yield return new WaitForSeconds(1f);
                lockState(false);


        }


    }
}