namespace CombatSystem
{
    public enum Type
    {
        player, enemy, other
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