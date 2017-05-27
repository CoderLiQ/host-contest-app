using System.Collections.Generic;
using HostContestApp.Domain.Entities;
using HostContestApp.Domain.Abstract;
using System;

namespace HostContestApp.Domain.Concrete
{
    public class EFFormRepository : IFormRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Form> Forms
        {
            get { return context.Forms; }
        }    

        public void SaveForm(Form form)
        {
            if (form.FormId == 0)
            {
                form.Date = DateTime.Now;
                form.ClosingDate1 = DateTime.Now;
                form.ClosingDate2 = DateTime.Now;
                form.ClosingDate3 = DateTime.Now;

                context.Forms.Add(form);
            }
            else
            {
                Form dbEntry = context.Forms.Find(form.FormId);
                if (dbEntry != null)
                {
                    //dbEntry.ResponsiblePersonName = form.ResponsiblePersonName;
                    dbEntry.Description = form.Description;
                    dbEntry.TypeId = form.TypeId;

                    dbEntry.ClosingDate = form.ClosingDate;
                    dbEntry.ClosingDate1 = form.ClosingDate1;
                    dbEntry.ClosingDate2 = form.ClosingDate2;
                    dbEntry.ClosingDate3 = form.ClosingDate3;
                    dbEntry.DateEditedXTimes = form.DateEditedXTimes;

                    dbEntry.Attachment = form.Attachment;
                    dbEntry.AttachmentName = form.AttachmentName;


                }
            }
            context.SaveChanges();
        }

        public Form DeleteForm(int formId)
        {
            Form dbEntry = context.Forms.Find(formId);
            if (dbEntry != null)
            {
                context.Forms.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
