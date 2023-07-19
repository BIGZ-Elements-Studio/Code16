using CombatSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace oct.cameraControl {
    public class CameraMovementThreeDDefault : CameraMovementController
    {
        public Vector3 offset;
        public Vector3 playerPosition { get { return combatController.PlayerActualPosition; } }
        public float playerWeight;
        public List<(float, Transform)> TotalTarget; // float means the percentage


        public override Vector3 GetDesirePosition()
        {
            Vector3 desirePosition = playerPosition * playerWeight;

            float totalWeight = playerWeight;

            // Mix playerPosition and each position of TotalTarget by their weight
            foreach ((float weight, Transform target) in TotalTarget)
            {
                desirePosition += target.position * weight;
                totalWeight += weight;
            }

            // Take the average position based on total weights
            if (totalWeight > 0f)
                desirePosition /= totalWeight;

            // Add the offset to the final position
            desirePosition += offset;

            return desirePosition;
        }

        public int AddTarget(Transform target)
        {
            TotalTarget.Add((0, target));
            return TotalTarget.Count-1;
        }

        public void RemoveTarget(int index, float smoothTime)
        {
            StartCoroutine(GraduallyChangeWeightAndRemove(index, smoothTime));
        }
        public void ChangeWeight(int index, float targetPercentage, float smoothTime)
        {
            StartCoroutine(GraduallyChangeWeight(index, targetPercentage, smoothTime));
        }
        private void Awake()
        {
            TotalTarget = new List<(float, Transform)>();
        }

        private IEnumerator GraduallyChangeWeightAndRemove(int index, float smoothTime)
        {
            float initialWeight = TotalTarget[index].Item1;
            float timer = 0f;

            while (timer < smoothTime)
            {
                float currentPercentage = Mathf.Lerp(initialWeight, 0, timer / smoothTime);
                ChangeWeight(index, currentPercentage);
                timer += Time.deltaTime;
                yield return null;
            }

            // Ensure the weight is exactly the target percentage when the loop finishes
            ChangeWeight(index, 0);
                RemoveTargetInternal(index);
        }
        private IEnumerator GraduallyChangeWeight(int index, float targetPercentage, float smoothTime)
        {
            float initialWeight = TotalTarget[index].Item1;
            float timer = 0f;

            while (timer < smoothTime)
            {
                float currentPercentage = Mathf.Lerp(initialWeight, targetPercentage, timer / smoothTime);
                ChangeWeight(index, currentPercentage);
                timer += Time.deltaTime;
                yield return null;
            }

            // Ensure the weight is exactly the target percentage when the loop finishes
            ChangeWeight(index, targetPercentage);

            // If the weight is zero, remove the target from the list
            if (targetPercentage == 0f)
                RemoveTargetInternal(index);
        }


        private void RemoveTargetInternal(int index)
        {
            TotalTarget.RemoveAt(index);
        }

        private void ChangeWeight(int index, float targetPercentage)
        {
            float currentAmount;
            if (index==-1)
            {
                currentAmount = playerWeight;
            }
            else
            {
                currentAmount = TotalTarget[index].Item1;
            }

            float remainTargetPercent = 100 - currentAmount;
            float remainCurrentPercent=playerWeight;
            for (int i = 0; i < TotalTarget.Count; i++)
            {
                 remainCurrentPercent+= TotalTarget[i].Item1;
            }
            remainCurrentPercent -= currentAmount;
            float ChangeRatio = remainTargetPercent / remainCurrentPercent;
            //Debug.Log(targetPercentage + " "+currentAmount + " "+remainTargetPercent+" "+ remainCurrentPercent+" "+ChangeRatio);
            if (index!=-1)
            {
                playerWeight *= ChangeRatio;
            }
            for (int i = 0; i < TotalTarget.Count; i++)
            {
                if (i == index)
                {
                    // Update the target weight
                    TotalTarget[i] = (targetPercentage, TotalTarget[i].Item2);
                }
                else
                {
                    // Update the other target weights
                    float adjustedPercentage = TotalTarget[i].Item1 * ChangeRatio;
                    TotalTarget[i] = (adjustedPercentage, TotalTarget[i].Item2);
                }
            }
        }

    }
    public class FloatPercentageUpdater
    {
        public static List<float> Add(List<float> percentages, int index, float target)
        {
            if (percentages == null || index < 0 || index >= percentages.Count)
            {
                throw new ArgumentException("Invalid input parameters.");
            }

            // Calculate the current sum of percentages
            float currentSum = 0;
            foreach (float percent in percentages)
            {
                currentSum += percent;
            }

            // Check if the input percentages add up to 100
            if (Math.Abs(currentSum - 100.0f) > float.Epsilon)
            {
                throw new ArgumentException("The input percentages must add up to 100.");
            }

            // Calculate the current value at the specified index
            float currentValue = percentages[index];

            // Calculate the difference required to reach the target value
            float difference = target - currentValue;

            // Adjust the target value if it exceeds the range [0, 100]
            target = Math.Max(0.0f, Math.Min(100.0f, target));

            // If the difference exceeds the allowed range, adjust it
            if (Math.Abs(difference) > float.Epsilon)
            {
                float newValue = currentValue + difference;

                // If the new value goes below 0 or above 100, set it to the limits
                newValue = Math.Max(0.0f, Math.Min(100.0f, newValue));

                // Distribute the difference across the other percentages to maintain the sum
                float remainingDifference = difference - (newValue - currentValue);
                float adjustmentStep = remainingDifference / (percentages.Count - 1);

                for (int i = 0; i < percentages.Count; i++)
                {
                    if (i != index)
                    {
                        percentages[i] += adjustmentStep;
                    }
                }

                // Set the new value at the specified index
                percentages[index] = newValue;
            }

            return percentages;
        }
    }
}