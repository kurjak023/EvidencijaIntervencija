using System.ComponentModel.DataAnnotations;

public class RegistracijaModel
{
    [Required(ErrorMessage = "Ime je obavezno.")]
    [StringLength(40, ErrorMessage = "Ime ne sme biti duže od 40 karaktera.")]
    public string Ime { get; set; } = "";

    [Required(ErrorMessage = "Prezime je obavezno.")]
    [StringLength(40, ErrorMessage = "Prezime ne sme biti duže od 40 karaktera.")]
    public string Prezime { get; set; } = "";

    [Required(ErrorMessage = "Korisničko ime je obavezno.")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Korisničko ime mora imati 3–20 karaktera.")]
    [RegularExpression(@"^[A-Za-z0-9._-]+$", ErrorMessage = "Dozvoljena su slova, brojevi i ._-")]
    public string KorisnickoIme { get; set; } = "";

    [Required(ErrorMessage = "Lozinka je obavezna.")]
    [DataType(DataType.Password)]
    public string Lozinka { get; set; } = "";

    [Required(ErrorMessage = "Potvrdite lozinku.")]
    [DataType(DataType.Password)]
    [Compare(nameof(Lozinka), ErrorMessage = "Lozinke se ne poklapaju.")]
    public string PotvrdaLozinke { get; set; } = "";
}
