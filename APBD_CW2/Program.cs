namespace APBD_CW2;

class Program
{
    static void Main(string[] args)
    {
        var lqContainer = new LiquidContainer(0.0, 10.1, 10.1, 5.0, 100, true);
        
        lqContainer.Load(100);
        lqContainer.Load(50);
        
        var lqContainerNoH = new LiquidContainer(0.0, 10.1, 10.1, 5.0, 100, false);
        
        lqContainerNoH.Load(100);
        lqContainerNoH.Load(50);
        
    }
}