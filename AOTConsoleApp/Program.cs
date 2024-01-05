using System.Media;
using System.Runtime.InteropServices;

class Program
{

    [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    private static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

    static void Main()
    {
        Console.WriteLine("Testing Microsoft.Data.Sqlite, NAudio, and Gma.System.MouseKeyHook");

        // Microsoft.Data.Sqlite
        using (var connection = new Microsoft.Data.Sqlite.SqliteConnection("Data Source=test.db"))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS SampleTable (Id INTEGER PRIMARY KEY, Name TEXT)";
                command.ExecuteNonQuery();
            }
        }

        SoundPlayer player = new SoundPlayer("C:/tmp/test.wav");
        player.PlaySync();

        //// NAudio
        //WaveOutEvent waveOut = new WaveOutEvent();
        //AudioFileReader audioFile = new AudioFileReader("test.wav");
        //waveOut.Init(audioFile);
        //waveOut.Play();

        //// Gma.System.MouseKeyHook
        //IKeyboardMouseEvents globalHook = Hook.GlobalEvents();
        //globalHook.KeyPress += (sender, e) =>
        //{
        //    Console.WriteLine($"Key Pressed: {e.KeyChar}");
        //};

        //Console.WriteLine("Press any key to exit.");
        //Console.ReadKey();
        //Console.ReadKey();

        //// Clean up
        //globalHook.Dispose();
        //waveOut.Stop();
        //waveOut.Dispose();

    }

}

