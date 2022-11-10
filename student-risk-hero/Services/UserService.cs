using student_risk_hero.Contracts;
using student_risk_hero.Data.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace student_risk_hero.Services
{
    public class UserService : BaseService<User>, IBaseService<User>
    {
        public UserService(IBaseRepository<User> baseRepository) : base(baseRepository)
        {
        }

        public override User Add(User entity)
        {
            if (string.IsNullOrEmpty(entity.Password)) throw new ArgumentNullException(nameof(entity.Password));
            if (string.IsNullOrEmpty(entity.Username)) throw new ArgumentNullException(nameof(entity.Username));
            if (string.IsNullOrEmpty(entity.Email)) throw new ArgumentNullException(nameof(entity.Email));

            if (entity.Password.Length < 4) throw new ArgumentException($"Field {nameof(entity.Password)} must have a length greater than 4.");
            if (!Regex.IsMatch(entity.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")) throw new ArgumentException($"Field {nameof(entity.Email)} must be a valid email.");

            entity.Password = Convert.ToBase64String(Encoding.ASCII.GetBytes(entity.Password));

            return base.Add(entity);
        }
    }
}
