using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DevCommQuestionsTracker.Helpers;
using DevCommQuestionsTracker.Models;

namespace DevCommQuestionsTracker.Repository
{
    public class ExcelRepository : IQuestionsRepository
    {
        private readonly IAuthProvider _authProvider;

        // Cache the items when you fetch them for the first time. Keep adding or updating as and when udpate happens 
        private Dictionary<string, Question> CachedItems { get; set; } = new Dictionary<string, Question>();

        public ExcelRepository(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }

        public async Task<List<Question>> GetAllQuestionAsync()
        {
            var token = await _authProvider.GetAccessTokenAsync();
            throw new NotImplementedException();
        }

        public async  Task<List<Question>> GetAllQuestionAsync(Expression<Func<Question, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<Question> GetQuestionByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task AddOrUpdateQuestionAsync(string id, Question question)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteQuestionAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
