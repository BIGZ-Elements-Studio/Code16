# Code16——游戏项目文档

        简介：本游戏自搭框架，拟搭建一个2d跑图，3d/2.5d战斗的游戏，游戏现阶段大致框架如下列所示：



1. [目录结构](#1目录结构)

2. [manager组件](#2manager组件)

3. [资源管理](#3资源管理)

4. [战斗系统](#4战斗系统)

5. [剧情系统](#5剧情系统)

6. [UI](#6ui)

7. [工具脚本](#7工具脚本)

8. [外部包](#8外部包)
   
   

---



## 1.目录结构

```
+---Scenes    //存放场景
\---Scripts
    \---Mono    //存放继承mono的unity脚本 
```



## 2.manager组件

        待更新



## 3.资源管理

        待更新



## 4.战斗系统

        战斗系统相关的类图如下：<img title="" src="file:///E:/章鱼开发文件/code16/ClassDiagram1.png" alt="ClassDiagram1" style="zoom:100%;">



> LeadingRole类：

字段：

        List<CollectRole>    collectRoles    以list集合列表的形式，存储收集角色类

        FightingAttribute    fightingAttribute    存储战斗属性类

        NaturalAttribute    naturalAttribute    存储自然属性类

        PlayerController    playerController    存储角色控制方法类

属性（索引器）：

        int    Level    等级

        int    Experience    经验

嵌套类：

        FightingAttribute    内部成员为战斗属性和方法

        NaturalAttribute    内部成员为加点属性和方法。加点属性值改变会影响战斗属性；方法可初始化属性或改变某一个属性的值。



> CollectRole类

属性：

        int    Recognition    认可度

        string    Name    收集角色名字

        CollectRoleMode    CollectRoleMode    收集角色类型

        int    Level    收集角色等级

        string    Description 收集角色的描述

子类：

        CollectWeapon    收集角色武器类

        CollectSkill       收集角色技能类

> Controller类        

        以状态模式为原型搭建此类和其子类，采用系统事件Action作为状态容器，通过AddState和RemoveState方法改变状态，状态继承unity的Start和Update方法，即状态运行在游戏帧上。

字段：

        System.Action state    状态容器

方法：

        AddState    给事件容器state添加StateName对应的方法（反射的方式动态获取stateName对应的方法）

```
public virtual void AddState(string stateName)
{
    var method = Delegate.CreateDelegate(typeof(Action), this, GetType().GetMethod(stateName, BindingFlags.NonPublic | BindingFlags.Instance));
    state += method as Action;
}
```

        RemoveState    给事件容器state移除StateName对应的方法

```
public virtual void RemoveState(string stateName)
{
    var method = Delegate.CreateDelegate(typeof(Action), this, GetType().GetMethod(stateName, BindingFlags.NonPublic | BindingFlags.Instance));
    state -= method as Action;
}

```

子类：

         EnemyController

        PlayerController



## 5.剧情系统

        待更新



## 6.UI

        待更新



## 7.工具脚本

        待更新



## 8.外部包

- spine    2d动画工具
