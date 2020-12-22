using WebApi.Data;

namespace WebApi.Models
{
    public class TriangleModel : IFigureModel
    {
        public int ID { get; set; }

        public int H { get; set; }

        public int A { get; set; }

        public int SaveInDB()
        {
            var id = DataBase.InsertFigure(FigureType.Triangle, H, A);
            return id;
        }

        public double GetSquare()
        {
            return  H * A / 2;
        }
    }
}
