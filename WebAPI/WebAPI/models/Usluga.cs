using System.ComponentModel.DataAnnotations;

namespace WebAPI.models
{
    public class Usluga
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nazwa { get; set; }

        [Required]
        [RegularExpression("(?i)^(budowlana|projektowa|edukacyjna)$", ErrorMessage = "Rodzaj musi być: budowlana, projektowa lub edukacyjna.")]
        public string Rodzaj { get; set; }

        [Range(0, 2025, ErrorMessage = "Rok nie może być większy niż 2025.")]
        public int Rok { get; set; }

        public string Wykonawca { get; set; }
    }
}