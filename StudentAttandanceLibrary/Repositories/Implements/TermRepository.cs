using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.Repositories.Implements
{
    public class TermRepository : ITermRepository
    {
        StudentAttendanceManagementContext context = new StudentAttendanceManagementContext();

        public void AddTerm(Term term)
        {
            throw new NotImplementedException();
        }

        public void DeleteTerm(Term term)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Term> GetAllTerms()
        {
            var query = context.Terms;
            return query;
        }

        public List<Term> GetListTerms()
        {
            throw new NotImplementedException();
        }

        public void UpdateTerm(Term term)
        {
            throw new NotImplementedException();
        }

    }
}
