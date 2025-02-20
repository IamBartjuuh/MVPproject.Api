using System.ComponentModel.DataAnnotations;

namespace ProjectNaam.WebApi;
public class Enviroment2D
{
    public Guid Id { get; set; }

    [Required]
    [Length(5,25)]
    public string Name { get; set; }
    public string OwnerUserId { get; set; }
    public int MaxLength { get; set; }
    public int MaxHeight { get; set; }
}
