namespace tryme
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             *Do not forget to add the folder "tohack" to your desktop and put some files in it before running the program. 
             *BEWARE: the program will encrypt all files in the "tohack" folder and save the encrypted data in "vault.bin" on your desktop.
             * 
             */

            bool deleteFiles = true; // Set to true if you want to delete the original files after encryption

            string filePath = Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

            string toHackPath = Path.Combine(filePath, "tohack");
            string vaultPath = Path.Combine(filePath, "vault.bin");
            string decryptedPath = Path.Combine(filePath, "unhacked");

            Console.WriteLine("Start...");

            Crypt crypt = new Crypt(toHackPath, deleteFiles);

            crypt.EncryptFolder(vaultPath);

            Console.WriteLine("Done!");

            Skull skull = new Skull(1000, 25);

            Console.WriteLine("Enter Key");

            crypt.DecryptFolder(vaultPath, decryptedPath);

            Console.WriteLine("Done!");
        }
    }
}
