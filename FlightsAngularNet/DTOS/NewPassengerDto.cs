using System.ComponentModel.DataAnnotations;

namespace FlightsAngularNet.DTOS
{
    public record NewPassengerDto
   (
        [Required]
        [StringLength(100,MinimumLength =3)]
        [EmailAddress]
        string Email,
        [Required]
        [MinLength(2),MaxLength(100)]
        string FirstName,
        [Required]
        [MinLength(2),MaxLength(100)]
        string LastName ,
        [Required]
         bool Gender
        
        );
}
