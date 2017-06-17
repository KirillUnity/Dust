using Newtonsoft.Json;

public class DataObject
{
    /// <summary>
    /// Уникальный идентификатор объекта
    /// </summary>
    [JsonProperty("Id")]
    public long Id { get; set; }
    /// <summary>
    /// Вес объекта
    /// </summary>
    [JsonProperty("Weight")]
    public int Weight { get; set; }
    /// <summary>
    /// Имя объекта
    /// </summary>
    [JsonProperty("name")]
    public virtual string Name { get; set; }

    /// <summary>
    /// Описание предмета
    /// </summary>
    [JsonProperty("desc")]
    public virtual string Description { get; set; }

    /// <summary>
    /// Имя типа объекта
    /// </summary>
    [JsonIgnore]
    public virtual string ObjectName
    {
        get
        {
            var name = GetType().Name;
            name = name.Substring(1, name.Length - 1);
            return name;
        }
    }

    public DataObject()
    {

    }

    public DataObject(DataObject old)
    {
        Description = old.Description;
        Name = string.Format("Copy_{0}", old.Name);
    }
}