using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.API.MVC.Models.ViewModels;

public record AddPostFormViewModel(
    [Required]
    [StringLength(50, MinimumLength = 10)]
    string Title,

    [Required]
    string Content,

    IFormFile[] Images);
