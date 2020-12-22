using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FigureController : ControllerBase
    {
        public FigureController()
        {
        }

        [HttpGet]
        public IEnumerable<IFigureModel> Get()
        {
            var data = DataBase.ReadFigures().ToArray();
            return data;
        }

        [HttpGet("{id}")]
        public double? Get(int id)
        {
            var data = DataBase.ReadFigures();
            var item = data.FirstOrDefault(f => f.ID == id);

            return item != null ? item.GetSquare() : (double?)null;
        }

        [HttpPost]
        public int Post([FromBody] JsonElement data)
        {
            var model = ModelParser.GetModel(data);
            var id = model.SaveInDB();
            return id;
        }
    }
}
