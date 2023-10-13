using CombatSystem;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;



namespace oct.world
{
    public class WorldBlock : MonoBehaviour
    {
        public GameObject Ground;
        public GameObject _wallPrefeb;
        float hight = 2.4f;
        float thick = 2f;
        List<GameObject> walls = new List<GameObject>();
        [SerializeField]
        List<Renderer> RenderersOfBlock = new List<Renderer>();
        [SerializeField]
        List<GameObject> DisableIn2d = new List<GameObject>();
        [SerializeField]
        List<GameObject> DisableIn3d = new List<GameObject>();
        public bool allow3d;
        private void Awake()
        {
            GameModeController.ModeChangediFTo2D += switchD;
          //  TPController.Tp += distoryWall;
          //  TPController.Tp += TPClearImage;
        }
        #region addRender

        private void OnDestroy()
        {
            GameModeController.ModeChangediFTo2D -= switchD;

        }
        public void autoAddChildRender()
        {
            RenderersOfBlock = new List<Renderer>();
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                if (!RenderersOfBlock.Contains(renderer))
                {
                    RenderersOfBlock.Add(renderer);
                }
            }
            addChildItem(transform);
        }
        void addChildItem(Transform t)
        {

            foreach (Transform child in t)
            {
                Renderer renderer = child.gameObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    if (!RenderersOfBlock.Contains(renderer))
                    {
                        RenderersOfBlock.Add(renderer);
                    }
                    Debug.Log(child.gameObject.name);
                }
                addChildItem(child);
            }
        }
        #endregion

        #region switchD
        public void remainShown()
        {
            showImg();
           // generateWall();
        }

        void switchD(bool to2d)
        {
            if (to2d)
            {
                showImg();
               // distoryWall();
                foreach (GameObject g in DisableIn2d)
                {
                    g?.SetActive(false);
                }
                foreach (GameObject g in DisableIn3d)
                {
                    g?.SetActive(true);
                }
            }
            else
            {
                foreach (GameObject g in DisableIn3d)
                {
                    g?.SetActive(false);

                }
                foreach (GameObject g in DisableIn2d)
                {
                    g?.SetActive(true);

                }
                if (combatController.PlayerActualposition2D.y < Ground.transform.position.y)
                {
                    
                    clearImg();
                }
            }
        }

      /*  void TPClearImage()
        {
            if (GameModeController.Is2d == false)
            {
                if (TeamController.currentCharaTransform.position.y < Ground.transform.position.y)
                {
                    clearImg();
                }
                else
                {
                    showImg();
                }
                distoryWall();
            }
        }*/

        void clearImg()
        {
            foreach (Renderer rd in RenderersOfBlock)
            {
                if (rd != null)
                {
                    rd.enabled = false;
                }
            }
        }

        void showImg()
        {
            foreach (Renderer rd in RenderersOfBlock)
            {
                if (rd != null)
                {
                    rd.enabled = true;
                }
            }
        }
        // Start is called before the first frame update
      /*  void generateWall()
        {
            GameObject left = Instantiate(wallPrefeb);
            left.transform.localScale = new Vector3(thick, hight, Ground.transform.lossyScale.z);
            left.transform.localPosition = new Vector3(Ground.transform.position.x - Ground.transform.localScale.x / 2 - thick / 2 - 1, Ground.transform.position.y + 1.14f, Ground.transform.position.z);
            left.transform.DOMoveX(Ground.transform.position.x - Ground.transform.localScale.x / 2 - thick / 2, 0.1f);
            walls.Add(left);
            GameObject right = Instantiate(wallPrefeb);
            right.transform.localScale = new Vector3(thick, hight, Ground.transform.lossyScale.z);
            right.transform.localPosition = new Vector3(Ground.transform.position.x + Ground.transform.localScale.x / 2 + thick / 2 + 1, Ground.transform.position.y + 1.14f, Ground.transform.position.z);
            right.transform.DOMoveX(Ground.transform.position.x + Ground.transform.localScale.x / 2 + thick / 2, 0.1f);
            walls.Add(right);
            GameObject front = Instantiate(wallPrefeb);
            front.transform.localScale = new Vector3(Ground.transform.localScale.x, hight, thick);
            front.transform.localPosition = new Vector3(Ground.transform.position.x, Ground.transform.position.y + 1.14f, Ground.transform.position.z - Ground.transform.localScale.z / 2 - thick / 2 - 1);
            front.transform.DOMoveZ(Ground.transform.position.z - Ground.transform.localScale.z / 2 - thick / 2, 0.1f);
            walls.Add(front);
            GameObject Back = Instantiate(wallPrefeb);
            Back.transform.localScale = new Vector3(Ground.transform.localScale.x, hight, thick);
            Back.transform.localPosition = new Vector3(Ground.transform.position.x, Ground.transform.position.y + 1.14f, Ground.transform.position.z + Ground.transform.localScale.z / 2 + thick / 2 + 1);
            Back.transform.DOMoveZ(Ground.transform.position.z + Ground.transform.localScale.z / 2 + thick / 2, 0.1f);
            walls.Add(Back);
            left.name = "1";
            right.name = "2";
            front.name = "3";
            Back.name = "4";
        }*/

        void distoryWall()
        {
            foreach (GameObject wall in walls)
            {
                Destroy(wall);
            }
        }
        #endregion
    }
    #if UNITY_EDITOR
    [CustomEditor(typeof(WorldBlock))]
    public class worldBlockCustomInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            WorldBlock a = (WorldBlock)target;
            if (GUILayout.Button("Add child render"))
            {
                a.autoAddChildRender();
            }
        }
    }
#endif

}
