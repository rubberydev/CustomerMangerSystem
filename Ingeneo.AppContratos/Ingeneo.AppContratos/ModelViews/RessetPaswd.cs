﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ingeneo.AppContratos.ModelViews
{
    public class RessetPaswd
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }
    }
}