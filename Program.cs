using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;

class MonitorEnumCallback
{
    public int Count = 0;

    public virtual unsafe BOOL Callback(HMONITOR monitor, HDC hdc, RECT* rect, LPARAM param)
    {
        Count++;
        return (BOOL)true;
    }
}

internal class Program
{
    private static unsafe void Main(string[] args)
    {
        MonitorEnumCallback closure = new();
        MONITORENUMPROC proc = new(closure.Callback);
        bool result = PInvoke.EnumDisplayMonitors(null, null, proc, IntPtr.Zero);

        if (result)
        {
            Console.WriteLine($"Found {closure.Count} monitors");
        }
        else
        {
            Console.WriteLine("EnumDisplayMonitors failed");
        }

        Console.ReadLine();
    }
}