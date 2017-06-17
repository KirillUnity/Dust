
public class FieldContainer{

    public bool Saved { get; set; }

    public DataField Field { get; set; }

    public FieldContainer(DataField field)
    {
        Field = field;
    }
}
