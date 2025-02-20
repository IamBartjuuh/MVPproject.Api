using System.ComponentModel.DataAnnotations;

namespace ProjectNaam.WebApi;

public class Object2D
{
    public Guid Id { get; set; }

    [Required]
    public int EnviromentId { get; set; }

    [Required]
    public double PositionX { get; set; }

    [Required]
    public double PositionY { get; set; }

    [Required]
    public double ScaleX { get; set; }

    [Required]
    public double ScaleY { get; set; }

    [Required]
    public double RotationZ { get; set; }

    [Required]
    public int Sortinglayer { get; set; }
}
