using System.ComponentModel.DataAnnotations;
using Gestion_Facturation.Data.Enums;

namespace Gestion_Facturation.Models;

public class Devis
{
    [Key]
    public int Id { get; set; }

    public string NumeroDevis { get; set; }

    public DateOnly DateDevis { get; set; }

    public decimal MontantTotal { get; set; }

    public StatutDevis Statut { get; set; }

    public int ProjetId { get; set; }
    public Projet Projet { get; set; }

    public ICollection<LigneDevis> LignesDevis { get; set; }

    public Devis()
    {
        LignesDevis = new List<LigneDevis>();
    }
}