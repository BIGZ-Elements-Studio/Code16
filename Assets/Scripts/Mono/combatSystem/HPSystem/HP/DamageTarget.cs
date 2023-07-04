using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface DamageTarget
{

    public CombatColor getColor();
    public TargetType getType();
    public void addBuff(CharacterBuff buff);
    public void addBuff(TeamBuff buff);
    public  void addforce(Vector3 originalWorldPosition,float magnitude);
    public void addforceOfDirection(Vector3 direction);
    // Method to apply damage to HP
    public bool Damage(DamageObject damage, CombatColor damageColor);

    // Method to apply damage to HP by percentage
    public void DamageByPercent(int percent, CombatColor damageColor);

    public Vector3 getCenterPosition();
}
