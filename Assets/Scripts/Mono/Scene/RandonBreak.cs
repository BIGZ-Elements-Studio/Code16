using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Scene
{
    public class RandonBreak : MonoBehaviour
    {
        public Collider BreakTrigger;
        public List<Rigidbody> rigidbodies;
        public List<Vector3> position;
        public List<Vector3> scale;
        public List<quaternion> Rotation;
        public Vector3 RangeMax;
        public Vector3 RangeMin;
        public Vector3 RangeMaxT;
        public Vector3 RangeMinT;
        public void addRb()
        {
            rigidbodies = new List<Rigidbody>();
            position = new List<Vector3>();
            scale = new List<Vector3>();
            Rotation = new List<quaternion>();
            // Recursively go through all child GameObjects and add Rigidbody if there isn't any
            RecursiveAddRigidbody(transform);
        }

        private void RecursiveAddRigidbody(Transform parent)
        {
            Debug.Log("called");

            foreach (Transform child in parent)
            {
                Debug.Log(child.name);
                if (child.GetComponent<Rigidbody>() != null)
                {
                    rigidbodies.Add(child.GetComponent<Rigidbody>());
                    position.Add(child.position);
                    scale.Add(child.localScale);
                    Rotation.Add(child.rotation);

                }

                // Check the children of this child
                if (child.childCount > 0)
                {
                    RecursiveAddRigidbody(child);
                }
            }
        }

        public void BreakLeaf()
        {
            // Give a random force by the range to all rigidbodies
            foreach (Rigidbody rb in rigidbodies)
            {
                rb.isKinematic = false;
                Vector3 force = new Vector3(Random.Range(RangeMin.x, RangeMax.x),
                                            Random.Range(RangeMin.y, RangeMax.y),
                                            Random.Range(RangeMin.z, RangeMax.z));
                rb.AddForce(force, ForceMode.Impulse);
                rb.useGravity = true;
                Vector3 torque = new Vector3(Random.Range(RangeMaxT.x, RangeMaxT.x),
                                            Random.Range(RangeMaxT.y, RangeMaxT.y),
                                            Random.Range(RangeMaxT.z, RangeMaxT.z));
                rb.AddTorque(force);
            }
            StartCoroutine(stop());
        }

        public IEnumerator stop()
        {
            if (BreakTrigger!=null) {
                BreakTrigger.enabled = false;
            }
            yield return new WaitForSeconds(3);
            foreach (Rigidbody rb in rigidbodies)
            {
                rb.gameObject.SetActive(false);
            }
        }
        public void resume()
        {// Re-enable the BreakTrigger if it exists
            if (BreakTrigger != null)
            {
                BreakTrigger.enabled = true;
            }

            foreach (Rigidbody rb in rigidbodies)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                int index = rigidbodies.IndexOf(rb); // Find the corresponding index in the lists
                rb.rotation = Rotation[index];
                rb.position = position[index];
                rb.gameObject.SetActive(true);
            }
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(RandonBreak))]
    public class RandonBreakEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            RandonBreak randomBreakScript = (RandonBreak)target;
            if (GUILayout.Button("Add Rigidbody to Children"))
            {
                randomBreakScript.addRb();
            }
            if (GUILayout.Button("break"))
            {
                randomBreakScript.BreakLeaf();
            }
        }
    }

#endif
}