﻿namespace prj_QLPKDK.Models
{
    public class LoginResponseModel
    {
        public string UserName { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
    }
}
