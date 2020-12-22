
POST на url "/figure"

для создания круга
{
    "circle": {
        "R": 2
    }
}

для создания треугольника
{
    "triangle": {
        "H": 10,
        "A": 20
    }
}



чтобы довавить новый тип фигур 
1. добавить наследника от IFigureModel с реализациями SaveInDB и GetSquare
2. в ModelParser добавить парсинг json в новый объект фигуры
3. добавить DataBase в ReadFigures чтение новой фигуры