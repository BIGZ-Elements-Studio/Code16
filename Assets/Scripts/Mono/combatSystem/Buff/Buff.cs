namespace CombatSystem
{
    public interface CharacterBuff
    {
        public void Add(Atkpoint hP);
       public void initiate(CharaBuffContainer target);
        public void overlying(CharacterBuff overlayedBuff, CharaBuffContainer target);

        public void overlayed();
    }

    public interface TeamBuff
    {
        public void Add(HPContainer hP);
        public void initiate(TeamBuffContainer target);
        public void overlying(TeamBuff overlayedBuff, TeamBuffContainer target);

        public void overlayed();
    }
}
