using System.Collections;
using System.Runtime.CompilerServices;
using Installer.Common.localization;

namespace Installer.Common.Framework;

public static class CustomExceptions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T CheckArgumentNull<T>(T arg, string name) where T : class
    {
        return arg is null ? throw new ArgumentNullException(name) : arg;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string CheckArgumentNullOrEmpty(string arg, string name)
    {
        return arg switch
        {
            null => throw new ArgumentNullException(name),
            "" => throw new ArgumentEmptyException(name),
            _ => arg
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T CheckArgumentNullOrEmpty<T>(T arg, string name) where T : IList
    {
        if (arg is null)
            throw new ArgumentNullException(name);
        if (arg.Count == 0)
            throw new ArgumentEmptyException(name);

        return arg;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CheckGameFileNotFoundException(string fullName)
    {
        CheckArgumentNullOrEmpty(fullName, "fullName");
        if (!File.Exists(fullName))
            throw new GameFileNotFoundException(fullName);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CheckPackageFileNotFoundException(string fullName)
    {
        CheckArgumentNullOrEmpty(fullName, "fullName");
        if (!File.Exists(fullName))
            throw new PackageFileNotFoundException(fullName);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WrongMagicException(string resourcesFile)
    {
        CheckArgumentNullOrEmpty(resourcesFile, "magic");

        using FileStream stream = File.OpenRead(resourcesFile);
        using BinaryReader br = new(stream);
        long magic = br.ReadInt64();
        if (magic != 0x494949584646524c)
            throw new PackageMagicException(Localization.Localizer.Get("Warning.WrongPackage"));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CheckDirectoryNotFoundException(string fullName)
    {
        CheckArgumentNullOrEmpty(fullName, "fullName");
        if (!Directory.Exists(fullName))
            throw new DirectoryNotFoundException(fullName);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T CheckArgumentOutOfRangeException<T>(T value, string name, T minValue, T maxValue) where T : IComparable<T>
    {
        if (value.CompareTo(minValue) < 0 || value.CompareTo(maxValue) > 0)
            throw new ArgumentOutOfRangeException(name, value, $@"Value of the argument ({name} = {value}) is out of the allowable range: ({minValue}~{maxValue}).");
        return value;
    }
}