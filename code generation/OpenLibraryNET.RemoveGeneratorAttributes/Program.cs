using Mono.Cecil;
using System;
using System.Linq;

namespace OpenLibraryNET.RemoveGeneratorAttributes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                throw new ArgumentException("Too few arguments! Pattern: $(Path)OpenLibraryNET.RemoveGeneratorAttributes.exe $(PathToOpenLibraryNET.dll) $(PathToOpenLibraryNET.GeneratorAttributes.dll)");
            }
            var assemblyPath = args[0];
            var attributeAssemblyPath = args[1];
            var assembly = AssemblyDefinition.ReadAssembly(assemblyPath);
            var attributeAssembly = AssemblyDefinition.ReadAssembly(attributeAssemblyPath);


            foreach (var type in assembly.MainModule.Types)
            {
                RemoveAttributes(type, attributeAssembly);
                //var attributesToRemove = type.CustomAttributes.Where(a => Test(a, attributeAssembly)).ToList();


                //foreach (var attr in attributesToRemove)
                //{
                //    type.CustomAttributes.Remove(attr);
                //}
            }

            assembly.Write(assemblyPath + ".modified");
        }

        private static void RemoveAttributes(TypeDefinition typeDef, AssemblyDefinition attributeAssembly)
        {
            var attributesToRemove = typeDef.CustomAttributes.Where(a => Test(a, attributeAssembly)).ToList();

            foreach (var attr in attributesToRemove)
            {
                typeDef.CustomAttributes.Remove(attr);
            }

            if (!typeDef.HasNestedTypes)
            {
                return;
            }

            foreach (var nestedType in typeDef.NestedTypes)
            {
                RemoveAttributes(nestedType, attributeAssembly);
            }
        }

        private static bool Test(CustomAttribute ca, AssemblyDefinition assembly)
        {
            return ca.AttributeType.Scope.Name == assembly.Name.Name;
        }
    }
}
