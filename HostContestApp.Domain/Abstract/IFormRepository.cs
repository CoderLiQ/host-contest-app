using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostContestApp.Domain.Entities;

namespace HostContestApp.Domain.Abstract
{
    public interface IFormRepository
    {

        IEnumerable<Form> Forms { get; }
        void SaveForm(Form form);
        Form DeleteForm(int formId);
    }
}
