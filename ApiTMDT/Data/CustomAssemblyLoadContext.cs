using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;
using System.Runtime.Loader;

public class CustomAssemblyLoadContext : AssemblyLoadContext
{
    public IntPtr LoadUnmanagedLibrary(string absolutePath)
    {
        if (!File.Exists(absolutePath))
        {
            throw new FileNotFoundException($"The specified DLL '{absolutePath}' was not found.");
        }

        return LoadUnmanagedDll(absolutePath);
    }

/*    protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
    {
        return LoadUnmanagedDllFromPath(unmanagedDllName);
    }
*/
    protected override Assembly Load(AssemblyName assemblyName)
    {
        throw new NotImplementedException();
    }
}

