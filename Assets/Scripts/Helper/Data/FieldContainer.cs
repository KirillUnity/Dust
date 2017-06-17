namespace Data
{
    public class FieldContainer
    {

        public bool Saved { get; set; }

        public int currentMob { get; set; }

        public int currentItem { get; set; }

        public DataField Field { get; set; }

        public FieldContainer(DataField field)
        {
            Field = field;
        }
    }
}
