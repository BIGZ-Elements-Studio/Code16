namespace CombatSystem
{
    public enum TargetType
    {
        player, enemy, other
    }
    public enum CombatColor
    {
        blue, red, yellow,empty
    }
    public class DamageObject
    {
        public int damage;
        public int hardness;
        public bool Critic;
        public static DamageObject GetdamageObject()
        {
            return new DamageObject();
        }
    }
}