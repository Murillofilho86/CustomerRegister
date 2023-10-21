using System.Text.RegularExpressions;

namespace CustomerMicroService.Framework.DomainObject
{
    public struct Email
    {
        public const int EnderecoMaxLength = 100;
        public const int EnderecoMinLength = 5;
        private string _mailAddress;

        public Email(string endereco)
        {
            if (!Validar(endereco)) throw new DomainException("E-mail inválido");
            _mailAddress = endereco;
        }

        public static bool Validar(string email)
        {
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return regexEmail.IsMatch(email);
        }

        public override bool Equals(object obj) => obj is Email email && Equals(email);

        public bool Equals(Email other) => _mailAddress == other._mailAddress;

        public override int GetHashCode()
        {
            unchecked
            {
                return _mailAddress.GetHashCode() * 97;
            }
        }

        public static implicit operator string(Email email) => email._mailAddress;
        public static implicit operator Email(string email) => new Email(email);
    }
}
