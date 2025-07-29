using System;
using System.IO.Ports;

public static class ScaleReader
{
    public static string ReadWeight(string scaleModel)
    {
        string port = DetectComPort(scaleModel);
        if (string.IsNullOrEmpty(port)) return "0.00";

        try
        {
            using var sp = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);
            sp.ReadTimeout = 1000;
            sp.Open();

            string raw = sp.ReadLine();
            foreach (char c in raw)
            {
                if (char.IsControl(c)) continue;
            }

            // Extract only digits, minus, dot
            string weight = "";
            foreach (char c in raw)
            {
                if (char.IsDigit(c) || c == '.' || c == '-') weight += c;
            }

            return weight;
        }
        catch
        {
            return "0.00";
        }
    }

    private static string DetectComPort(string scaleModel)
    {
        // Eventually we could map model → COM port detection.
        // For now, assume COM3 is used for test.
        return "COM3";
    }
}
