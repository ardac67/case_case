
using System;
using System.ComponentModel.DataAnnotations;
public class Comments
{
    public Guid Id { get; set; }

    
    [Required(ErrorMessage = "Comment is required.")]
    [StringLength(500, ErrorMessage = "Do not exceed 500 characters.")]
    public string Comment { get; set; }
    [Required(ErrorMessage = "User ID is required.")]
    public Guid UserId { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Film ID is required.")]
    public Guid FilmId { get; set; }
    public Film Film { get; set; }
    [Range(1, 10, ErrorMessage = "Point must be between 1 and 10.")]
    public int Point { get; set; }
}


