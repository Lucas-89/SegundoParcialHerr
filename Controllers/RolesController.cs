using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SegundoParcialHerr.Models;
using SegundoParcialHerr.ViewModels;

namespace SegundoParcialHerr.Controllers;
[Authorize(Roles = "Administrador")]
public class RolesController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly RoleManager<IdentityRole>  _roleManager;

    public RolesController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        // aca debe listar los roles
       var roles = _roleManager.Roles.ToList();
        return View(roles);
    }
    
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(RoleCreateViewModel model)
    {
        // verifico si el roll no es nulo
        if (string.IsNullOrEmpty(model.RoleName))
        {
            return View();
        }
        
        bool roleExists =  _roleManager.RoleExistsAsync(model.RoleName).Result;

        // verifico si el rol existe
        if (roleExists)
        {
            ModelState.AddModelError("", "El rol ya existe.");
            return View();
        }
        
        var role = new IdentityRole(model.RoleName);
        _roleManager.CreateAsync(role);

        return RedirectToAction("Index");
    }


}
