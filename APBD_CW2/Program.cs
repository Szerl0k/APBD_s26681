namespace APBD_CW2;

class Program
{
    static void Main(string[] args)
    {

        var liquid = new LiquidContainer(50.0, 5.0, 5.0, 10.0, 200.0, false);

        var gas = new GasContainer(600.0, 5.0, 5.0, 10.0, 200.0, 4.0);

        var cold = new ColdContainer(700.0, 5.0, 5.0, 10.0, 200.0, "Frozen Pizza");

        var ship = new ContainerShip(12.0, 5, 1000);

        try
        {
            ship.AddContainer(liquid);
            ship.AddContainer(cold);
            ship.AddContainer(gas);
        }
        catch (ContainerOverflowException e)
        {
            Console.WriteLine(e.Message);
        }

        Console.WriteLine(ship.ToString());

    }
}