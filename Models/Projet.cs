using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Gestion_Facturation.Models;

public class Projet
{
    [Key]
    public int Id { get; set; }

    public string NomProjet { get; set; }

    public string Description { get; set; }

    public DateOnly DateDebut { get; set; }

    public DateOnly DateFin { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; }

    public ICollection<Devis> Devis { get; set; }
    public ICollection<Facture> Factures { get; set; }

    public Projet()
    {
        Devis = new List<Devis>();
        Factures = new List<Facture>();
    }
}