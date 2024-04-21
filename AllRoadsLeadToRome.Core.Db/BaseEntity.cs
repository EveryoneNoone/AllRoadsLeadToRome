using System.ComponentModel.DataAnnotations;

namespace AllRoadsLeadToRome.Core.Db;

public class BaseEntity
{
    [Key] public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
}