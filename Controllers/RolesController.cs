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

    public async Task<IActionResult> Edit(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);

        var roleVM = new RoleCreateViewModel();
        roleVM.RoleName = role.Name ?? string.Empty;

        return View(roleVM);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(RoleCreateViewModel model)
    {
        var role = await _roleManager.FindByNameAsync(model.RoleName);
        if (model != null)
        {
            await _roleManager.UpdateAsync(role);
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(string id)
    {
        if (id == null )
        {
            return NotFound();
        }

        var rol = await _roleManager.FindByIdAsync(id);
        if (rol == null)
        {
            return NotFound();
        }

        return View(rol);
    }
    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var rol = await _roleManager.FindByIdAsync(id);

        await _roleManager.DeleteAsync(rol);
        
        return RedirectToAction(nameof(Index));
    }

}
