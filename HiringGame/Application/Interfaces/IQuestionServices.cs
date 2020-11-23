using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HiringGame.Application.Dtos;
using HiringGame.Common;
using HiringGame.DataModel;

namespace HiringGame.Application.Interfaces
{
    public interface IQuestionServices
    {
        IList<QuestionDto> GetList(int typeId);
        ClientMessage<int> Create(QuestionDto model);
        ClientMessage<int> Update(QuestionDto model);
        ClientMessage<QuestionDto> GetById(int id);
        List<QuestionDto> GetByWhere(Expression<Func<Question, bool>> predicate);
    }
}
