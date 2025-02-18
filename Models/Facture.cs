using System.ComponentModel.DataAnnotations;
using Gestion_Facturation.Data.Enums;

namespace Gestion_Facturation.Models;

public class Facture
{
    [Key]
    public int Id { get; set; }

    public string NumeroFacture { get; set; }

    public DateOnly DateFacture { get; set; }

    public decimal MontantTotal { get; set; }

    public StatutFacture StatutFacture { get; set; }

    public int ProjetId { get; set; }
    public Projet Projet { get; set; }

    public ICollection<LigneFacture> LignesFacture { get; set; }
    public ICollection<Acompte> Acomptes { get; set; }

    public Facture()
    {
        LignesFacture = new List<LigneFacture>();
        Acomptes = new List<Acompte>();
    }
}