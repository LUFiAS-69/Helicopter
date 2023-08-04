
public static class Converter
{    
    public static string ConvertMoneyToText(float _value)
    {
        string[] intValue = _value.ToString().Split(".");

        if (intValue[0].ToString().Length >= 10) // B
            return (_value / 1000000000f).ToString("0.##B");

        else if (intValue[0].ToString().Length >= 7) // M
            return (_value / 1000000f).ToString("0.##M");

        else if (intValue[0].ToString().Length >= 4) // k
            return (_value / 1000f).ToString("0.#k");

        else return _value.ToString("0.##");
    }

}
