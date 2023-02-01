using System.Collections;
using System.Runtime.CompilerServices;
using Installer.Common.Localizations;

namespace Installer.Common.Framework;

public static class Exceptions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Exception CreateException(string message, params object[] args)
    {
        return new Exception(string.Format(message, args));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Exception CreateArgumentException(string paramName, string message, params object[] args)
    {
        return new ArgumentException(string.Format(message, args), paramName);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T CheckArgumentNull<T>(T arg, string name) where T : class
    {
        return ReferenceEquals(arg, null) ? throw new ArgumentNullException(name) : arg;
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
        if (ReferenceEquals(arg, null))
            throw new ArgumentNullException(name);
        if (arg.Count == 0)
            throw new ArgumentEmptyException(name);

        return arg;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string CheckGameFileNotFoundException(string fullName)
    {
        CheckArgumentNullOrEmpty(fullName, "fullName");
        if (!File.Exists(fullName))
            throw new GameFileNotFoundException(fullName);

        return fullName;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string CheckPackageFileNotFoundException(string fullName)
    {
        CheckArgumentNullOrEmpty(fullName, "fullName");
        if (!File.Exists(fullName))
            throw new PackageFileNotFoundException(fullName);

        return fullName;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WrongMagicException(string resourcesFile)
    {
        CheckArgumentNullOrEmpty(resourcesFile, "magic");

        using FileStream stream = File.OpenRead(resourcesFile);
        using BinaryReader br = new(stream);
        long magic = br.ReadInt64();
        if (magic != 0x494949584646524c)
            throw new PackageMagicException(Localization.Instance.WrongPackage);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string CheckDirectoryNotFoundException(string fullName)
    {
        CheckArgumentNullOrEmpty(fullName, "fullName");
        if (!Directory.Exists(fullName))
            throw new DirectoryNotFoundException(fullName);

        return fullName;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T CheckArgumentOutOfRangeException<T>(T value, string name, T minValue, T maxValue) where T : IComparable<T>
    {
        if (value.CompareTo(minValue) < 0 || value.CompareTo(maxValue) > 0)
            throw new ArgumentOutOfRangeException(name, value, $@"Value of the argument ({name} = {value}) is out of the allowable range: ({minValue}~{maxValue}).");
        return value;
    }
}