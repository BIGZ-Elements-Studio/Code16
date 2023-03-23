public abstract class CollectRole
{
    //����
    public string Name
    {
        get
        {
            if (string.IsNullOrEmpty(_name))
                _name = GetType().Name;
            return _name;
        }
        set => _name = value;
    }
    //����
    public string Description
    {
        get
        {
            if (string.IsNullOrEmpty(_description))
                _description = Name + "_Description";
            return _description;
        }
        set => _description = value;
    }
    //�ȼ�
    public int Level { get; }
    //�Ͽɶ�
    public int Recognition { get => _recognition; set => _recognition = value; }
    //�ռ���ɫ����
    public abstract CollectRoleMode CollectRoleMode { get; }


    private int _recognition;
    private string _name;
    private string _description;
}


//�ռ���ɫ-����
public class CollectWeapon : CollectRole
{
    public override CollectRoleMode CollectRoleMode { get { return CollectRoleMode.Weapon; } }

    public void ChargeAttack()
    {
        // TODO: Implement code for charging attack
    }

    public void ReleaseAttack()
    {
        // TODO: Implement code for releasing attack
    }

    public void PassiveSkill()
    {
        // TODO: Implement code for passive skill
    }

    public void ProvideAttribute()
    {
        // TODO: Implement code for providing attributes to player
    }
}


//�ռ���ɫ-����
public class CollectSkill : CollectRole
{
    public override CollectRoleMode CollectRoleMode { get { return CollectRoleMode.Skill; } }
    //ʩ��ʱ��
    public float CastTime { get => _castTime; set => _castTime = value; }
    //��ȴʱ��
    public float CooldownTime { get => _cooldownTime; set => _cooldownTime = value; }

    private float _castTime;
    private float _cooldownTime;

    public void ChargeSkill()
    {
        // TODO: Implement code for charging skill
    }

    public void ReleaseSkill()
    {
        // TODO: Implement code for releasing skill
    }

    public void PassiveSkill()
    {
        // TODO: Implement code for passive skill
    }

    public void ProvideAttribute()
    {
        // TODO: Implement code for providing attributes to player
    }
}



public enum CollectRoleMode
{
    Weapon,
    Skill,
    Passive,
}