using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SegundoParcialHerr.Models;

namespace SegundoParcialHerr.ViewModels
{
    public class UserEditViewModel
    {
        public string UserName {get;set;}
        public string Email {get;set;}
        public string Role {get;set;}
        public SelectList Roles{get;set;} 
    }
}