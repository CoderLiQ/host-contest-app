using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HostContestApp.Domain.Abstract;
using HostContestApp.Domain.Entities;
using System.Web;
using System.IO;

namespace HostContestApp.WebUI.Controllers
{   
    public class AdminController : Controller
      {
         IFormRepository repository;

        DateTime tmpDate;
        DateTime tmpClosingDate;
        
         public AdminController(IFormRepository repo)
          {            
            repository = repo;
           }

        public ViewResult About()
        {
            return View();
        }
        public ViewResult Index()
        {
            TempData["IsAttDeleted"] = false;
            return View(repository.Forms);
        }

        [HttpPost]
        public ViewResult Index(int? formTypeFilter)
        {            
            //if (!formTypeFilter.HasValue)
            //{
            //    return View(repository.Forms);
            //}
            return View(repository.Forms.Where(f => f.TypeId == formTypeFilter));
        }

        public ViewResult Create()
        {
            return View("Create", new Form ());
        }

        //public ViewResult FilterForms()
        //{
        //    return View("Create", new Form());
        //}

        //[HttpGet]
        


        [HttpPost]
        public ActionResult Create(int? types, Form form, DateTime? ClosingDate, HttpPostedFileBase uploadFile = null)
        {
            if (ModelState.IsValid)
            {
                if (ClosingDate < DateTime.Now)
                {
                    TempData["message"] = string.Format("Дата закрытия не может быть меньше сегодняшней!");
                    ModelState.AddModelError("СlosingDate", "Дата закрытия не может быть меньше сегодняшней!");
                    return View(form);

                }

                if (!types.HasValue)
                {
                    TempData["message"] = string.Format("Выберите тип!");
                    ModelState.AddModelError("TypeId", "Пожалуйста, укажите тип");
                    return View(form);
                }
                else
                {
                    form.TypeId = Convert.ToInt32(types);
                }

                if (uploadFile != null)
                {
                    //TempData["message"] = string.Format("attachement != null");
                    string fileName = Path.GetFileName(uploadFile.FileName);

                    byte[] Data = null;
                    using (var binaryReader = new BinaryReader(uploadFile.InputStream))
                    {
                        Data = binaryReader.ReadBytes(uploadFile.ContentLength);
                    }

                    form.Attachment = Data;
                    form.AttachmentName = fileName;
                }

                form.DateEditedXTimes = 0;

                repository.SaveForm(form);
                TempData["message"] = string.Format("Новая форма № {0} добавлена", form.FormId);
                return RedirectToAction("Index");

                
            }
            else
            {
                TempData["message"] = string.Format("Ошибка! Проверьте корректность данных");
                // Что-то не так со значениями данных
                return View(form);
            }

        }

        public ViewResult Edit(int formId)
        {           

            Form form = repository.Forms
                .FirstOrDefault(f => f.FormId == formId);
            //tmpDate = form.ClosingDate;
            TempData["tmpDate"] = form.ClosingDate;            
            return View(form);
        }


        // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        public ActionResult Edit(Form form, int? types, DateTime? ClosingDate, HttpPostedFileBase uploadFile = null)
        {
            
            if (ModelState.IsValid && ClosingDate != null)
            {                    
                    tmpClosingDate = Convert.ToDateTime(ClosingDate);
                        if (form.ClosingDate < DateTime.Now)
                        {
                            ModelState.AddModelError("СlosingDate", "Дата закрытия не может быть меньше сегодняшней!");
                            TempData["message"] = string.Format("Дата закрытия не может быть меньше сегодняшней!");
                            return View(form);
                        }

                        if (!types.HasValue)
                        {
                            TempData["message"] = string.Format("Выберите тип!");
                            ModelState.AddModelError("TypeId", "Пожалуйста, укажите тип");
                            return View(form);
                        }
                        else
                        {
                            form.TypeId = Convert.ToInt32(types);
                        }

                        if (uploadFile != null)
                        {


                    if (uploadFile.ContentLength < 4000000)
                    {
                        //TempData["message"] = string.Format("attachement != null");
                        string fileName = Path.GetFileName(uploadFile.FileName);

                        byte[] Data = null;
                        using (var binaryReader = new BinaryReader(uploadFile.InputStream))
                        {
                            Data = binaryReader.ReadBytes(uploadFile.ContentLength);
                        }

                        form.Attachment = Data;
                        form.AttachmentName = fileName;
                    }
                    else
                    {
                        TempData["message"] = string.Format("Слишком большой файл! Максимальный размер - 4мб");
                        return View(form);
                    }
                        }                       


                        if (TempData["tmpDate"] != null) //этим контрится клик по кнопке "сохранить", после выдачи уведомления
                                                         //о достижении 3-х попыток.
                        {
                            if (form.ClosingDate != (DateTime)TempData["tmpDate"])
                            {
                                tmpDate = (DateTime)TempData["tmpDate"];

                                if (form.DateEditedXTimes < 3)
                                {
                                    switch (form.DateEditedXTimes)
                                    {
                                        case 0:
                                            form.ClosingDate1 = tmpDate;
                                            form.ClosingDate = tmpClosingDate;
                                            form.DateEditedXTimes = 1;
                                            break;
                                        case 1:
                                            form.ClosingDate2 = tmpDate;
                                            form.ClosingDate = tmpClosingDate;
                                            form.DateEditedXTimes = 2;
                                            break;
                                        case 2:
                                            form.ClosingDate3 = tmpDate;
                                            form.ClosingDate = tmpClosingDate;
                                            form.DateEditedXTimes = 3;
                                            break;

                                    }
                                }
                                else
                                {
                                    TempData["message"] = string.Format("Нельзя менять дату больше 3 раз!");
                                    ModelState.AddModelError("ClosingDate", "Нельзя менять дату больше 3 раз!");
                                    return View(form);
                                }
                            } 
                        }
                
                else
                {
                    TempData["message"] = string.Format("Нельзя менять дату больше 3 раз!");
                    ModelState.AddModelError("ClosingDate", "Нельзя менять дату больше 3 раз!");
                    return View(form);
                }

                if ((bool)TempData["IsAttDeleted"])
                {
                    form.Attachment = null;
                    form.AttachmentName = null;
                }            


                repository.SaveForm(form);
                TempData["message"] = string.Format("Изменения в форме № {0} были сохранены", form.FormId);
                return RedirectToAction("Index");

            }
            else
            {
                
                // Что-то не так со значениями данных
                TempData["message"] = string.Format("Ошибка! Проверьте корректность данных");
                return View(form);
            }
        }

        [HttpPost]
        public ActionResult Delete(int formId)
        {
            Form deletedForm = repository.DeleteForm(formId);
            if (deletedForm != null)
            {
                TempData["message"] = string.Format("Форма № {0} была удалена",
                    deletedForm.FormId);
            }
            return RedirectToAction("Index");
        }

        //[HttpPost]
        public ActionResult DeleteAtt(int formId)
        {
            //form.AttachmentName = null;
            TempData["IsAttDeleted"] = true;
            TempData["message"] 
                = string.Format("Файл в форме № {0} был удален. Изменения вступят в силу после сохранения формы!", 
                                                                                                        formId);
                                    
            return RedirectToAction("Edit", new { formId });
        }

        public FileResult Download(int formId)
        {
            Form form = repository.Forms
                .FirstOrDefault(f => f.FormId == formId);
            return File(form.Attachment, System.Net.Mime.MediaTypeNames.Application.Octet, form.AttachmentName);
        }



    }
}
