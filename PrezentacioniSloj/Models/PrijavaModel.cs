using System.ComponentModel.DataAnnotations;

public class PrijavaModel
{
    [Required(ErrorMessage = "Unesite korisničko ime.")]
    public string KorisnickoIme { get; set; } = "";

    [Required(ErrorMessage = "Unesite lozinku.")]
    [DataType(DataType.Password)]
    public string Lozinka { get; set; } = "";
}
