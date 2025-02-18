using System.ComponentModel.DataAnnotations;

namespace Gestion_Facturation.Models;

public class Acompte
{
    [Key]
    public int Id { get; set; }

    public DateOnly DateAcompte { get; set; }

    public decimal MontantAcompte { get; set; }

    public int FactureId { get; set; }
    public Facture Facture { get; set; }
}