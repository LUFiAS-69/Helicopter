using System;
using System.Threading.Tasks;

public static class AsyncHelperLUFI
{
    public static async void DoThisAsynchronously(Action action)
    {
        await Task.Run(() =>
        {
            action();
        });
    }

}
