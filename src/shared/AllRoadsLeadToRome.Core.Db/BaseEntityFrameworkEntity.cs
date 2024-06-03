using System.ComponentModel.DataAnnotations;

namespace AllRoadsLeadToRome.Core.Db;

public class BaseEntityFrameworkEntity
{
    [Key] public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
}