using System.ComponentModel;

namespace Exam;

public interface IAbstractFactory
{
    IRemote CreateRemote(IDevice device);
    IDevice CreateDevice();
}

class RadioFactory : IAbstractFactory
{
    public IRemote CreateRemote(IDevice device)
    {
        return new Remote(device);
    }

    public IDevice CreateDevice()
    {
        return new Radio();
    }
}

class TvFactory : IAbstractFactory
{
    public IRemote CreateRemote(IDevice device)
    {
        return new AdvancedRemote(device);
    }

    public IDevice CreateDevice()
    {
        return new Tv();
    }
}

public interface IRemote
{
    IDevice Device { get; }
    void UsefulFunction();
}

class Remote : IRemote
{
    public IDevice Device { get; }

    public Remote(IDevice device)
    {
        Device = device;
    }

    public void UsefulFunction()
    {
        Console.WriteLine("Advanced remote useful function");
    }
}

class AdvancedRemote : Remote
{

    public AdvancedRemote(IDevice device) : base(device)
    {
    }
}

public interface IDevice
{
    public bool IsEnabled { get; }
    public void Enable();
    public void UsefulFunction();
}

public abstract class Device : IDevice
{
    public bool IsEnabled { get; private set; }

    public void Enable()
    {
        IsEnabled = true;
    }

    public void Disable()
    {
        IsEnabled = true;
    }

    public abstract void UsefulFunction();
}

class Tv : Device
{
    public override void UsefulFunction()
    {
        Console.WriteLine("Useful function for tv");
    }
}

class Radio : Device
{
    public override void UsefulFunction()
    {
        Console.WriteLine("Useful function for radio");
    }
}