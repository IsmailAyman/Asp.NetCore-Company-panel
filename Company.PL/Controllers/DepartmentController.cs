using AutoMapper;
using Company.BLL.Interfaces;
using Company.BLL.Repositrios;
using Company.DAL.Models;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
	[Authorize]
	public class DepartmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DepartmentController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Department> Departments;
            if(string.IsNullOrEmpty(SearchValue))
             Departments = await unitOfWork.DepartmentRepository.GetAll();
            else
                Departments = unitOfWork.DepartmentRepository.GetDepartmentsByName(SearchValue);

            var DepartmentMapped = mapper.Map<IEnumerable<Department>,IEnumerable<DepartmentViewModel>>(Departments);
            return View(DepartmentMapped);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            if(ModelState.IsValid)
            {
                var DepartmentMapped = mapper.Map<DepartmentViewModel ,Department>(departmentVM);               
                    await unitOfWork.DepartmentRepository.Add(DepartmentMapped);
                int result = await unitOfWork.Complete();
                if (result > 0)
                {
                    TempData["Message"] = "Department is Created successfully!";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(departmentVM);
        }

        public async Task<IActionResult> Details(int? id , string viewName ="Details")
        {
            if (id is null)
                return BadRequest();

            var department = await unitOfWork.DepartmentRepository.Get(id.Value);

            if (department is null)
                return NotFound();
            var DepartmentMapped = mapper.Map<Department, DepartmentViewModel>(department);
            return View(viewName, DepartmentMapped);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");        

        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id,DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var DepartmentMapped = mapper.Map<DepartmentViewModel, Department>(departmentVM);
                    unitOfWork.DepartmentRepository.Update(DepartmentMapped);
                   await unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                   
                }
            }
            return View(departmentVM);
           
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id) 
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]int id,DepartmentViewModel departmentVM)
        {
            if(id != departmentVM.Id)
                return BadRequest();
            try
            {
                var DepartmentMapped = mapper.Map<DepartmentViewModel, Department>(departmentVM);
                unitOfWork.DepartmentRepository.Delete(DepartmentMapped);
               await unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(departmentVM);
            }
        }

    }
}
