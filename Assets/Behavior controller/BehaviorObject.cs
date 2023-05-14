/*�����δ����ṩ��һ����UnityӦ����ʵ�ֵ�״̬��ϵͳ��״̬����һ�ֹ������״̬ת���ķ�ʽ�������������͹��������ƶ������Ϊ���������δ����У�ʵ����һ������������״̬�������ڹ�����Ϸ��Ӧ���еĲ�ͬ��Ϊ��

���ȣ�BehaviorController ����״̬���Ŀ�������������ǰ״̬�Լ���������Ϊ����ʹ�� currentState ������׷�ٵ�ǰ״̬����ʹ�� behaviors �б����洢��ÿ��״̬��������Ϊ��

��Σ�BehaviorObject ���ʾ������Ϊ�����������ݡ�����������Ϊ�����ȼ��������ʹ�����Ϊ�ı�����

StateBehavior ���ʾ��ͬ��״̬������״̬�����������ȼ��͹�������Ϊ��ÿ��״̬����һ�����ȼ������ھ�����ǰ�״̬��״̬�е�����ͨ�� Condition �ࣨ�� BoolCondition �� FloatCondition������ʾ�����Ƕ����˴���״̬ת����������

���ConditionVariable �ࣨ�� BoolVariable �� FloatVariable����ʾ�����ı��������Ǳ�����������������ֵ��

������ԣ������δ���ʵ����һ������������״̬��ϵͳ�����ڹ���UnityӦ���в�ͬ״̬�µ���Ϊ�����������͹���״̬�������Զ��л�״̬����������Ӧ����Ϊ*/

using System.Collections.Generic;
using UnityEngine;

// ��Ϊ�����࣬���ڴ�����Ϊ����
[CreateAssetMenu(menuName = "Behavior Object")]
[System.Serializable]
public class BehaviorObject : ScriptableObject
{
    public List<StateBehavior> stateBehaviors; // ״̬��Ϊ�б����ڴ洢��ͬ״̬�µ���Ϊ
    [SerializeReference]
    public List<ConditionVariable> ConditionVariables; // ���������б����ڴ洢��ͬ�����ı���

    public BehaviorObject()
    {
        stateBehaviors = new List<StateBehavior>();
        ConditionVariables = new List<ConditionVariable>();
    }
}

// �������������࣬���ڱ�ʾ��ͬ���͵���������
[System.Serializable]
public abstract class ConditionVariable
{
    public string name; // ������
    public ConditionType type; // ��������

    public enum ConditionType
    {
        BoolVariable, // �����ͱ���
        FloatVariable // �����ͱ���
    }
}

// �����ͱ����࣬�̳����������������࣬���ڱ�ʾ�����ͱ���
[System.Serializable]
public class FloatVariable : ConditionVariable
{
    public FloatVariable()
    {
        type = ConditionType.FloatVariable;
    }
}

// �����ͱ����࣬�̳����������������࣬���ڱ�ʾ�����ͱ���
[System.Serializable]
public class BoolVariable : ConditionVariable
{
}

// ״̬��Ϊ�࣬���ڱ�ʾ��ͬ״̬�µ���Ϊ
[System.Serializable]
public class StateBehavior
{
    public string stateName; // ״̬��
    public int priority;// ���ȼ�
    public bool effect;//����һ��״̬��������״̬���ⴥ����Ч��
    [SerializeField]
    [SerializeReference]
    public List<Condition> conditions; // �����б����ڴ洢��ͬ����

    public bool showConditions; // �Ƿ���ʾ����
    public bool allowBufferedInput;
    public bool AllowReEnterDuringProcess;
    public StateBehavior()
    {
        conditions = new List<Condition>();
    }
}

// ���������࣬���ڱ�ʾ��ͬ���͵�����
[System.Serializable]
public abstract class Condition
{
    public string conditionName; // ������
}

// �����������࣬�̳������������࣬���ڱ�ʾ����������
[System.Serializable]
public class BoolCondition : Condition
{
    public bool conditionValue; // ����ֵ

    public bool CheckCondition(bool value)
    {
        return value == conditionValue; // ��������Ƿ����
    }
}

// �����������࣬�̳������������࣬���ڱ�ʾ����������
[System.Serializable]
public class FloatCondition : Condition
{
    public float conditionValue; // ����ֵ
    public ComparisonType comparisonType; // �Ƚ�����

    public bool CheckCondition(float value)
    {
        switch (comparisonType)
        {
            case ComparisonType.Equals: // ����
                return value == conditionValue;
            case ComparisonType.Greater: // ����
                return value > conditionValue;
            case ComparisonType.Smaller: // С��
                return value < conditionValue;
            default:
                return false;
        }
    }
    [System.Serializable]
    public enum ComparisonType
    {
        Equals,
        Greater,
        Smaller
    }
}


