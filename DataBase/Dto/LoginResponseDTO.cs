﻿using backendTask.DataBase.Models;

namespace backendTask.DataBase.Dto
{
    public class LoginResponseDTO
    {
        public User email{ get; set; }
        public string token { get; set; }
    }
}
