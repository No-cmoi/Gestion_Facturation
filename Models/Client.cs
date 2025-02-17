using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis;

namespace Gestion_Facturation.Models;

public class Client
{
    [Key]
    public int Id { get; set; }

    public string Nom { get; set; }
    
    public string AdresseRue { get; set; }
    
    public string AdresseCp { get; set; }
    
    public string AdresseVille { get; set; }
    
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [DataType(DataType.PhoneNumber)]
    public string Telephone { get; set; }

    public ICollection<Projet> Projets { get; set; }

    public Client()
    {
        Projets = new List<Projet>();
    }
}