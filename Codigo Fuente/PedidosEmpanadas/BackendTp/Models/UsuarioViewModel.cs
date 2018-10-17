﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendTp.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public UsuarioViewModel() { }
        public UsuarioViewModel(int id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}