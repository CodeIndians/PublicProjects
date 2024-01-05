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

        // start recording 
        mciSendString("open new Type waveaudio Alias recsound", "", 0, 0);
        mciSendString("record recsound", "", 0, 0);

        Console.WriteLine("recording has started, press any key to stop recording");
        Console.ReadKey();

        //save recording
        mciSendString(@"save recsound " + "C:/tmp/" + "recording" + ".wav", "", 0, 0);
        mciSendString("close recsound ", "", 0, 0);
        Console.WriteLine("Recording saved to C:/tmp/recording.wav");

        Console.WriteLine("Press any key to play the recording");
        Console.ReadKey();

        // play the recored file
        SoundPlayer player = new SoundPlayer("C:/tmp/recording.wav");
        player.PlaySync();

    }

}

