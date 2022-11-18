using System.Text;

namespace student_risk_hero.Utills
{
    public static class Cryptography
    {
        public static string Encode(string val)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(val));
        }

        public static string Decode(string val)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(val));
        }
    }
}
