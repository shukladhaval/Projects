namespace Acquarium
{
    public interface ITank
    {
        string Name{get;set;}
		string Capacity{get;set;}
		double Feed();
    }
}
