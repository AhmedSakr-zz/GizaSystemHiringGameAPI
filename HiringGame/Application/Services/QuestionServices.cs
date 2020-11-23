using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HiringGame.Application.Dtos;
using HiringGame.Application.Interfaces;
using HiringGame.Common;
using HiringGame.DataModel;
using Microsoft.EntityFrameworkCore;

namespace HiringGame.Application.Services
{
    public class QuestionServices : IQuestionServices
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public QuestionServices(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IList<QuestionDto> GetList(int typeId)
        {
            var allQuestions = _dbContext.Questions.AsQueryable().Where(t => t.QuestionTypeId == typeId).ToList();
            return _mapper.Map<List<QuestionDto>>(allQuestions).ToList();
        }

        public ClientMessage<int> Create(QuestionDto model)
        {
            var clientMessage = new ClientMessage<int>();

            try
            {
                if (model.QuestionTypeId == 1)
                {
                    if (model.Answers.Count < 3)
                    {
                        clientMessage.ClientMessageContent = new List<string> { "Question must have 3 answers or more." };
                        clientMessage.ReturnedData = 0;
                        clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                        return clientMessage;
                    }

                    if (model.Answers.Count(t => t.IsCorrectAnswer) != 1)
                    {
                        clientMessage.ClientMessageContent = new List<string> { "Question must have one correct answer." };
                        clientMessage.ReturnedData = 0;
                        clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                        return clientMessage;
                    }
                }
                else
                {
                    if (model.Answers.Count < 2)
                    {
                        clientMessage.ClientMessageContent = new List<string> { "Question must have 2 answers or more." };
                        clientMessage.ReturnedData = 0;
                        clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                        return clientMessage;
                    }

                    if (model.Answers.Any(answer => string.IsNullOrWhiteSpace(answer.PersonalityKey)))
                    {
                        clientMessage.ClientMessageContent = new List<string> { "All answers must have personality key" };
                        clientMessage.ReturnedData = 0;
                        clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                        return clientMessage;
                    }
                }

                var question = _mapper.Map<Question>(model);
                _dbContext.Questions.Add(question);
                _dbContext.SaveChanges();

                clientMessage.ClientMessageContent = new List<string> { "Question created successfully" };
                clientMessage.ReturnedData = question.Id;
                clientMessage.ClientStatusCode = DataEnum.OperationStatus.Ok;
                return clientMessage;
            }
            catch (Exception ex)
            {
                clientMessage.ClientMessageContent = new List<string> { ex.Message };
                clientMessage.ReturnedData = 0;
                clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                return clientMessage;
            }

        }

        public ClientMessage<QuestionDto> GetById(int id)
        {
            var clientMessage = new ClientMessage<QuestionDto>();

            var question = _dbContext.Questions.Include(t => t.Answers)
                .FirstOrDefault(t => t.Id == id && t.IsActive);

            if (question == null)
            {
                clientMessage = new ClientMessage<QuestionDto>
                {
                    ClientStatusCode = DataEnum.OperationStatus.Error,
                    ClientMessageContent = new List<string> { "Id is not correct" },
                    ReturnedData = null
                };
                return clientMessage;
            }


            clientMessage.ReturnedData = _mapper.Map<QuestionDto>(question);
            clientMessage.ClientStatusCode = DataEnum.OperationStatus.Ok;
            return clientMessage;



        }

        public List<QuestionDto> GetByWhere(Expression<Func<Question, bool>> predicate)
        {
            var allQuestions = _dbContext.Questions.AsQueryable().Where(predicate).ToList();
            return _mapper.Map<List<QuestionDto>>(allQuestions).ToList();
        }

        public ClientMessage<int> Update(QuestionDto model)
        {
            var clientMessage = new ClientMessage<int>();

            try
            {
                if (model.QuestionTypeId == 1)
                {
                    if (model.Answers.Count < 3)
                    {
                        clientMessage.ClientMessageContent = new List<string> { "Question must have 3 answers or more." };
                        clientMessage.ReturnedData = 0;
                        clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                        return clientMessage;
                    }

                    if (model.Answers.Count(t => t.IsCorrectAnswer) != 1)
                    {
                        clientMessage.ClientMessageContent = new List<string> { "Question must have one correct answer." };
                        clientMessage.ReturnedData = 0;
                        clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                        return clientMessage;
                    }
                }
                else
                {
                    if (model.Answers.Count < 2)
                    {
                        clientMessage.ClientMessageContent = new List<string> { "Question must have 2 answers or more." };
                        clientMessage.ReturnedData = 0;
                        clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                        return clientMessage;
                    }

                    if (model.Answers.Any(answer => string.IsNullOrWhiteSpace(answer.PersonalityKey)))
                    {
                        clientMessage.ClientMessageContent = new List<string> { "All answers must have personality key" };
                        clientMessage.ReturnedData = 0;
                        clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                        return clientMessage;
                    }
                }

                var question = _dbContext.Questions.Find(model.Id);
                _mapper.Map(model, question);

                question.Answers = new List<Answer>();


                var answersIds = model.Answers.Select(t => t.Id).ToList();
                var oldAnswers = _dbContext.Answers.Where(t => t.QuestionId == model.Id).ToList();
                var deletedAnswers = oldAnswers.Where(t => !answersIds.Contains(t.Id)).ToList();
                var updatedAnswers = oldAnswers.Where(t => answersIds.Contains(t.Id)).ToList();
                var newAnswers = model.Answers.Where(t => t.Id == 0).ToList();

                _dbContext.Answers.RemoveRange(deletedAnswers);
                _dbContext.SaveChanges();

                foreach (var updatedAnswer in updatedAnswers)
                {
                    _mapper.Map(model.Answers.First(t => t.Id == updatedAnswer.Id), updatedAnswer);
                    updatedAnswer.QuestionId = model.Id.Value;
                    updatedAnswer.UpdatedById = model.UpdatedById;
                    updatedAnswer.UpdatedDate = model.UpdatedDate;
                }

                foreach (var newAnswer in newAnswers)
                {
                    newAnswer.QuestionId = model.Id.Value;
                }
                _dbContext.Answers.AddRange(_mapper.Map<IList<Answer>>(newAnswers));

                _dbContext.SaveChanges();

                clientMessage.ClientMessageContent = new List<string> { "Question updated successfully" };
                clientMessage.ReturnedData = question.Id;
                clientMessage.ClientStatusCode = DataEnum.OperationStatus.Ok;
                return clientMessage;

            }
            catch (Exception ex)
            {
                clientMessage.ClientMessageContent = new List<string> { ex.Message };
                clientMessage.ReturnedData = 0;
                clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                return clientMessage;
            }

        }
    }
}
