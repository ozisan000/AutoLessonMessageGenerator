using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MessageGeneratorSystem.Generator
{
    public class GeneratorDirectory
    {
        public static string GetLocalDataPath(Assembly assembly)
        {
            string localDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string? assemblyName = assembly.GetName().Name;
            if (assemblyName == null) return "";
            Attribute? customAttr = Attribute.GetCustomAttribute(assembly, typeof(AssemblyCompanyAttribute));
            if (!(customAttr is AssemblyCompanyAttribute)) return "";
            var companyAttr = (AssemblyCompanyAttribute)customAttr;
            string company = companyAttr.Company;
            localDataPath += $"\\{company}\\{assemblyName}";
            return localDataPath;
        }

        public static string CheckAndCopyFile(string sourcePath, string checkPath)
        {
            if (File.Exists(checkPath)) 
                return "";
            try
            {
                File.Copy(sourcePath, checkPath, true);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            return "";
        }
    }
}
