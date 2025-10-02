using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Fleet.Domain.Entities;
public class User : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Profile { get; set; } = string.Empty;

    [StringLength(150)]
    public string? FullName { get; set; }

    [EmailAddress]
    [StringLength(150)]
    public string? Email { get; set; }

    [Phone]
    [StringLength(20)]
    public string? Phone { get; set; }
    public UserStatus Status { get; set; } = UserStatus.Active;
    public DateTime? LastLogin { get; set; }
}
