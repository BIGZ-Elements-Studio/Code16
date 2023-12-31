namespace CombatSystem
{
    public interface CharacterBuff
    {
       public void initiate(CharaBuffContainer target);
        public void overlying(CharacterBuff overlayedBuff, CharaBuffContainer target);
        public void OnRemoved();
        public void overlayed();
    }

    public interface TeamBuff
    {
        public void initiate(FieldForTeamBuff target);
        public void overlying(TeamBuff overlayedBuff, FieldForTeamBuff target);

        public void overlayed();
    }
}
