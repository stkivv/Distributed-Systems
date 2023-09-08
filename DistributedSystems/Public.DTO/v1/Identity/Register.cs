﻿using System.ComponentModel.DataAnnotations;

namespace Public.DTO.v1.Identity;

public class Register
{
    [StringLength(128, MinimumLength = 1, ErrorMessage = "Incorrect length")]
    public string Email { get; set; } = default!;
    
    [StringLength(128, MinimumLength = 1, ErrorMessage = "Incorrect length")]
    public string Password { get; set; } = default!;
    
    [StringLength(128, MinimumLength = 1, ErrorMessage = "Incorrect length")]
    public string Firstname { get; set; } = default!;

    [StringLength(128, MinimumLength = 1, ErrorMessage = "Incorrect length")]
    public string Lastname { get; set; } = default!;

}