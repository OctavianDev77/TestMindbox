using WebApi.Data;

namespace WebApi.Models
{
    public class CircleModel : IFigureModel
    {
        public int ID { get; set; }

        public int R { get; set; }

        public int SaveInDB()
        {
            var id = DataBase.InsertFigure(FigureType.Circle, R, null);
            return id;
        }

        public double GetSquare()
        {
            return System.Math.PI * R * R;
        }
    }
}
