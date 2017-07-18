namespace Data
{
    public struct FieldContainer
    {
        public bool Saved;

        public int currentMob;

        public int currentItem;

        public DataField Field;

        public FieldContainer(DataField field)
        {
            Saved = true;
            currentMob = 1;
            currentItem = 1;
            Field = field;
        }
    }
}
