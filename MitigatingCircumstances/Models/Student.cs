﻿using Google.Cloud.Datastore.V1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MitigatingCircumstances.Models
{
    public class Student
    {
        public Key Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }


        [EmailAddress]
        public string Email { get; set; }
    }
}