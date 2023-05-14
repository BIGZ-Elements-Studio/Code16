/*这两段代码提供了一个在Unity应用中实现的状态机系统。状态机是一种管理对象状态转换的方式，它根据条件和规则来控制对象的行为。在这两段代码中，实现了一个基于条件的状态机，用于管理游戏或应用中的不同行为。

首先，BehaviorController 类是状态机的控制器，它管理当前状态以及关联的行为。它使用 currentState 变量来追踪当前状态，并使用 behaviors 列表来存储与每个状态关联的行为。

其次，BehaviorObject 类表示定义行为和条件的数据。它包含了行为的优先级、条件和触发行为的变量。

StateBehavior 类表示不同的状态，包括状态的条件、优先级和关联的行为。每个状态都有一个优先级，用于决定当前活动状态。状态中的条件通过 Condition 类（如 BoolCondition 和 FloatCondition）来表示，它们定义了触发状态转换的条件。

最后，ConditionVariable 类（如 BoolVariable 和 FloatVariable）表示条件的变量，它们保存了用于条件检查的值。

整体而言，这两段代码实现了一个基于条件的状态机系统，用于管理Unity应用中不同状态下的行为。根据条件和规则，状态机可以自动切换状态，并触发相应的行为*/

using System.Collections.Generic;
using UnityEngine;

// 行为对象类，用于创建行为对象
[CreateAssetMenu(menuName = "Behavior Object")]
[System.Serializable]
public class BehaviorObject : ScriptableObject
{
    public List<StateBehavior> stateBehaviors; // 状态行为列表，用于存储不同状态下的行为
    [SerializeReference]
    public List<ConditionVariable> ConditionVariables; // 条件变量列表，用于存储不同条件的变量

    public BehaviorObject()
    {
        stateBehaviors = new List<StateBehavior>();
        ConditionVariables = new List<ConditionVariable>();
    }
}

// 条件变量抽象类，用于表示不同类型的条件变量
[System.Serializable]
public abstract class ConditionVariable
{
    public string name; // 变量名
    public ConditionType type; // 变量类型

    public enum ConditionType
    {
        BoolVariable, // 布尔型变量
        FloatVariable // 浮点型变量
    }
}

// 浮点型变量类，继承自条件变量抽象类，用于表示浮点型变量
[System.Serializable]
public class FloatVariable : ConditionVariable
{
    public FloatVariable()
    {
        type = ConditionType.FloatVariable;
    }
}

// 布尔型变量类，继承自条件变量抽象类，用于表示布尔型变量
[System.Serializable]
public class BoolVariable : ConditionVariable
{
}

// 状态行为类，用于表示不同状态下的行为
[System.Serializable]
public class StateBehavior
{
    public string stateName; // 状态名
    public int priority;// 优先级
    public bool effect;//不是一个状态，独立于状态轴外触发的效果
    [SerializeField]
    [SerializeReference]
    public List<Condition> conditions; // 条件列表，用于存储不同条件

    public bool showConditions; // 是否显示条件
    public bool allowBufferedInput;
    public bool AllowReEnterDuringProcess;
    public StateBehavior()
    {
        conditions = new List<Condition>();
    }
}

// 条件抽象类，用于表示不同类型的条件
[System.Serializable]
public abstract class Condition
{
    public string conditionName; // 条件名
}

// 布尔型条件类，继承自条件抽象类，用于表示布尔型条件
[System.Serializable]
public class BoolCondition : Condition
{
    public bool conditionValue; // 条件值

    public bool CheckCondition(bool value)
    {
        return value == conditionValue; // 检查条件是否成立
    }
}

// 浮点型条件类，继承自条件抽象类，用于表示浮点型条件
[System.Serializable]
public class FloatCondition : Condition
{
    public float conditionValue; // 条件值
    public ComparisonType comparisonType; // 比较类型

    public bool CheckCondition(float value)
    {
        switch (comparisonType)
        {
            case ComparisonType.Equals: // 等于
                return value == conditionValue;
            case ComparisonType.Greater: // 大于
                return value > conditionValue;
            case ComparisonType.Smaller: // 小于
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


