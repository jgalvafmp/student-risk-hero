﻿using student_risk_hero.Contracts;
using student_risk_hero.Data.Models;
using student_risk_hero.Services.EmailServices;
using student_risk_hero.Utills;
using System.Text;
using System.Text.RegularExpressions;

namespace student_risk_hero.Services
{
    public class UserService : BaseService<User>, IBaseService<User>
    {
        private readonly IEmailService emailService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IBaseRepository<User> BaseRepository;

        public UserService(IBaseRepository<User> baseRepository, IEmailService emailService, IUnitOfWork unitOfWork) : base(baseRepository)
        {
            BaseRepository = baseRepository;
            this.emailService = emailService;
            this.unitOfWork = unitOfWork;
        }


        public override User Add(User entity)
        {
            if (string.IsNullOrEmpty(entity.Firstname)) throw new ArgumentNullException(nameof(entity.Firstname));
            if (string.IsNullOrEmpty(entity.Password)) throw new ArgumentNullException(nameof(entity.Password));
            if (string.IsNullOrEmpty(entity.Username)) throw new ArgumentNullException(nameof(entity.Username));
            if (string.IsNullOrEmpty(entity.Email)) throw new ArgumentNullException(nameof(entity.Email));
            if (string.IsNullOrEmpty(entity.Role)) throw new ArgumentNullException(nameof(entity.Role));

            if (BaseRepository.Exists(x => x.Username == entity.Username)) throw new ArgumentException($"The {nameof(entity.Username)} is already taken.");
            if (entity.Password.Length < 4) throw new ArgumentException($"Field {nameof(entity.Password)} must have a length greater than 4.");
            if (!Regex.IsMatch(entity.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")) throw new ArgumentException($"Field {nameof(entity.Email)} must be a valid email.");

            entity.Password = Cryptography.Encode(entity.Password);

            var trans = unitOfWork.CreateTransaction();

            try
            {
                var createdUser = base.Add(entity);

                if(entity.Role == nameof(RoleTypes.Teacher))
                {
                    var teacher = new Teacher();
                    teacher.Firstname = entity.Firstname;
                    teacher.Lastname = entity.Lastname;
                    teacher.Gender = entity.Gender  ;
                    teacher.UserId = createdUser.Id;
                }

                emailService.SendNewUserMail(createdUser, "Welcome to Student Risk Hero", EmailTypesEnum.Welcoming);

                trans.Commit();

                return createdUser;
            }
            catch (Exception)
            {
                trans.Rollback();

                throw;
            }
        }
    }
}
