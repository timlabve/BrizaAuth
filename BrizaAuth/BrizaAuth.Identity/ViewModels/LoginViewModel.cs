﻿namespace BrizaAuth.Identity.ViewModels
{
  public class LoginViewModel
  {
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string Token { get; set; }
  }
}
