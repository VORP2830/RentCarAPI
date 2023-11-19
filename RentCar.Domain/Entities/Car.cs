using RentCar.Domain.Exceptions;

namespace RentCar.Domain.Entities;

public class Car : BaseEntity
{
    public string Brand { get; protected set; }
    public string Model { get; protected set; }
    protected Car() { }
    public Car(string brand, string model)
    {
        RentCarException.When(brand is null,"Marca é obrigatoria");
        RentCarException.When(model is null,"Modelo é obrigatorio");
        RentCarException.When(string.IsNullOrEmpty(brand),"Marca é obrigatoria");
        RentCarException.When(string.IsNullOrEmpty(model),"Modelo é obrigatorio");
        Brand = brand.Trim();
        Model = model.Trim();
        Active = true;
    }
}
