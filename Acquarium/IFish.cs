using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acquarium
{
    public interface IFish
    {
        string Name { get; set; }
        string Type { get; set; }
        double FoodRequire { get; set; }
	string Color { get;set; }
    }
}
