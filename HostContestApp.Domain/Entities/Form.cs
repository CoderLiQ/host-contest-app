using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HostContestApp.Domain.Entities
{
    public class Form
    {
        [HiddenInput(DisplayValue = false)]
        public int FormId { get; set; }

        [Display(Name = "Дата создания заявки")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        
        [Display(Name = "Имя ответственного")]
        [Required(ErrorMessage = "Пожалуйста, введите имя ответственного")]
        public string ResponsiblePersonName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Пожалуйста, введите описание")]
        [MaxLength(300)]
        public string Description { get; set; }

        [Display(Name = "Тип заявки")]
        [Required(ErrorMessage = "Пожалуйста, укажите тип")]
        public int TypeId { get; set; }

        [Display(Name = "Ориентировочная дата закрытия")]
        [DataType(DataType.Date)]        
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Пожалуйста, введите дату")]
        public DateTime ClosingDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ClosingDate1 { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ClosingDate2 { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ClosingDate3 { get; set; }

        public int DateEditedXTimes { get; set; }

        [Display(Name = "Вложенный файл")]
        public string AttachmentName { get; set; }
        public byte[] Attachment { get; set; }

        
        
    }
}
