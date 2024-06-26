using System.ComponentModel.DataAnnotations;

public class EmailRequest
{
    [Required(ErrorMessage = "Recipient email address is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string To { get; set; }

    [Required(ErrorMessage = "Subject is required.")]
    public string Subject { get; set; }

    [Required(ErrorMessage = "Body is required.")]
    public string Body { get; set; }

    [Required(ErrorMessage = "Film ID is required.")]
    public Guid film_id { get; set; }
}