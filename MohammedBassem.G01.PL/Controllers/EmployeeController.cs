using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MohammedBassem.G01.BLL.interfaces;
using MohammedBassem.G01.DAL.Models;
using MohammedBassem.G01.PL.Helpers;
using MohammedBassem.G01.PL.ViewModels;
using System.Collections.ObjectModel;

namespace MohammedBassem.G01.PL.Controllers
{
	[Authorize]
	public class EmployeeController : Controller
    {

        //private readonly IEmployeeRepository _employeeRepository; //NULL --> GetAll دي عشان اجيب منها الميثود اللي اسمها property انا عملت ال
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper mapper;

        private readonly IUnitOfWork UnitOfWork;

        public EmployeeController(
            //IEmployeeRepository employeeRepository,
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper) // ASK CLR To Create Object From DepartmentRepository
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            UnitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        //Data base مع ال connection فلازم افتح Data base ده هعرض فيه كل حاجه موجوده في ال view ال
        public async Task<IActionResult> Index(string SearchInput)
        {
            var employees = Enumerable.Empty<Employee>();
            var employeeViewModels = new Collection<EmployeeViewModel>();
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = await UnitOfWork.employeeRepository.GetAllAsync();
            }
            else
            {
                employees = await UnitOfWork.employeeRepository.GetByNameAsync(SearchInput);
            }

            //Auto Mapping
            var Result = mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

            //Note : Navigational Property ل loading مش بيعمل EF Core by Default ال

            var employee = await UnitOfWork.employeeRepository.GetAllAsync();

            //View ل ال Action لو جيت ف يوم من الايام وعاوز تبعب معلومه زياده من ال
            //[Extra Informations] Transfer Data From Action To View's [One Ways] <-- وبستخدمه عشان اعمل View Dictionary ف انت هتعتمد علي حاجه اسمها
            // قدامك 3 طرق هما :

            //string Message = "Hello World";
            // 1. ViewData : Property Inherited From Controller - Dictionary من نوع

            // ViewData["Message"] = Message + " From View Data";

            // 2. ViewBag : Property Inherited From Controller - Dynamic من نوع
            //ViewBag.viewBag = Message + " From View Bag";


            // 3. TempData : Property Inherited From Controller - Dictionary من نوع
            // للتاني request بستخدمه لما اعوز ابعت داتا من --> Use Transfer For The Data From Request To Another
            //TempData["Message01"] = Message + "From Temp Data";
            return View(Result);
        }

        public async Task<IActionResult> Create()
        {
            var department = await UnitOfWork.departmentRepository.GetAllAsync(); // Extar Information
                                                                        // View's Dictionary 
                                                                        // 1.ViewData
            ViewData["Department"] = department;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {

            if (ModelState.IsValid) //Server Side Validation
            {
                model.ImageName = DocumentSettings.UploadFile(model.Image, "images");


                // Insert Department To DataBase

                //Casting : EmployeeViewModel --> Employee
                // Manual Mapping

                //Employee employee = new Employee()
                //{
                //    Id = model.Id,
                //    Name = model.Name,
                //    Age = model.Age,
                //    Adderss = model.Adderss,
                //    salary= model.salary,
                //    PhoneNumber = model.PhoneNumber,
                //    Email = model.Email,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    DateOfCreation = model.DateOfCreation,
                //    HiringData = model.HiringData,
                //    WorkFor = model.WorkFor,
                //    WorkForId = model.WorkForId,
                //};

                //////////////////////////////////////////////////////////////

                //Auto Mapping
                var employee1 = mapper.Map<Employee>(model);

                var count = await UnitOfWork.employeeRepository.AddAsync(employee1);
                if (count > 0)
                {
                    TempData["Message"] = "Employee is Created Successfully";
                }
                else
                {
                    TempData["Message"] = "Employee is Bot Created Successfully";

                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();
         
            var employee = await UnitOfWork.employeeRepository.GetAsync(id.Value);
            
            if (employee is null) return NotFound();

            //var employeeViewModel = new EmployeeViewModel()
            //{
            //    Name = employee.Name,
            //    Age = employee.Age,
            //    Adderss = employee.Adderss,
            //    DateOfCreation = employee.DateOfCreation,
            //    Email = employee.Email,
            //    Id = employee.Id,
            //    HiringData = employee.HiringData,
            //    salary = employee.salary,
            //    Image = null,
            //    IsActive = employee.IsActive,
            //    IsDeleted = employee.IsDeleted,
            //    PhoneNumber = employee.PhoneNumber,
            //    ImageName = employee.ImageName,
            //    WorkFor = employee.WorkFor,
            //    WorkForId = employee.WorkForId,

            //};
          var result =  mapper.Map<EmployeeViewModel>(employee);

            return View(ViewName, result);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //    if (id is null) return BadRequest(); 
            //    var employee = _employeeRepository.Get(id.Value);
            //    if (employee is null) return NotFound();
            //    return View(employee);

            ////////////////
            var department = await UnitOfWork.departmentRepository.GetAllAsync(); // Extar Information
            // 1.ViewData
            ViewData["Department"] = department;
            //var employee = _employeeRepository.Get(id);
            //return View(employee);
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Request بتمنع ان اي حد من برا يبعت 
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel model)
        {
            try
            {
                // Manual Mapping
                //Employee employee1 = new Employee()
                //{
                //    Id = model.Id,
                //    Name = model.Name,
                //    Age = model.Age,
                //    Adderss = model.Adderss,
                //    salary = model.salary,
                //    PhoneNumber = model.PhoneNumber,
                //    Email = model.Email,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    DateOfCreation = model.DateOfCreation,
                //    HiringData = model.HiringData,
                //    WorkFor = model.WorkFor,
                //    WorkForId = model.WorkForId,
                //};

                //////////////////////////////////////////////////////////////
                //Auto Mapping

                if (model.ImageName is not null)
                {
                    DocumentSettings.DeleteFile(model.ImageName, "images");
                }
                model.ImageName = DocumentSettings.UploadFile(model.Image, "images");

                var employee = mapper.Map<Employee>(model);

                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    var count = await UnitOfWork.employeeRepository.UpdateAsync(employee);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            //if (id is null)
            //{
            //    return BadRequest();
            //}
            //var employee = _employeeRepository.Get(id.Value);
            //if (employee is null) return NotFound();
            //return View(employee);

            ////////////////////
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, EmployeeViewModel model)
        {
            if (id != model.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    // Manual Mapping
                    Employee employee = new Employee()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Age = model.Age,
                        Adderss = model.Adderss,
                        salary = model.salary,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        IsActive = model.IsActive,
                        IsDeleted = model.IsDeleted,
                        DateOfCreation = model.DateOfCreation,
                        HiringData = model.HiringData,
                        WorkFor = model.WorkFor,
                        WorkForId = model.WorkForId,
                    };
                    var employees = mapper.Map<Employee>(model);
                    var count = await UnitOfWork.employeeRepository.DeleteAsync(employee);
                    if (count > 0)
                    {
                        DocumentSettings.DeleteFile(model.ImageName, "images");
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    throw;
                }
            }
            return View(model);

        }
  
    }
}
