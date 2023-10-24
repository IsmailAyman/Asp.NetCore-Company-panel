using AutoMapper;
using Company.BLL.Interfaces;
using Company.BLL.Repositrios;
using Company.DAL.Models;
using Company.PL.Helpers;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
	[Authorize]
	public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

       

        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> Employees;
            if (string.IsNullOrEmpty(SearchValue))
                Employees = await unitOfWork.EmployeeRepository.GetAll();
            else
                Employees = unitOfWork.EmployeeRepository.GetEmployeesbyName(SearchValue);

            var MappedEmployee = mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(Employees);
            return View(MappedEmployee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewBag.Departments = departmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel EmployeeVM)
        {
            if (ModelState.IsValid)
            {
                var MappedEmp = mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);
                MappedEmp.ImageName = DocumentSettings.UploadImage(EmployeeVM.image,"Images");
                await unitOfWork.EmployeeRepository.Add(MappedEmp);
                 int result = await unitOfWork.Complete();
                if (result > 0)
                {
                    TempData["message"] = "Employee is Created successfully!";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(EmployeeVM);
        }

        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var employee = await unitOfWork.EmployeeRepository.Get(id.Value);

            if (employee is null)
                return NotFound();
            var MappedEmployee = mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(viewName, MappedEmployee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Departments = unitOfWork.DepartmentRepository.GetAll();
            return await Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id, EmployeeViewModel EmployeeVM)
        {
            if (id != EmployeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedEmp = mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);
                    unitOfWork.EmployeeRepository.Update(MappedEmp);
                    await unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
            }
            return View(EmployeeVM);

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel EmployeeVM)
        {
            if (id != EmployeeVM.Id)
                return BadRequest();
            try
            {
                var MappedEmp = mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);
                if(MappedEmp.ImageName is not null)
                {
                    DocumentSettings.DeleteFile(MappedEmp.ImageName, "Images");
                }
                unitOfWork.EmployeeRepository.Delete(MappedEmp);
                await unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(EmployeeVM);
            }
        }
    }
}
