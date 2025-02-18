using System.ComponentModel.DataAnnotations;
using Gestion_Facturation.Data.Enums;

namespace Gestion_Facturation.Models;

public class Tarif
{
    [Key]
    public int Id { get; set; }

    public string Description { get; set; }

    public decimal PrixUnitaire { get; set; }

    public TypeTarif TypeTarif { get; set; }

    public ICollection<LigneDevis> LignesDevis { get; set; }
    public ICollection<LigneFacture> LignesFacture { get; set; }
}