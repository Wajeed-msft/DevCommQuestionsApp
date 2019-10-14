using DevCommQuestionsTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevCommQuestionsTracker.Repository
{
    public interface IQuestionsRepository
    {
        Task<List<Question>> GetAllQuestionAsync();
        Task<List<Question>> GetAllQuestionAsync(Expression<Func<Question, bool>> predicate);
        Task<Question> GetQuestionByIdAsync(string id);
        Task AddOrUpdateQuestionAsync(string id, Question question);
        Task DeleteQuestionAsync(string id);
    }
}
