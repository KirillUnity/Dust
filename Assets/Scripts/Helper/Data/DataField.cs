using Newtonsoft.Json;

public class DataField : DataObject
{
    #region FieldSize
    [JsonProperty("height")]
    public int Height { get; set; }
    [JsonProperty("width")]
    public int Width { get; set; }
    #endregion

    #region Mobs
    [JsonProperty("mobsAmountMin")]
    public int MobsAmountMin { get; set; }

    [JsonProperty("mobsAmountMax")]
    public int MobsAmountMax { get; set; }

    #endregion

    #region Enter

    [JsonProperty("RandomEnterPosition")]
    public bool RandomEnterPosition { get; set; }

    [JsonIgnore]
    public IntPos EnterPosition
    {
        get
        {
            if (!RandomEnterPosition)
                return _enterPosition;
            else
            {
                return _enterRandomPosition;
            }
        }
        set
        {
            if (!RandomEnterPosition)
                _enterPosition = value;
            else _enterRandomPosition = value;
        }
    }

    [JsonIgnore]
    private IntPos _enterRandomPosition = new IntPos(0, 0);

    [JsonProperty("enterPosition")]
    public IntPos _enterPosition = new IntPos(0, 0);

    #endregion

    #region Exit
    [JsonProperty("exitPosition")]
    public IntPos ExitPosition { get; set; }

    [JsonProperty("needKillMobs")]
    public bool NeedKillMobs { get; set; }

    #endregion

    public DataField()
    {
    }

    public class IntPos
    {
        [JsonProperty("x")]
        public int X { get; set; }
        [JsonProperty("y")]
        public int Y { get; set; }

        public IntPos(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public IntPos()
        {
            this.X = 0;
            this.Y = 0;
        }

        public IntPos(IntPos old)
        {
            this.X = old.X;
            this.Y = old.Y;
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}]", this.X, this.Y);
        }

        public static IntPos operator +(IntPos firts, IntPos second)
        {
            return new IntPos()
            {
                X = firts.X + second.X,
                Y = second.Y + second.Y
            };
        }

        public override bool Equals(object o)
        {
            return o.ToString() == this.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
 
