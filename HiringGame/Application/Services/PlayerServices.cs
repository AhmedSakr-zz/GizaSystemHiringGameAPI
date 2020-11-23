using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HiringGame.Application.Dtos;
using HiringGame.Application.Interfaces;
using HiringGame.Common;
using HiringGame.DataModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HiringGame.Application.Services
{
    public class PlayerServices : IPlayerServices
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IQuestionServices _questionServices;

        [Obsolete]
        public PlayerServices(IMapper mapper,
            AppDbContext dbContext,
            IWebHostEnvironment hostingEnvironment,
            IQuestionServices questionServices)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
            _questionServices = questionServices;
        }

        public List<PlayerForReturnDto> GetList()
        {
            var allPlayers = _dbContext.Players.AsQueryable().Include(t => t.Job).ToList();
            return _mapper.Map<List<PlayerForReturnDto>>(allPlayers).ToList();
        }

        public PlayerForReturnDto GetById(int id)
        {
            var player = _dbContext.Players.AsQueryable().Include(t => t.Job).FirstOrDefault();
            return player == null ? null : _mapper.Map<PlayerForReturnDto>(player);
        }

        public ClientMessage<PlayerScoreDto> GetPlayerScore(int playerId)
        {
            var clientMessage = new ClientMessage<PlayerScoreDto>();

            var player = _dbContext.Players.Include(t => t.Transactions)
                .ThenInclude(t => t.Answer)
                .Include(t => t.Transactions)
                .ThenInclude(t => t.Question)
                .Include(t => t.Job)
                .FirstOrDefault(t => t.Id == playerId && t.IsActive);

            if (player == null)
            {
                clientMessage = new ClientMessage<PlayerScoreDto>
                {
                    ClientStatusCode = DataEnum.OperationStatus.Error,
                    ClientMessageContent = new List<string> { "Id is not correct" }
                };
                return clientMessage;
            }

            var disResult = GetPlayerDiscReport(playerId);
            clientMessage.ReturnedData = _mapper.Map<PlayerScoreDto>(player);
            clientMessage.ReturnedData.DiscResults = disResult.ToList();
            clientMessage.ReturnedData.Player = _mapper.Map<PlayerForReturnDto>(player);
            clientMessage.ClientStatusCode = DataEnum.OperationStatus.Ok;
            return clientMessage;
        }

        public ClientMessage<RegistrationResult> Register(PlayerDto dto)
        {

            var clientMessage = new ClientMessage<RegistrationResult>();

            try
            {
                var player = _mapper.Map<Player>(dto);

                var alreadyExist = _dbContext.Players.Include(t => t.Transactions).ThenInclude(t => t.Answer)
                    .ThenInclude(t => t.Question).FirstOrDefault(t => t.EmailAddress == player.EmailAddress);

                if (alreadyExist != null)
                {

                    var personalityQuestions = alreadyExist.Transactions.Select(t => t.Answer.Question)
                        .Where(t => t.QuestionTypeId == (int)AppEnum.QuestionTypes.Level_3).Select(t => t.Id).Distinct();
                    if (personalityQuestions.Count() == 7)
                    {
                        clientMessage.ReturnedData = new RegistrationResult { PlayerId = alreadyExist.Id, NewPlayer = false };
                        clientMessage.ClientStatusCode = DataEnum.OperationStatus.Ok;
                        return clientMessage;
                    }
                    else
                    {
                        _dbContext.Players.Remove(alreadyExist);
                        _dbContext.SaveChanges();
                    }

                }

                if (dto.PlayerCV != null)
                {
                    var physicalFileName = Guid.NewGuid().ToString();
                    physicalFileName += Path.GetExtension(dto.PlayerCV.FileName);
                    var virtualFileName = dto.PlayerCV.FileName;
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "App_Files/");
                    using (var fileStream = new FileStream(Path.Combine(filePath, physicalFileName), FileMode.Create))
                    {
                        dto.PlayerCV.CopyTo(fileStream);
                    }

                    player.CvPhysicalName = physicalFileName;
                    player.CvVirtualName = virtualFileName;

                }

                player.CreatedById = -1;
                player.CreatedDate = DateTime.Now;
                player.IsActive = true;


                _dbContext.Players.Add(player);
                _dbContext.SaveChanges();

                clientMessage.ClientMessageContent = new List<string> { "Player created successfully" };
                clientMessage.ReturnedData = new RegistrationResult { PlayerId = player.Id, NewPlayer = true };

                clientMessage.ClientStatusCode = DataEnum.OperationStatus.Ok;
                return clientMessage;

            }
            catch (Exception ex)
            {
                clientMessage.ClientMessageContent = new List<string> { ex.Message + " " + ex.InnerException };
                clientMessage.ReturnedData = new RegistrationResult();
                clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                return clientMessage;
            }
        }

        public ClientMessage<bool> SubmitTotalScore(SubmitScoreDto dto)
        {

            var clientMessage = new ClientMessage<bool>();

            try
            {
                var player = _dbContext.Players.FirstOrDefault(t => t.Id == dto.Id);
                if (player == null)
                {
                    clientMessage.ClientMessageContent = new List<string> { "Player is not existing" };
                    clientMessage.ReturnedData = false;
                    clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                    return clientMessage;
                }

                player.Score = dto.TotalScore;
                _dbContext.SaveChanges();
                clientMessage.ClientMessageContent = new List<string> { "Player score saved successfully." };
                clientMessage.ReturnedData = true;
                clientMessage.ClientStatusCode = DataEnum.OperationStatus.Ok;
                return clientMessage;

            }
            catch (Exception ex)
            {
                clientMessage.ClientMessageContent = new List<string> { ex.Message + " " + ex.InnerException };
                clientMessage.ReturnedData = false;
                clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                return clientMessage;
            }
        }

        public ClientMessage<List<QuestionForReturnDto>> GetLevel2Questions()
        {
            var clientMessage = new ClientMessage<List<QuestionForReturnDto>>();
            try
            {
                var questions = _dbContext.Questions
                    .Where(t => t.IsActive && t.QuestionTypeId == 1)
                    .Include(t => t.Answers);

                foreach (var question in questions)
                {
                    question.Answers = question.Answers.Where(t => t.IsActive).ToList();
                }

                clientMessage.ReturnedData = _mapper.Map<List<QuestionForReturnDto>>(questions);
                clientMessage.ClientStatusCode = DataEnum.OperationStatus.Ok;
                return clientMessage;
            }
            catch (Exception ex)
            {
                clientMessage.ClientMessageContent = new List<string> { ex.Message + " " + ex.InnerException };
                clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                return clientMessage;
            }
        }

        public ClientMessage<List<QuestionForReturnDto>> GetLevel3Questions()
        {
            var clientMessage = new ClientMessage<List<QuestionForReturnDto>>();
            try
            {
                var questions = _dbContext.Questions
                    .Where(t => t.IsActive && t.QuestionTypeId == 2)
                    .Include(t => t.Answers);

                foreach (var question in questions)
                {
                    question.Answers = question.Answers.Where(t => t.IsActive).ToList();
                }

                clientMessage.ReturnedData = _mapper.Map<List<QuestionForReturnDto>>(questions);
                clientMessage.ClientStatusCode = DataEnum.OperationStatus.Ok;
                return clientMessage;
            }
            catch (Exception ex)
            {
                clientMessage.ClientMessageContent = new List<string> { ex.Message + " " + ex.InnerException };
                clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                return clientMessage;
            }
        }

        public ClientMessage<int> CreateLevel2Transaction(TransactionDto dto)
        {
            var clientMessage = new ClientMessage<int>();
            const int correctScore = 10;
            const int wrongScore = 0;
            try
            {
                var transaction = _mapper.Map<Transaction>(dto);
                var player = _dbContext.Players.FirstOrDefault(t => t.Id == dto.PlayerId);
                var question = _questionServices.GetById(dto.QuestionId).ReturnedData;

                var answer = question.Answers.FirstOrDefault(t => t.Id == dto.AnswerId);

                if (player == null)
                {
                    clientMessage.ClientMessageContent = new List<string> { "Error: Player id is not correct" };
                    clientMessage.ReturnedData = 0;
                    clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                    return clientMessage;
                }
                if (question.Id == null || question.Id == 0)
                {
                    clientMessage.ClientMessageContent = new List<string> { "Error: Question id is not correct" };
                    clientMessage.ReturnedData = 0;
                    clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                    return clientMessage;
                }
                if (answer == null)
                {
                    clientMessage.ClientMessageContent = new List<string> { "Error: Selected answer does not belong the the question" };
                    clientMessage.ReturnedData = 0;
                    clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                    return clientMessage;
                }

                if (answer.IsCorrectAnswer)
                {
                    transaction.Score = correctScore.ToString();
                    if (int.TryParse(player.Score, out var playerScore))
                    {
                        playerScore += correctScore;
                        player.Score = playerScore.ToString();
                    }
                    else
                    {
                        player.Score = correctScore.ToString();
                    }

                }
                else
                {
                    transaction.Score = wrongScore.ToString();
                }

                transaction.CreatedById = -1;
                transaction.CreatedDate = DateTime.Now;
                transaction.IsActive = true;
                _dbContext.Transactions.Add(transaction);
                _dbContext.SaveChanges();

                clientMessage.ClientMessageContent = new List<string> { "Transaction created successfully" };
                clientMessage.ReturnedData = transaction.Id;
                clientMessage.ClientStatusCode = DataEnum.OperationStatus.Ok;
                return clientMessage;

            }
            catch (Exception ex)
            {
                clientMessage.ClientMessageContent = new List<string> { ex.Message + " " + ex.InnerException };
                clientMessage.ReturnedData = 0;
                clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                return clientMessage;
            }
        }

        //[
        //{
        //    "PlayerId":1,
        //    "QuestionId":6,
        //    "AnswerId":7,
        //    "PersonalityAnswerChoiceId":1
        //},
        //{
        //    "PlayerId":1,
        //    "QuestionId":6,
        //    "AnswerId":9,
        //    "PersonalityAnswerChoiceId":3
        //},
        //{
        //    "PlayerId":1,
        //    "QuestionId":6,
        //    "AnswerId":9,
        //    "PersonalityAnswerChoiceId":2
        //}
        //]
        public ClientMessage<bool> CreateLevel3Transaction(List<TransactionDto> dto)
        {
            var clientMessage = new ClientMessage<bool>();

            if (!dto.Any())
            {
                clientMessage.ClientMessageContent = new List<string> { "Error: there is no answers." };
                clientMessage.ReturnedData = false;
                clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                return clientMessage;
            }


            try
            {
                var transactionList = _mapper.Map<List<Transaction>>(dto);
                var player = _dbContext.Players.FirstOrDefault(t => t.Id == dto.First().PlayerId);
                var question = _questionServices.GetById(dto.First().QuestionId).ReturnedData;

                if (player == null)
                {
                    clientMessage.ClientMessageContent = new List<string> { "Error: Player id is not correct" };
                    clientMessage.ReturnedData = false;
                    clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                    return clientMessage;
                }
                if (question.Id == null || question.Id == 0)
                {
                    clientMessage.ClientMessageContent = new List<string> { "Error: Question id is not correct" };
                    clientMessage.ReturnedData = false;
                    clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                    return clientMessage;
                }

                foreach (var transaction in transactionList)
                {
                    var answer = question.Answers.FirstOrDefault(t => t.Id == transaction.AnswerId);
                    if (answer == null)
                    {
                        clientMessage.ClientMessageContent = new List<string>
                            {$"Error: answer {transaction.AnswerId} does not belong the the question"};
                        clientMessage.ReturnedData = false;
                        clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                        return clientMessage;
                    }

                    transaction.CreatedById = -1;
                    transaction.CreatedDate = DateTime.Now;
                    transaction.IsActive = true;
                    _dbContext.Transactions.Add(transaction);
                }

                _dbContext.SaveChanges();

                clientMessage.ClientMessageContent = new List<string> { "Transactions created successfully" };
                clientMessage.ReturnedData = true;
                clientMessage.ClientStatusCode = DataEnum.OperationStatus.Ok;
                return clientMessage;

            }
            catch (Exception ex)
            {
                clientMessage.ClientMessageContent = new List<string> { ex.Message + " " + ex.InnerException };
                clientMessage.ReturnedData = false;
                clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                return clientMessage;
            }
        }

        private IEnumerable<DiscResult> GetPlayerDiscReport(int playerId)
        {
            var discList = new List<DiscResult>
                {
                    new DiscResult {PersonalityKey = "D", Score = 0},
                    new DiscResult {PersonalityKey = "I", Score = 0},
                    new DiscResult {PersonalityKey = "S", Score = 0},
                    new DiscResult {PersonalityKey = "C", Score = 0}
                };
            var queryable = _dbContext.Transactions.Where(t =>
                    t.PlayerId == playerId && t.Question.QuestionTypeId == (int)AppEnum.QuestionTypes.Level_3 &&
                    t.IsActive)
                .Select(t => new
                {
                    t.Player.Id,
                    t.Player.FirstName,
                    t.Player.LastName,
                    t.Answer.PersonalityKey,
                    t.PersonalityAnswerChoice.ChoiceValue
                });
            foreach (var item in discList)
            {
                item.Score = queryable.Where(t => t.PersonalityKey == item.PersonalityKey)
                    .Select(t => t.ChoiceValue)
                    .Sum();
            }

            discList = discList.OrderByDescending(t => t.Score).ToList();

            return discList;
        }
    }
}
