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

        string directoryPath = "C:\\tmp";

        // Check if the directory exists
        if (!Directory.Exists(directoryPath))
        {
            // If not, create the directory
            Directory.CreateDirectory(directoryPath);
            Console.WriteLine("Directory created successfully.");
        }
        else
        {
            Console.WriteLine("Directory already exists.");
        }

        int recordingCounter = 1;

        // start recording loop
        while (!Console.KeyAvailable)
        {
            string recordingFileName = $"C:/tmp/recording{recordingCounter}.wav";

            // start recording
            mciSendString("open new Type waveaudio Alias recsound", "", 0, 0);
            mciSendString("record recsound", "", 0, 0);

            Console.WriteLine($"Recording {recordingCounter}, press any key to stop recording");
            Thread.Sleep(5000); // Record for 5 seconds

            // stop recording
            mciSendString("stop recsound", "", 0, 0);
            mciSendString($@"save recsound {recordingFileName}", "", 0, 0);
            mciSendString("close recsound", "", 0, 0);

            Console.WriteLine($"Recording {recordingCounter} saved to {recordingFileName}");

            byte[] bytes = File.ReadAllBytes(recordingFileName);

            recordingCounter++;
        }
        

        Console.WriteLine("Press any key to play the recordings");
        Console.ReadKey();

    }

}

