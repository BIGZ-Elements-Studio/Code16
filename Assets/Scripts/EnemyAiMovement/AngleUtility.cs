using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleUtility
{

    public static Vector3 LerpAngles(Vector3 a, Vector3 b, float percentage)
    {
        percentage = Mathf.Clamp01(percentage);

        // Calculate the angles in degrees
        float angleA = Mathf.Atan2(a.x, a.z) * Mathf.Rad2Deg;
        float angleB = Mathf.Atan2(b.x, b.z) * Mathf.Rad2Deg;

        // Calculate the shortest angle difference between angleA and angleB
        float shortestAngle = Mathf.DeltaAngle(angleA, angleB);

        // Interpolate the angles based on the percentage
        float interpolatedAngle = angleA + (shortestAngle * percentage);

        // Convert the interpolated angle back to radians
        float finalAngle = interpolatedAngle * Mathf.Deg2Rad;
        // Return the direction vector based on the final angle

        return outputAngle(finalAngle);
    }
    public static Vector3 LerpAngles(float a, float b, float percentage)
    {
        percentage = Mathf.Clamp01(percentage);

        // Calculate the angles in degrees
        float angleA = a;
        float angleB = b;

        // Calculate the shortest angle difference between angleA and angleB
        float shortestAngle = Mathf.DeltaAngle(angleA, angleB);

        // Interpolate the angles based on the percentage
        float interpolatedAngle = angleA + (shortestAngle * percentage);

        // Convert the interpolated angle back to radians
        float finalAngle = interpolatedAngle * Mathf.Deg2Rad;
        // Return the direction vector based on the final angle
        return outputAngle(finalAngle);
    }
    public static float VtoA(Vector3 angle)
    {
        return Mathf.Atan2(angle.x, angle.z) * Mathf.Rad2Deg;
    }
    public static Vector3 AddAngle(float a, float b)
    {
        // Calculate the angles in degrees
        float angleA = a;
        float angleB = b;

        // Calculate the shortest angle difference between angleA and angleB
        float shortestAngle = Mathf.DeltaAngle(angleA, angleB);

        // Interpolate the angles based on the percentage
        float interpolatedAngle = angleA + angleB;
        // Convert the interpolated angle back to radians
        float finalAngle = interpolatedAngle * Mathf.Deg2Rad;
        // Return the direction vector based on the final angle
        return outputAngle(finalAngle);
    }
    public static Vector3 outputAngle(float angle)
    {
        return new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle));
    }
    public static Vector3 AddNoise(Vector3 original, float maxAngle, float roughness, float current, float seed)
    {
        float angleA = Mathf.Atan2(original.x, original.z) * Mathf.Rad2Deg;

        // Calculate the noise range based on the maxAngle
        float noiseRange = maxAngle / 2f;

        float samllestAnfle = angleA - noiseRange;
        float biggestAnfle = angleA + noiseRange;
        // Calculate the position along the noise range based on the current value
        float position = current * roughness;

        // Generate the noise value using Perlin noise
        float noise = Mathf.PerlinNoise(seed + position, 0f);
        return LerpAngles(samllestAnfle, biggestAnfle, noise);

    }

}
