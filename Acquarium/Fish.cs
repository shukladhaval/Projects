
namespace Acquarium
{
    public class Fish : IFish
    {
        string _name;
        string _type;
        double _foodRequire;
        string _color;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public double FoodRequire
        {
            get
            {
                return _foodRequire;
            }
            set
            {
                _foodRequire = value;
            }
        }
        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }
    }
}
