using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using HostContestApp.Domain.Abstract;
using HostContestApp.Domain.Entities;
using HostContestApp.WebUI.Controllers;

namespace HostContestApp.UnitTest
{
    [TestClass]
    public class UnitTest1
    {  

        [TestMethod]
        public void Can_Edit_Form()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IFormRepository> mock = new Mock<IFormRepository>();
            mock.Setup(m => m.Forms).Returns(new List<Form>
    {
                new Form { FormId = 1, Description = "desc1"},
                new Form { FormId = 2, Description = "desc2"},
                new Form { FormId = 3, Description = "desc3"},
                new Form { FormId = 4, Description = "desc4"},
                new Form { FormId = 5, Description = "desc5"}
    });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            Form form1 = controller.Edit(1).ViewData.Model as Form;
            Form form2 = controller.Edit(2).ViewData.Model as Form;
            Form form3 = controller.Edit(3).ViewData.Model as Form;

            // Assert
            Assert.AreEqual(1, form1.FormId);
            Assert.AreEqual(2, form2.FormId);
            Assert.AreEqual(3, form3.FormId);
        }

        /// <summary>
        /// Перестал проходить после добавления строчки 
        /// TempData["tmpDate"] = form.ClosingDate в AdminController
        /// </summary>
        [TestMethod]
        public void Cannot_Edit_Nonexistent_Form()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IFormRepository> mock = new Mock<IFormRepository>();
            mock.Setup(m => m.Forms).Returns(new List<Form>
              {
                new Form { FormId = 1, Date = DateTime.Now, ClosingDate = new DateTime(2018, 7, 20), Description = "desc1"},
               });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            Form result = controller.Edit(2).ViewData.Model as Form;

            // Assert
        }

        [TestMethod]
        public void Can_Delete_Forms()
        {
            // Организация - создание объекта Form
            Form form = new Form { FormId = 2, Description = "desc2" };

            // Организация - создание имитированного хранилища данных
            Mock<IFormRepository> mock = new Mock<IFormRepository>();
            mock.Setup(m => m.Forms).Returns(new List<Form>
    {
        new Form { FormId = 1, Description = "desc1"},
        new Form { FormId = 2, Description = "desc2"}
    });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие - удаление 
            controller.Delete(form.FormId);

            // Утверждение - проверка того, что метод удаления в хранилище
            // вызывается для корректного объекта Form
            mock.Verify(m => m.DeleteForm(form.FormId));
        }

        [TestMethod]
        public void Index_Contains_All_Forms()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IFormRepository> mock = new Mock<IFormRepository>();
            mock.Setup(m => m.Forms).Returns(new List<Form>
            {
                new Form { FormId = 1, Description = "desc1"},
                new Form { FormId = 2, Description = "desc2"},
                new Form { FormId = 3, Description = "desc3"},
                new Form { FormId = 4, Description = "desc4"},
                new Form { FormId = 5, Description = "desc5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            List<Form> result = ((IEnumerable<Form>)controller.Index().
                ViewData.Model).ToList();

            // Утверждение
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("desc1", result[0].Description);
            Assert.AreEqual("desc2", result[1].Description);
            Assert.AreEqual("desc3", result[2].Description);
        }


        [TestMethod]
        public void Can_Filter_Forms()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IFormRepository> mock = new Mock<IFormRepository>();
            mock.Setup(m => m.Forms).Returns(new List<Form>
            {
                new Form { FormId = 1, Description = "desc1", TypeId = 1},
                new Form { FormId = 2, Description = "desc2", TypeId = 1},
                new Form { FormId = 3, Description = "desc3", TypeId = 2},
                new Form { FormId = 4, Description = "desc4", TypeId = 2},
                new Form { FormId = 5, Description = "desc5", TypeId = 4}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            List<Form> result1 = ((IEnumerable<Form>)controller.Index(2).
                ViewData.Model).ToList();
            

            // Утверждение
            Assert.AreEqual(result1.Count(), 2);

            foreach (var r in result1)
            {
                Assert.AreEqual(2, r.TypeId);           

            }
        }

    }
}