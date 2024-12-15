using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO;

public class NewsInfo
{
    [Required]
    public string PostTime { get; set; } = default!;

    [Required]
    public string Header { get; set; } = default!;
}
