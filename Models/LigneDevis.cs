using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Facturation.Models;

public class LigneDevis
{
    [Key] public int Id { get; set; }

    public int DevisId { get; set; }
    public Devis Devis { get; set; }

    public int TarifId { get; set; }
    public Tarif Tarif { get; set; }

    public int Quantité { get; set; }
    
}