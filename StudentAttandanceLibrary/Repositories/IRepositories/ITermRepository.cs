using StudentAttandanceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.Repositories.IRepositories
{
    public interface ITermRepository
    {
        public List<Term> GetListTerms();
        public void AddTerm(Term term);
        public void UpdateTerm(Term term);
        public void DeleteTerm(Term term);
    }
}
