using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Facturation.Models;

public class LigneFacture
{
    [Key] 
    public int Id { get; set; }

    public int FactureId { get; set; }
    public Facture Facture { get; set; }

    public int TarifId { get; set; }
    public Tarif Tarif { get; set; }

    public int Quantité { get; set; }

}