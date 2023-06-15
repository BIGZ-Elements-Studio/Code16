namespace CombatSystem
{
    public interface Buff
    {
        public void Add(HPController hP);
       public void initiate(BuffContainer target);
        public void overlying(Buff overlayedBuff, BuffContainer target);

        public void overlayed();
    }
}
