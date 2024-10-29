



using ConsoleApp1;
using Newtonsoft.Json;
using System.Reflection.Emit;

public class ObjFile
{
    public string Name;
    public string LocalPath;
    public int space = 0;
    public List<ObjFile> ObjFiles = new List<ObjFile>();

    public ObjFile Add( string path , ObjFile objFile)
    {
        if (!Program.keyValuePairs.ContainsKey(path))
        {
            Program.keyValuePairs.Add(path, objFile );
            this.ObjFiles.Add(objFile );
            return objFile;
        }
        return Program.keyValuePairs[path];
    }
    public override string ToString()
    {
        Console.WriteLine();
        for (int i = 0; i < space; i++) {
            Console.Write(" ");
        }
        
        Console.Write(Name);
       
        foreach (var item in ObjFiles)
        {
            item.ToString();
        }
        return Name;
    }

}


public class Program
{
    public static Dictionary<string, ObjFile> keyValuePairs = new Dictionary<string, ObjFile>();

    static void Main(string[] args)
    {
        ObjFile ___base = new ObjFile()
        {
            LocalPath = "base_pach",
            Name = "base"
        };
        int c = 0;
        foreach (string item in FileExplorer.Paths)
        {
            string v = item;
            string[] strings = v.Split("/");
            ObjFile asdasd = ___base;
            string full = "";
            foreach (string s in strings)
            {
                full = System.IO.Path.Combine(full, s);
                ObjFile asdasdas = new ObjFile()
                {
                    LocalPath = full,
                    Name = s
                };
                asdasd = asdasd.Add(full, asdasdas);
            }  
        }
        ___base.ToString();
        File.WriteAllText("test.json", JsonConvert.SerializeObject(___base, Formatting.Indented));
        Console.ReadLine();
    }
}