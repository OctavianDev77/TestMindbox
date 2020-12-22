namespace WebApi.Models
{
    public interface IFigureModel
    {
        int ID { get; set; }

        int SaveInDB();

        double GetSquare();
    }
}
