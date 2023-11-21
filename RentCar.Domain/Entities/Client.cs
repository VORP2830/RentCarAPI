using System.Text.RegularExpressions;
using RentCar.Domain.Exceptions;

namespace RentCar.Domain.Entities;

public class Client : BaseEntity
{
    public string Name { get; protected set; }
    public string CPF { get; protected set; }
    public DateOnly BirthDay { get; protected set; }
    protected Client() { }
    public Client(string name, string cpf, DateOnly birthDay)
    {
        ValidateDomain(name, cpf, birthDay);
        Active = true;
    }
    private void ValidateDomain(string name, string cpf, DateOnly birthDay)
    {
        RentCarException.When(name.Length < 3, "Nome deve ter no mínimo 3 caracteres");
        RentCarException.When(name.Length > 50, "Nome deve ter no máximo 50 caracteres");
        cpf = Regex.Replace(cpf, "[^0-9]", "");
        RentCarException.When(cpf.Length != 11, "CPF deve ter 11 números");
        RentCarException.When(birthDay > DateOnly.FromDateTime(DateTime.Now), "Você não pode nascer no futuro");
        Name = name.Trim();
        CPF = cpf;
        BirthDay = birthDay;
    }
}
