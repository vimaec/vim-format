using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Vim.Format
{
    // TODO: these are some question that I would like to address
    // https://stackoverflow.com/questions/1103495/is-there-a-proper-way-to-read-csv-files
    // https://docs.microsoft.com/en-us/dotnet/visual-basic/developing-apps/programming/drives-directories-files/how-to-read-from-comma-delimited-text-files
    // https://stackoverflow.com/questions/1050112/how-to-read-a-csv-file-into-a-net-datatable?noredirect=1&lq=1
    // https://www.codeproject.com/Articles/11698/A-Portable-and-Efficient-Generic-Parser-for-Flat-F
    // https://stackoverflow.com/questions/1898/csv-file-imports-in-net
    // https://stackoverflow.com/questions/217902/reading-writing-an-ini-file
    // https://www.codeproject.com/Tips/771772/A-Simple-and-Efficient-INI-File-Reader-in-Csharp

    public static unsafe class Util
    {
        public static int GetPositiveHashCode(this object obj)
            => obj.GetHashCode() & 0xfffffff;

        /// <summary>
        /// Returns an 8 character lowercase alphanumeric string representing the hexadecimal format of the given value's hash code.
        /// </summary>
        public static string ToHexHash<T>(this T value)
            => $"{value.GetHashCode():x}";

        public static string ToIdentifier(this string self)
            => string.IsNullOrEmpty(self) ? "_" : self.ReplaceNonAlphaNumeric("_");

        public static string ReplaceNonAlphaNumeric(this string self, string replace)
            => Regex.Replace(self, "[^a-zA-Z0-9]", replace);

        // https://stackoverflow.com/questions/4823467/using-linq-to-find-the-cumulative-sum-of-an-array-of-numbers-in-c-sharp/

        #region LINQ to find the cumulative sum of an array

        public static IEnumerable<U> Accumulate<T, U>(this IEnumerable<T> self, U init, Func<U, T, U> f)
        {
            foreach (var x in self)
                yield return init = f(init, x);
        }

        public static IEnumerable<T> Accumulate<T>(this IEnumerable<T> self, Func<T, T, T> f)
            => self.Accumulate(default, f);

        public static IEnumerable<double> PartialSums(this IEnumerable<double> self)
            => self.Accumulate((x, y) => x + y);

        public static IEnumerable<int> PartialSums(this IEnumerable<int> self)
            => self.Accumulate((x, y) => x + y);

        public static bool IsNullOrEmpty(this IEnumerable obj)
        {
            if (obj == null)
                return true;

            foreach (var _ in obj)
                return false;

            return true;
        }

        #endregion

        #region Reflection to create a DataTable from a Class?

        #endregion

        /// <summary>
        /// Returns all the values in a data column converted into the specified type if possible.
        /// https://stackoverflow.com/questions/2916583/how-to-get-a-specific-column-value-from-a-datatable
        /// </summary>
        public static IEnumerable<T> ColumnValues<T>(this DataColumn self)
            => self.Table.Select().Select(dr => (T)Convert.ChangeType(dr[self], typeof(T)));

        public static string IfEmpty(this string self, string other)
            => string.IsNullOrWhiteSpace(self) ? other : self;

        public static string ElidedSubstring(this string self, int start, int length, int max)
            => (length > max) ? self.Substring(start, max) + "..." : self.Substring(start, length);

        public static IEnumerable<T> DifferentFromPrevious<T>(this IEnumerable<T> self)
        {
            var first = true;
            var prev = default(T);
            foreach (var x in self)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    if (!x.Equals(prev))
                        yield return x;
                }

                prev = x;
            }
        }

        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> self)
            => self.Where(x => x != null);

        /// <summary>
        /// The normalized DateTime format, suitable for inclusion in a filename.
        /// </summary>
        public const string NormalizedDateTimeFormat = "yyyy-MM-dd_HH-mm-ss";

        /// <summary>
        /// Returns the normalized representation of the given DateTime.
        /// </summary>
        public static string ToNormalizedString(this DateTime dateTime)
            => dateTime.ToString(NormalizedDateTimeFormat);

        /// <summary>
        /// Returns the current date-time in a format appropriate for appending to files.
        /// </summary>
        public static string GetTimeStamp()
            => DateTime.Now.ToNormalizedString();

        /// <summary>
        /// Returns the time stamped filename and extension.
        /// </summary>
        public static string TimeStampedFileName(this string path)
            => Path.Combine(Path.GetDirectoryName(path),
                $"{Path.GetFileNameWithoutExtension(path)}-{GetTimeStamp()}{Path.GetExtension(path)}");

        public static string CopyToFolder(this string path, string dir, bool dontCreate = false)
        {
            if (!dontCreate)
                Directory.CreateDirectory(dir);
            var newPath = Path.Combine(dir, Path.GetFileName(path));
            File.Copy(path, newPath);
            return newPath;
        }

        public static string MoveToFolder(this string path, string dir, bool dontCreate = false)
        {
            if (!dontCreate)
                Directory.CreateDirectory(dir);
            var newPath = Path.Combine(dir, Path.GetFileName(path));
            File.Move(path, newPath);
            return newPath;
        }

        public static void CopyDirectory(string sourceDirectory, string targetDirectory)
        {
            var diSource = new DirectoryInfo(sourceDirectory);
            var diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (var fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (var diSourceSubDir in source.GetDirectories())
            {
                var nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        /// <summary>
        /// Returns the size of the directory in bytes.
        /// </summary>
        public static long GetDirectorySizeInBytes(string directoryPath)
            => GetAllFiles(directoryPath).Aggregate(0L, (acc, path) =>
            {
                var fileInfo = new FileInfo(path);
                return fileInfo.Exists ? acc + fileInfo.Length : acc;
            });

        /// <summary>
        /// Given a file name, returns a new file name that has the parent directory name prepended to it.
        /// </summary>
        public static string GetFileNameWithParentDirectory(this string file, string sep = "-")
        {
            var baseName = Path.GetFileName(file);
            var dir = Path.GetDirectoryName(file);
            var dirName = new DirectoryInfo(dir).Name;
            return $"{dirName}{sep}{baseName}";
        }

        public static Action ToAction<R>(this Func<R> f)
            => () => f();

        public static Action<A0> ToAction<A0, R>(this Func<A0, R> f)
            => x => f(x);

        public static Action<A0, A1> ToAction<A0, A1, R>(Func<A0, A1, R> f)
            => (x, y) => f(x, y);

        public static Func<bool> ToFunction(this Action action)
            => () =>
            {
                action();
                return true;
            };

        public static void TimeIt(this Action action, string label = "")
            => TimeIt(action.ToFunction(), label);

        // public static Disposer TimeIt(string label = "")
        // {
        //     Console.WriteLine($"Starting timing {label}");
        //     var sw = Stopwatch.StartNew();
        //     return Disposer(() => sw.OutputTimeElapsed(label));
        // }

        public static string PrettyPrintTimeElapsed(this Stopwatch sw)
            => $"{sw.Elapsed.Minutes}:{sw.Elapsed.Seconds}.{sw.Elapsed.Milliseconds}";

        public static void OutputTimeElapsed(this Stopwatch sw, string label)
            => Console.WriteLine($"{label}: time elapsed {sw.PrettyPrintTimeElapsed()}");

        public static string MSecToSecondsString(long msec)
            => $"{msec / 1000}.{msec % 1000}";

        public static T TimeIt<T>(this Func<T> function, string label = "")
        {
            var sw = Stopwatch.StartNew();
            var r = function();
            sw.OutputTimeElapsed(label);
            return r;
        }

        /// <summary>
        /// Returns all instance fields, public and private.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IEnumerable<FieldInfo> GetAllFields(this Type self)
            => self.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        /// <summary>
        /// Ignores the property or the field when they are being enumerated from a given object.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
        public class IgnoreInReflectedListAttribute : Attribute { }

        /// <summary>
        /// Converts fields to a dictionary of string/object pair.
        /// </summary>
        public static IDictionary<string, object> FieldsToDictionary(this object self)
            => self.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Where(fi => !fi.GetCustomAttributes<IgnoreInReflectedListAttribute>().Any())
                .ToDictionary(fi => fi.Name, fi => fi.GetValue(self));

        /// <summary>
        /// Converts properties to a dictionary of string/object pair.
        /// </summary>
        public static IDictionary<string, object> PropertiesToDictionary(this object self)
            => self.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(pi => !pi.GetCustomAttributes<IgnoreInReflectedListAttribute>().Any())
                .ToDictionary(pi => pi.Name, fi => fi.GetValue(self));

        /// <summary>
        /// Converts properties to an enumerable of strings.
        /// </summary>
        public static IEnumerable<string> PropertiesToStrings(this object self)
            => self.PropertiesToDictionary().Select(kv => $"{kv.Key}: {kv.Value}");

        /// <summary>
        /// Returns a shallow clone of the given object's properties.
        /// </summary>
        public static T ShallowClone<T>(T obj) where T : class, new()
        {
            var type = obj.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var clonedObj = new T();

            foreach (var property in properties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    var value = property.GetValue(obj);
                    property.SetValue(clonedObj, value);
                }
            }

            return clonedObj;
        }

        /// <summary>
        /// Returns true if the type is a "plain old data" type (is a struct type that contains no references).
        /// This means that we should be able to create pointers to the type, and copying
        /// arrays of them into buffers makes sense.
        /// </summary>
        public static bool ContainsNoReferences(this Type t)
            => t.IsPrimitive || t.GetAllFields().Select(f => f.FieldType).All(ContainsNoReferences);

        /// <summary>
        /// Given a dictionary looks up the key, or uses the function to add to the dictionary, and returns that result.
        /// </summary>
        public static V GetOrCompute<K, V>(this IDictionary<K, V> self, K key, Func<K, V> func)
        {
            if (self.ContainsKey(key))
                return self[key];
            var value = func(key);
            self.Add(key, value);
            return value;
        }

        /// <summary>
        /// Given a dictionary looks up the key, or uses the function to add to the dictionary, and returns that result.
        /// </summary>
        public static V GetOrCompute<K, V, TContext>(this IDictionary<K, V> self, TContext context, K key,
            Func<TContext, K, V> func)
        {
            if (self.ContainsKey(key))
                return self[key];
            var value = func(context, key);
            self.Add(key, value);
            return value;
        }

        /// <summary>
        /// Returns a value if found in the dictionary, or default if not present.
        /// </summary>
        public static V GetOrDefault<K, V>(this IDictionary<K, V> self, K key, V defaultValue)
            => self.ContainsKey(key) ? self[key] : defaultValue;

        /// <summary>
        /// Returns a value if found in the dictionary, or default if not present.
        /// </summary>
        public static V GetOrDefaultAllowNulls<K, V>(this IDictionary<K, V> self, K key, V defaultValue) where K : class
            => key != null && self.ContainsKey(key) ? self[key] : defaultValue;

        /// <summary>
        /// Returns a value if found in the dictionary, or default if not present.
        /// </summary>
        public static V GetOrDefault<K, V>(this IDictionary<K, V> self, K key)
            => self.GetOrDefault(key, default);

        /// <summary>
        /// Returns a value if found in the dictionary, or default if not present.
        /// </summary>
        public static V GetOrDefaultAllowNulls<K, V>(this IDictionary<K, V> self, K key) where K : class
            => self.GetOrDefaultAllowNulls(key, default);

        /// <summary>
        /// Adds a key and value to a dictionary if the key is not already present, otherwise does nothing and returns false.
        /// </summary>
        public static bool TryAdd<K, V>(this IDictionary<K, V> self, K key, V value)
        {
            if (self.ContainsKey(key)) return false;
            self.Add(key, value);
            return true;
        }

        /// <summary>
        /// Returns a value if found in the dictionary, or default if not present.
        /// </summary>
        public static void AddOrReplace<K, V>(this IDictionary<K, V> self, K key, V value)
        {
            if (self.ContainsKey(key))
                self[key] = value;
            else
                self.Add(key, value);
        }

        /// <summary>
        /// Returns a value if found in the dictionary, or default if not present.
        /// </summary>
        public static void AddIfNotPresent<K, V>(this IDictionary<K, V> self, K key, V value)
        {
            if (!self.ContainsKey(key))
                self.Add(key, value);
        }

        /// <summary>
        /// Updates a value in the dictionary using the provided function, passing default
        /// if no value is present.
        /// </summary>
        public static void AddOrUpdate<K, V>(this IDictionary<K, V> self, K key, Func<V, V> func)
        {
            if (self.ContainsKey(key))
                self[key] = func(self[key]);
            else
                self.Add(key, func(default));
        }

        /// <summary>
        /// Merges two dictionaries together, replacing values in the first with the second if the keys overlap
        /// </summary>
        public static Dictionary<K, V> Merge<K, V>(this IDictionary<K, V> self, IDictionary<K, V> other)
        {
            var r = new Dictionary<K, V>(self);
            foreach (var kv in other)
                r.AddOrUpdate(kv.Key, v => kv.Value);
            return r;
        }

        /// <summary>
        /// Computes the size of the given managed type. Slow, but reliable. Does not give the same result as Marshal.SizeOf
        /// https://stackoverflow.com/questions/8173239/c-getting-size-of-a-value-type-variable-at-runtime
        /// https://stackoverflow.com/questions/3804638/whats-the-size-of-this-c-sharp-struct?noredirect=1&lq=1
        /// NOTE: this is not the same as what is the distance between these types in an array. That varies depending on alignment.
        /// </summary>
        /// note: 0 references
        //public static int ComputeSizeOf(Type t)
        //{
        //    // all this just to invoke one opcode with no arguments!
        //    var method = new DynamicMethod("ComputeSizeOfImpl", typeof(int), Type.EmptyTypes, typeof(Util), false);
        //    var gen = method.GetILGenerator();
        //    gen.Emit(OpCodes.Sizeof, t);
        //    gen.Emit(OpCodes.Ret);
        //    var func = (Func<int>)method.CreateDelegate(typeof(Func<int>));
        //    return func();
        //}

        /// <summary>
        /// Returns the size of the managed type.
        /// </summary>
        public static int SizeOf(this Type t)
            => Marshal.SizeOf(t);

        /// <summary>
        /// Returns the size of the managed type.
        /// </summary>
        public static int SizeOf<T>()
            => SizeOf(typeof(T));

        /// <summary>
        /// Copies the specified number of bytes between memory locations.
        /// Alternatives include: Marshal.Copy, and PInvoke to CopyMem
        /// https://stackoverflow.com/questions/15975972/copy-data-from-from-intptr-to-intptr
        /// </summary>
        public static void MemoryCopy(IntPtr source, IntPtr dest, long size)
            => MemoryCopy(source.ToPointer(), dest.ToPointer(), size);

        /// <summary>
        /// Copies the specified number of bytes between memory locations.
        /// Alternatives include: Marshal.Copy, and PInvoke to CopyMem
        /// https://stackoverflow.com/questions/15975972/copy-data-from-from-intptr-to-intptr
        /// </summary>
        public static void MemoryCopy(void* source, void* dest, long size)
            => Buffer.MemoryCopy(source, dest, size, size);

        public static void MemoryCopy<T>(IntPtr src, T[] dest, long numBytes) where T : unmanaged
            => MemoryCopy(src.ToPointer(), dest, numBytes);

        public unsafe static void MemoryCopy<T>(void* src, T[] dest, long numBytes) where T : unmanaged
        {
            fixed (void* p = dest)
                Buffer.MemoryCopy(src, p, sizeof(T) * dest.Length, numBytes);
        }

        /// <summary>
        /// Provides access to a byte array as a stream.
        /// https://docs.microsoft.com/en-us/dotnet/api/system.io.memorystream?view=netframework-4.7.2
        /// </summary>
        public static MemoryStream ToMemoryStream(this byte[] self)
            => new MemoryStream(self);

        /// <summary>
        /// Reads all bytes from a stream
        /// https://stackoverflow.com/questions/1080442/how-to-convert-an-stream-into-a-byte-in-c
        /// </summary>
        public static byte[] ReadAllBytes(this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Returns true if the type self is an instance of the given generic type
        /// </summary>
        public static bool InstanceOfGenericType(this Type self, Type genericType)
            => self.IsGenericType && self.GetGenericTypeDefinition() == genericType;

        /// <summary>
        /// Returns true if the type self is an instance of the given generic interface, or implements the interface
        /// </summary>
        public static bool InstanceOfGenericInterface(this Type self, Type ifaceType)
            => self.InstanceOfGenericType(ifaceType)
               || self.GetInterfaces().Any(i => i.InstanceOfGenericType(ifaceType));

        /// <summary>
        /// https://stackoverflow.com/questions/4963160/how-to-determine-if-a-type-implements-an-interface-with-c-sharp-reflection
        /// </summary>
        public static bool ImplementsInterface(this Type self, Type ifaceType)
            => ifaceType.IsAssignableFrom(self) || self.GetInterfaces().Contains(ifaceType);

        public static bool ImplementsInterface<TInterface>(this Type self)
            => self.ImplementsInterface(typeof(TInterface));

        /// <summary>
        /// Returns true if the type implements IList with a generic parmaeter.
        /// </summary>
        public static bool ImplementsIList(this Type t)
            => t.InstanceOfGenericInterface(typeof(IList<>));

        /// <summary>
        /// Returns true if the source type can be cast to doubles.
        /// </summary>
        public static bool CanCastToDouble(this Type typeSrc)
            => typeSrc.IsPrimitive
               && typeSrc != typeof(char)
               && typeSrc != typeof(decimal)
               && typeSrc != typeof(bool);

        public static FileStream OpenFileStreamWriting(string filePath, int bufferSize)
            => new FileStream(filePath, FileMode.Open, FileAccess.Write, FileShare.None, bufferSize);

        public static FileStream OpenFileStreamReading(string filePath, int bufferSize)
            => new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize);

        /// <summary>
        /// The official Stream.Read iis a PITA, because it could return anywhere from 0 to the number of bytes
        /// requested, even in mid-stream. This call will read everything it can until it reaches
        /// the end of the stream of "count" bytes.
        /// </summary>
        public static int SafeRead(this Stream stream, byte[] buffer, int offset, int count)
        {
            var r = stream.Read(buffer, offset, count);
            if (r != 0 && r < count)
            {
                // We didn't read everything, so let's keep trying until we get a zero
                while (true)
                {
                    var tmp = stream.Read(buffer, r, count - r);
                    if (tmp == 0)
                        break;
                    r += tmp;
                }
            }

            return r;
        }

        public static bool NaiveSequenceEqual<T>(T[] buffer1, T[] buffer2) where T : IEquatable<T>
        {
            if (buffer1 == buffer2) return true;
            if (buffer1 == null || buffer2 == null) return false;
            if (buffer1.Length != buffer2.Length) return false;
            for (var i = 0; i < buffer1.Length; ++i)
            {
                if (!buffer1[i].Equals(buffer2[i]))
                    return false;
            }

            return true;
        }

        public static bool SequenceEqual<T>(T[] buffer1, T[] buffer2) where T : IEquatable<T> => (buffer1 == buffer2) ||
            !(buffer1 != null && buffer2 != null) || buffer1.AsSpan().SequenceEqual(buffer2.AsSpan());

        public class ArrayEqualityComparer<T> : IEqualityComparer<T[]> where T : IEquatable<T>
        {
            public bool Equals(T[] x, T[] y)
                => NaiveSequenceEqual(x, y);

            public int GetHashCode(T[] obj) => obj.GetHashCode();
        }

        // From: https://stackoverflow.com/a/3928856
        public static bool DictionaryEqual<TKey, TValue>(
            this IDictionary<TKey, TValue> first,
            IDictionary<TKey, TValue> second,
            IEqualityComparer<TValue> valueComparer = null)
        {
            if (first == second) return true;
            if ((first == null) || (second == null)) return false;
            if (first.Count != second.Count) return false;

            valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;

            foreach (var kvp in first)
            {
                if (!second.TryGetValue(kvp.Key, out var secondValue)) return false;
                if (!valueComparer.Equals(kvp.Value, secondValue)) return false;
            }
            return true;
        }

        // Improved answer over:
        // https://stackoverflow.com/questions/211008/c-sharp-file-management
        // https://stackoverflow.com/questions/1358510/how-to-compare-2-files-fast-using-net?noredirect=1&lq=1
        // Should be faster than the SHA version, with no chance of a mismatch.
        public static bool CompareFiles(string filePath1, string filePath2)
        {
            if (!File.Exists(filePath2) || !File.Exists(filePath2))
                return false;

            if (new FileInfo(filePath1).Length != new FileInfo(filePath2).Length)
                return false;

            // Default buffer size of File stream * 16.
            // Profiling revealed this to be faster than the default
            const int bufferSize = 4096 * 16;
            var buf1 = new byte[bufferSize];
            var buf2 = new byte[bufferSize];

            // open both for reading
            using (var stream1 = OpenFileStreamReading(filePath1, bufferSize))
            using (var stream2 = OpenFileStreamReading(filePath1, bufferSize))
            {
                // Read buffers (need to be careful because of contract of stream.Read)
                var tmp1 = stream1.SafeRead(buf1, 0, bufferSize);
                var tmp2 = stream2.SafeRead(buf2, 0, bufferSize);

                // Check that we read the same size
                if (tmp1 != tmp2) return false;

                // Compare the bytes
                for (var i = 0; i < tmp1; ++i)
                {
                    if (buf1[i] != buf2[i])
                        return false;
                }
            }

            return true;
        }

        public static bool NaiveCompareFiles(string filePath1, string filePath2)
            => SequenceEqual(File.ReadAllBytes(filePath1), File.ReadAllBytes(filePath2));

        public static byte[] FileSHA256(string filePath)
            => SHA256.Create().ComputeHash(File.OpenRead(filePath));

        public static bool HashCompareFiles(string filePath1, string filePath2)
            => SequenceEqual(FileSHA256(filePath1), FileSHA256(filePath2));

        /// <summary>
        /// Executes an action capturing the console output.
        /// Improved answer over:
        /// https://stackoverflow.com/questions/11911660/redirect-console-writeline-from-windows-application-to-a-string
        /// </summary>
        public static string RunCodeReturnConsoleOut(Action action)
        {
            var originalConsoleOut = Console.Out;
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);
                try
                {
                    action();
                    writer.Flush();
                    return writer.GetStringBuilder().ToString();
                }
                finally
                {
                    Console.SetOut(originalConsoleOut);
                }
            }
        }

        /// <summary>
        /// Perform some action when the current process shuts down.
        /// </summary>
        public static void OnShutdown(Action action)
            => AppDomain.CurrentDomain.ProcessExit += (object sender, EventArgs e) => action();

        /// <summary>
        /// Closes a process if it isin't null and hasn't already exited.
        /// </summary>
        /// <param name="process"></param>
        public static void SafeClose(this Process process)
        {
            if (process != null && !process.HasExited)
                process.CloseMainWindow();
        }

        /// <summary>
        /// Given a memory mapped file, creates a buffere, and reads data into it.
        /// </summary>
        public static byte[] ReadBytes(this MemoryMappedFile mmf, long offset, int count)
            => mmf.ReadBytes(offset, new byte[count]);

        /// <summary>
        /// Given a memory mapped file and a buffer fills it with data from the given offset.
        /// </summary>
        public static byte[] ReadBytes(this MemoryMappedFile mmf, long offset, byte[] buffer)
        {
            using (var view = mmf.CreateViewStream(offset, buffer.Length, MemoryMappedFileAccess.Read))
            {
                view.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        /// <summary>
        /// Outputs information about the current memory state to the passed textWriter, or standard out if null.
        /// </summary>
        public static void SnapshotMemory(TextWriter tw = null)
        {
            var process = Process.GetCurrentProcess();
            tw = tw ?? Console.Out;
            if (process != null)
            {
                var memsize = process.PrivateMemorySize64 >> 10;
                tw.WriteLine("Private memory: " + memsize.ToString("n"));
                memsize = process.WorkingSet64 >> 10;
                tw.WriteLine("Working set: " + memsize.ToString("n"));
                memsize = process.PeakWorkingSet64 >> 10;
                tw.WriteLine("Peak working set: " + memsize.ToString("n"));
            }
        }

        public static T ToStruct<T>(this byte[] bytes) where T : struct
            => bytes.ToStructs<T>()[0];

        public static T ToStruct<T>(this Span<byte> span) where T : struct
            => span.ToStructs<T>()[0];

        public static Memory<T> ToMemory<T>(this Span<T> span)
            => new Memory<T>(span.ToArray());

        public static Span<T> Cast<T>(this Span<byte> span) where T : struct
            => MemoryMarshal.Cast<byte, T>(span);

        public static Span<byte> AsByteSpan<T>(this Span<T> span) where T : struct
            => MemoryMarshal.AsBytes(span);

        public static Span<byte> AsByteSpan<T>(this Memory<T> memory) where T : struct
            => memory.Span.AsByteSpan();

        public static byte[] ToBytes<T>(this Span<T> span) where T : struct
            => span.AsByteSpan().ToArray();

        public static byte[] ToBytes<T>(this Memory<T> memory) where T : struct
            => memory.Span.ToBytes();

        public static Memory<T> ToMemory<T>(this T[] data) where T : struct
            => new Memory<T>(data);

        public static T[] ToStructs<T>(this Span<byte> span) where T : struct
            => span.Cast<T>().ToArray();

        public static T[] ToStructs<T>(this byte[] bytes) where T : struct
            => new Span<byte>(bytes).ToStructs<T>();

        public static string ToUtf8(this byte[] bytes)
            => Encoding.UTF8.GetString(bytes);

        public static string ToAscii(this byte[] bytes)
            => Encoding.ASCII.GetString(bytes);

        public static byte[] ToBytesUtf8(this string s)
            => Encoding.UTF8.GetBytes(s);

        public static byte[] ToBytesAscii(this string s)
            => Encoding.ASCII.GetBytes(s);

        public static string ToHex(this byte[] bytes, bool upperCase = false)
            => string.Join("", bytes.Select(b => b.ToString(upperCase ? "X2" : "x2")));

        public static string Base64ToHex(this string base64)
            => Convert.FromBase64String(base64).ToHex();

        public static string ToBase64(this byte[] bytes)
            => Convert.ToBase64String(bytes);

        public static string ToBitConverterLowerInvariant(this byte[] bytes)
            => BitConverter.ToString(bytes).Replace("-", string.Empty).ToLowerInvariant();

        /// <summary>
        /// Useful quick test to assure that we can create a file in the folder and write to it.
        /// </summary>
        public static void TestWrite(this DirectoryInfo di)
            => TestWrite(di.FullName);

        public static bool HasWildCard(string filePath)
            => filePath.Contains("*") || filePath.Contains("?");

        /// <summary>
        /// Returns all the files in the given directory and its subdirectories,
        /// or just returns the passed file.
        /// </summary>
        public static IEnumerable<string> GetAllFiles(string path, string searchPattern = "*")
            => GetFiles(path, searchPattern, true);

        /// <summary>
        /// Returns all the files in the given directory and optionally its subdirectories,
        /// or just returns the passed file.
        /// </summary>
        public static IEnumerable<string> GetFiles(string path, string searchPattern = "*", bool recurse = false)
            => File.Exists(path)
                ? Enumerable.Repeat(path, 1)
                : Directory.Exists(path)
                    ? Directory.EnumerateFiles(path, searchPattern,
                        recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                    : Array.Empty<string>();

        /// <summary>
        /// Deletes the file or directory (recursively) at the given path.
        /// </summary>
        public static void Delete(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
            if (Directory.Exists(path))
                Directory.Delete(path, true);
        }

        /// <summary>
        /// Deletes all contents in a folder
        /// https://stackoverflow.com/questions/1288718/how-to-delete-all-files-and-folders-in-a-directory
        /// </summary>
        public static void DeleteFolderContents(string folderPath)
        {
            var di = new DirectoryInfo(folderPath);
            foreach (var dir in di.EnumerateDirectories().AsParallel())
                DeleteFolderAndAllContents(dir.FullName);
            foreach (var file in di.EnumerateFiles().AsParallel())
                file.Delete();
        }

        /// <summary>
        /// Deletes everything in a folder and then the folder.
        /// </summary>
        public static void DeleteFolderAndAllContents(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                return;
            DeleteFolderContents(folderPath);
            Directory.Delete(folderPath);
        }

        /// <summary>
        /// Creates a directory if needed, or clears all of its contents otherwise
        /// </summary>
        public static string CreateDirectory(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            return dirPath;
        }

        /// <summary>
        /// Create the directory for the given filepath if it doesn't exist.
        /// </summary>
        public static string CreateFileDirectory(string filepath)
        {
            var dirPath = Path.GetDirectoryName(filepath);
            if (!string.IsNullOrEmpty(dirPath) && !Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            return filepath;
        }

        /// <summary>
        /// Creates a directory if needed, or clears all of its contents otherwise
        /// </summary>
        public static string CreateAndClearDirectory(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            else
                DeleteFolderContents(dirPath);
            return dirPath;
        }

        /// <summary>
        /// Returns true if the given directory contains no files or if the directory does not exist.
        /// </summary>
        public static bool DirectoryIsEmpty(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                return true;
            }

            return Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories).Length == 0;
        }

        /// <summary>
        /// Deletes the target filepath if it exists and creates the containing directory.
        /// </summary>
        public static string DeleteFilepathAndCreateParentDirectory(string filepath)
        {
            // Delete the filepath (or directory) if it already exists.
            if (File.Exists(filepath)) { File.Delete(filepath); }
            else if (Directory.Exists(filepath)) { Directory.Delete(filepath, true); }

            // Create the target directory containing the output path.
            var fullPath = Path.GetFullPath(filepath);
            var fullDirPath = Path.GetDirectoryName(fullPath);
            Directory.CreateDirectory(fullDirPath);

            return filepath;
        }

        /// <summary>
        /// Returns the files in the given directory matching the given predicate function.
        /// </summary>
        public static IEnumerable<FileInfo> GetFilesInDirectoryWhere(string dirPath, Func<FileInfo, bool> predicateFn,
            bool recurse = true)
            => !Directory.Exists(dirPath)
                ? Enumerable.Empty<FileInfo>()
                : Directory.GetFiles(dirPath, "*",
                        recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                    .Select(f => new FileInfo(f))
                    .Where(predicateFn);

        /// <summary>
        /// Useful quick test to assure that we can create a file in the folder and write to it.
        /// </summary>
        public static void TestWrite(string folder)
        {
            var fileName = Path.Combine(folder, "_deleteme_.tmp");
            File.WriteAllText(fileName, "test");
            File.Delete(fileName);
        }

        // File size reporting

        static readonly string[] ByteSuffixes = {"B", "KB", "MB", "GB", "TB", "PB", "EB"}; //Longs run out around EB

        /// Improved version of https://stackoverflow.com/questions/281640/how-do-i-get-a-human-readable-file-size-in-bytes-abbreviation-using-net
        public static string BytesToString(long byteCount, int numPlacesToRound = 1)
        {
            if (byteCount == 0) return "0B";
            var bytes = Math.Abs(byteCount);
            var place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            var num = Math.Round(bytes / Math.Pow(1024, place), numPlacesToRound);
            return $"{(Math.Sign(byteCount) * num).ToString($"F{numPlacesToRound}")}{ByteSuffixes[place]}";
        }

        /// <summary>
        /// Returns the file size in bytes, or 0 if there is no file.
        /// </summary>
        public static long FileSize(string fileName)
            => File.Exists(fileName) ? new FileInfo(fileName).Length : 0;

        /// <summary>
        /// Returns the file size in bytes, or 0 if there is no file.
        /// </summary>
        public static string FileSizeAsString(string fileName, int numPlacesToShow = 1)
            => BytesToString(FileSize(fileName), numPlacesToShow);

        /// <summary>
        /// Returns the file size in bytes, or 0 if there is no file.
        /// </summary>
        public static string FileSizeAsString(this FileInfo f, int numPlacesToShow = 1)
            => BytesToString(f.Length, numPlacesToShow);

        /// <summary>
        /// Returns the total file size of all files given
        /// </summary>
        public static long TotalFileSize(IEnumerable<string> files)
            => files.Sum(FileSize);

        /// <summary>
        /// Returns the total file size of all files given as a human readable string
        /// </summary>
        public static string TotalFileSizeAsString(IEnumerable<string> files, int numPlacesToShow = 1)
            => BytesToString(TotalFileSize(files), numPlacesToShow);

        /// <summary>
        /// Returns the most recently written to sub-folder
        /// </summary>
        public static string GetMostRecentSubFolder(string folderPath)
            => Directory.GetDirectories(folderPath).OrderByDescending(f => new DirectoryInfo(f).LastWriteTime)
                .FirstOrDefault();

        /// <summary>
        /// Adds an item to the referenced list, creating it if needed
        /// </summary>
        public static void AddToList<T>(ref IList<T> xs, T x)
            => (xs ?? (xs = new List<T>())).Add(x);

        /// <summary>
        /// Remove starting and ending quotes.
        /// </summary>
        public static string StripQuotes(this string s)
            => s.Length >= 2 && s[0] == '"' && s[s.Length - 1] == '"' ? s.Substring(1, s.Length - 2) : s;

        /// <summary>
        /// Creates a string using the given quote delimiters. If endQuote is null, the beginQuote is used at the end as well.
        /// </summary>
        public static string Quote(this string s, string beginQuote = "\"", string endQuote = null)
            => $"{beginQuote}{s}{endQuote ?? beginQuote}";

        /// <summary>
        /// Given a sequence of elements, and a mapping function from element to parent, returns a dictionary of lists that maps elements to children.
        /// </summary>
        public static DictionaryOfLists<T, T> ComputeChildren<T>(this IEnumerable<T> elements,
            Func<T, T> parentSelector)
        {
            var r = new DictionaryOfLists<T, T>();
            foreach (var e in elements)
            {
                var p = parentSelector(e);
                if (p != null)
                    r.Add(p, e);
            }

            return r;
        }


        /// <summary>
        /// Treats a Dictionary of lists as a graph and visits the node,
        /// and all its descendants, exactly once using a depth first traversal.
        /// </summary>
        public static IEnumerable<T> EnumerateSubNodes<T>(this DictionaryOfLists<T, T> self, T target,
            HashSet<T> visited = null)
        {
            if (visited?.Contains(target) ?? false)
                yield break;
            yield return target;
            visited = visited ?? new HashSet<T>();
            visited.Add(target);
            foreach (var x in self.GetOrDefault(target))
            {
                foreach (var c in self.EnumerateSubNodes(x, visited))
                    yield return c;
            }
        }

        /// <summary>
        /// Returns the top of a stack, or the default T value if none is present.
        /// </summary>
        public static T PeekOrDefault<T>(this Stack<T> self)
            => self.Count > 0 ? self.Peek() : default;

        /// <summary>
        /// A substitute for SingleOrDefault which does not throw an exception when the collection size is greater than 1.
        /// If the collection size is greater than 1, returns default.
        /// </summary>
        public static T OneOrDefault<T>(this IEnumerable values)
        {
            var items = values?.OfType<T>().ToList();
            return items?.Count == 1 ? items[0] : default;
        }

        /// <summary>
        /// Zips a file and places the result into a newly created file in the temporary directory
        /// </summary>
        public static string ZipFile(string filePath)
            => ZipFile(filePath, Path.GetTempFileName());

        /// <summary>
        /// Zips a file and places the result into a newly created file in the temporary directory
        /// </summary>
        public static string ZipFile(string filePath, string outputFile)
        {
            using (var za = new ZipArchive(File.OpenWrite(outputFile), ZipArchiveMode.Create))
            {
                var zae = za.CreateEntry(Path.GetFileName(filePath) ?? "");
                using (var outputStream = zae.Open())
                using (var inputStream = File.OpenRead(filePath))
                    inputStream.CopyTo(outputStream);
            }

            return outputFile;
        }

        /// <summary>
        /// Unzips the first entry in an archive to a designated file, returning that file path.
        /// </summary>
        public static string UnzipFile(string zipFilePath, string outputFile)
        {
            using (var za = new ZipArchive(File.OpenRead(zipFilePath), ZipArchiveMode.Read))
            {
                var zae = za.Entries[0];
                using (var inputStream = zae.Open())
                using (var outputStream = File.OpenWrite(outputFile))
                    inputStream.CopyTo(outputStream);
            }

            return outputFile;
        }

        public static StreamWriter CreateAndOpenEntry(this ZipArchive archive, string entryName)
        {
            var entry = archive.CreateEntry(entryName);
            return new StreamWriter(entry.Open());
        }

        // TODO: there could be a bug in this code, when I used it I seemed to have some problems with sporadic Zip creation
        public static void CreateEntryFromText(this ZipArchive archive, string entryName, string content)
        {
            using (var sw = archive.CreateAndOpenEntry(entryName))
            {
                sw.Write(content);
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary>
        /// Unzips the first entry in an archive into a temp generated file, returning that file path
        /// </summary>
        public static string UnzipFile(string zipFilePath)
            => UnzipFile(zipFilePath, Path.GetTempFileName());

        /// <summary>
        /// Returns a binary writer for the given file path
        /// </summary>
        public static BinaryWriter CreateBinaryWriter(string filePath)
            => new BinaryWriter(File.OpenWrite(filePath));

        /// <summary>
        /// Returns a binary reader for the given file path
        /// </summary>
        public static BinaryReader CreateBinaryReader(string filePath)
            => new BinaryReader(File.OpenRead(filePath));

        /// <summary>
        /// Generic depth first traversal. Improved answer over:
        /// https://stackoverflow.com/questions/5804844/implementing-depth-first-search-into-c-sharp-using-list-and-stack
        /// </summary>
        public static IEnumerable<T> DepthFirstTraversal<T>(T root, Func<T, IEnumerable<T>> childGen,
            HashSet<T> visited = null)
            => DepthFirstTraversal(Enumerable.Repeat(root, 1), childGen, visited);

        /// <summary>
        /// Generic depth first traversal. Improved answer over:
        /// https://stackoverflow.com/questions/5804844/implementing-depth-first-search-into-c-sharp-using-list-and-stack
        /// </summary>
        public static IEnumerable<T> DepthFirstTraversal<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> childGen,
            HashSet<T> visited = null)
        {
            var stk = new Stack<T>();
            foreach (var root in roots)
                stk.Push(root);
            visited = visited ?? new HashSet<T>();
            while (stk.Count > 0)
            {
                var current = stk.Pop();
                if (!visited.Add(current))
                    continue;
                yield return current;
                var children = childGen(current);
                if (children != null)
                {
                    foreach (var x in children)
                    {
                        if (!visited.Contains(x))
                            stk.Push(x);
                    }
                }
            }
        }

        /// <summary>
        /// Generic breadth first traversal.
        /// </summary>
        public static IEnumerable<T> BreadthFirstTraversal<T>(T root, Func<T, IEnumerable<T>> childGen,
            HashSet<T> visited = null)
            => BreadthFirstTraversal(new[] {root}, childGen, visited);

        /// <summary>
        /// Generic breadth first traversal.
        /// </summary>
        public static IEnumerable<T> BreadthFirstTraversal<T>(this IEnumerable<T> roots,
            Func<T, IEnumerable<T>> childGen, HashSet<T> visited = null)
        {
            var q = new Queue<T>();
            foreach (var root in roots)
                q.Enqueue(root);
            visited = visited ?? new HashSet<T>();
            while (q.Count > 0)
            {
                var current = q.Dequeue();
                if (!visited.Add(current))
                    continue;
                yield return current;
                var children = childGen(current);
                if (children != null)
                {
                    foreach (var x in children)
                    {
                        if (!visited.Contains(x))
                            q.Enqueue(x);
                    }
                }
            }
        }

        /// <summary>
        /// Given a full file path, collapses the full path into a checksum, and return a file name.
        /// </summary>
        // public static string FilePathToUniqueFileName(string filePath)
        //     => filePath.Replace('/', '\\').MD5HashAsBitConverterLowerInvariant() + "_" + Path.GetFileName(filePath);

        /// <summary>
        /// Returns the path in which the given directory path has been removed.
        /// </summary>
        public static string TrimDirectoryFromPath(string dir, string path)
        {
            if (string.IsNullOrEmpty(path)) return null;
            if (string.IsNullOrEmpty(dir)) return path;

            var dirFullPath = Path.GetFullPath(dir).ToLowerInvariant();
            var fullPath = Path.GetFullPath(path).ToLowerInvariant();

            return fullPath.StartsWith(dirFullPath)
                ? fullPath.Substring(dir.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                : fullPath;
        }

        /// <summary>
        /// A helper function for append one or more items to an IEnumerable.
        /// </summary>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> xs, params T[] x)
            => xs.Concat(x);

        /// <summary>
        /// Generates a Regular Expression character set from an array of characters
        /// </summary>
        public static Regex CharSetToRegex(params char[] chars)
            => new Regex($"[{Regex.Escape(new string(chars))}]");

        /// <summary>
        /// Creates a regular expression for finding illegal file name characters.
        /// </summary>
        public static Regex InvalidFileNameRegex =>
            CharSetToRegex(Path.GetInvalidFileNameChars());

        /// <summary>
        /// Convert a string to a valid name
        /// https://stackoverflow.com/questions/146134/how-to-remove-illegal-characters-from-path-and-filenames
        /// https://stackoverflow.com/questions/2230826/remove-invalid-disallowed-bad-characters-from-filename-or-directory-folder?noredirect=1&lq=1
        /// https://stackoverflow.com/questions/10898338/c-sharp-string-replace-to-remove-illegal-characters?noredirect=1&lq=1
        /// </summary>
        public static string ToValidFileName(this string s, string replacement = "_", int maxLength = -1)
        {
            var replaced = InvalidFileNameRegex.Replace(s, m => replacement);
            
            if (maxLength >= 0 && maxLength != replaced.Length)
            {
                replaced = replaced.Substring(0, Math.Min(maxLength, replaced.Length));
            }

            return replaced;
        }

        /// <summary>
        /// Returns true if the string has any invalid file name chars
        /// </summary>
        public static bool HasInvalidFileNameChars(this string s)
            => InvalidFileNameRegex.Match(s).Success;

        /// <summary>
        /// Returns the name of the outer most folder given a file path or a directory path
        /// https://stackoverflow.com/questions/3736462/getting-the-folder-name-from-a-path
        /// </summary>
        public static string DirectoryName(string filePath)
            => new DirectoryInfo(filePath).Name;

        /// <summary>
        /// Changes the directory and the extension of a file. The new extension may or may not be specified with a leading period.
        /// </summary>
        public static string ChangeDirectoryAndExt(string filePath, string newFolder, string newExt)
            => Path.ChangeExtension(ChangeDirectory(filePath, newFolder), newExt);

        /// <summary>
        /// Changes the directory of a file
        /// </summary>
        public static string ChangeDirectory(string filePath, string newFolder)
            => Path.Combine(newFolder, Path.GetFileName(filePath));

        /// <summary>
        /// Counts groups of a given key
        /// </summary>
        public static Dictionary<T, int> CountGroups<T>(this IEnumerable<T> self)
            => self.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());

        /// <summary>
        /// Returns distinct values each one assigned a new incremented index.
        /// </summary>
        public static IndexedSet<T> ToIndexedSet<T>(this IEnumerable<T> self)
            => new IndexedSet<T>(self);

        /// <summary>
        /// Applies a function to transform a function name (withtout extension) leaving it in the same folder and keeping the original extension
        /// </summary>
        public static string TransformFileName(string filePath, Func<string, string> func)
            => Path.Combine(Path.GetDirectoryName(filePath) ?? "",
                func(Path.GetFileNameWithoutExtension(filePath)) + Path.GetExtension(filePath));

        /// <summary>
        /// Prepends text to the file name keeping it in the same folder and with the same extension
        /// </summary>
        public static string PrependFileName(string filePath, string text)
            => TransformFileName(filePath, name => text + name);

        /// <summary>
        /// Prepends text to the file name keeping it in the same folder and with the same extension
        /// </summary>
        public static string AppendFileName(string filePath, string text)
            => TransformFileName(filePath, name => name + text);

        /// <summary>
        /// Returns all the lines of all the files
        /// </summary>
        public static IEnumerable<string> ReadManyLines(IEnumerable<string> fileNames)
            => fileNames.SelectMany(File.ReadLines);

        /// <summary>
        /// Concatenates the contents of all the files and writes them to a new file.
        /// </summary>
        public static void ConcatFiles(string filePath, IEnumerable<string> fileNames)
            => File.WriteAllLines(filePath, ReadManyLines(fileNames));

        /// <summary>
        /// Concatenates the contents of all the CSV files and writes them to a new file.
        /// Only the header of the first file is kept
        /// </summary>
        public static void ConcatCsvFiles(string filePath, IEnumerable<string> fileNames)
            => File.WriteAllLines(filePath,
                fileNames.SelectMany((f, n) =>
                    File.ReadLines(f).Skip(n > 0 ? 1 : 0)));

        /// <summary>
        /// Given a collection of items, counts how often each unique item is found.
        /// </summary>
        public static Dictionary<T, int> CountInstances<T>(this IEnumerable<T> self)
            => self.WhereNotNull().GroupBy(x => x).ToDictionary(grp => grp.Key, grp => grp.Count());

        /// <summary>
        /// Given a collection of items, and a map function, counts how often each mapped item is found.
        /// </summary>
        public static Dictionary<U, int> CountInstances<T, U>(this IEnumerable<T> self, Func<T, U> map)
            => self.Select(map).CountInstances();

        public static DictionaryOfLists<TKey, TValue> ToDictionaryOfLists<TKey, TValue>(
            this IEnumerable<IGrouping<TKey, TValue>> groups)
            => new DictionaryOfLists<TKey, TValue>(groups);

        public static string[] SplitAtNull(this string s)
            => s.Split('\0');

        public static string[] SplitWhitespace(this string value)
            => value.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

        public static string JoinEllided<T>(string separator, IEnumerable<T> values, int count = 5)
        {
            var tmp = values.Take(count).ToList();
            var prefix = string.Join(separator, tmp);
            if (tmp.Count < count)
                return prefix;
            var last = values.Last();
            return $"{prefix} ... {last}";
        }

        public static string JoinWithNull(this IEnumerable<string> strings)
            => string.Join("\0", strings);

        public static string JoinDistinct(this IEnumerable<string> strings, string delim = ";")
            => string.Join(delim, strings.Distinct().OrderBy(x => x));

        // NOTE: Calling a function generates additional memory
        public static long GetMemoryConsumption(Action action)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            var memBefore = GC.GetTotalMemory(true);
            action();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return GC.GetTotalMemory(true) - memBefore;
        }

        public class MemWatch
        {
            private long _before;
            private long _after;

            public long ElapsedBytes => _after - _before;
            public long ElapsedKiloBytes => ElapsedBytes / 1000;
            public long ElapsedMegaBytes => ElapsedBytes / 1000000;

            public void Start()
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                _before = GC.GetTotalMemory(true);
            }

            public void Stop()
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                _after = GC.GetTotalMemory(true);
            }
        }

        public static long GetMSecElapsed(Action action)
        {
            var sw = Stopwatch.StartNew();
            action();
            return sw.ElapsedMilliseconds;
        }

        public static (long, long) GetMemoryConsumptionAndMSecElapsed(Action action)
        {
            var time = 0L;
            var mem = GetMemoryConsumption(
                () => time = GetMSecElapsed(action));
            return (mem, time);
        }

        public static IEnumerable<Type> GetAllTypes()
            => AppDomain.CurrentDomain.GetAssemblies().SelectMany(asm => asm.GetTypes()).Distinct();

        // https://stackoverflow.com/questions/857705/get-all-derived-types-of-a-type
        public static IEnumerable<Type> GetAllSubclassesOf(Type t)
            => GetAllTypes().Where(x => x.IsSubclassOf(t));

        public static IEnumerable<Type> GetAllSubclassesOf(Assembly asm, Type t)
            => asm.GetTypes().Where(x => x.IsSubclassOf(t));

        // public static Disposer Disposer(Action action)
        //     => new Disposer(action);
        //
        // public static Disposer Disposer(Action beforeAction, Action afterAction)
        // {
        //     beforeAction();
        //     return new Disposer(afterAction);
        // }
        //
        // public static Disposer ReportMemoryConsumptionAndTimeElapsed()
        // {
        //     GC.Collect();
        //     GC.WaitForPendingFinalizers();
        //     var memBefore = GC.GetTotalMemory(true);
        //     var sw = Stopwatch.StartNew();
        //     return Disposer(() =>
        //     {
        //         OutputTimeElapsed(sw, "Time Elapsed");
        //         GC.Collect();
        //         GC.WaitForPendingFinalizers();
        //         var memConsumption = GC.GetTotalMemory(true) - memBefore;
        //         Console.WriteLine($"Approximate memory consumption = {BytesToString(memConsumption)}");
        //     });
        // }

        public static T LoadFileAndReportStats<T>(string fileName, Func<string, T> loadFunc)
        {
            T file = default;
            var (mem, msec) = GetMemoryConsumptionAndMSecElapsed(() => file = loadFunc(fileName));
            Console.WriteLine(
                $"Loading {fileName}\nof size {FileSizeAsString(fileName)}\ntakes {MSecToSecondsString(msec)}\nconsumes {BytesToString(mem)}");
            return file;
        }

        public static Process OpenFolderInExplorer(string folderPath)
            => Process.Start("explorer.exe", folderPath);

        public static Process SelectFileInExplorer(string filePath)
            => Process.Start(new ProcessStartInfo
            {
                FileName = "explorer.exe", Arguments = $"/select,\"{filePath}\"", UseShellExecute = false
            });

        public static Process ShellExecute(string filePath)
            => Process.Start(new ProcessStartInfo {FileName = filePath, UseShellExecute = true});

        public static Process OpenFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("", filePath);

            // Expand the file name
            filePath = new FileInfo(filePath).FullName;

            // Open the file with the default file extension handler.
            try
            {
                return Process.Start(filePath);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }

            // If there is no default file extension handler, use shell execute
            try
            {
                return ShellExecute(filePath);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }

            // If that didn't work, show the file in explorer.
            return SelectFileInExplorer(filePath);
        }

        /// <summary>
        /// Returns true if the URI is valid.
        /// </summary>
        public static bool IsValidUri(string uri)
        {
            // see: https://stackoverflow.com/a/33573227
            if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                return false;
            Uri tmp;
            if (!Uri.TryCreate(uri, UriKind.Absolute, out tmp))
                return false;
            return tmp.Scheme == Uri.UriSchemeHttp || tmp.Scheme == Uri.UriSchemeHttps;
        }

        /// <summary>
        /// Opens the URI in the browser and returns true if the uri is valid.
        /// </summary>
        public static bool OpenUri(string uri)
        {
            // see: https://stackoverflow.com/a/33573227
            if (!IsValidUri(uri))
                return false;
            Process.Start(new ProcessStartInfo(uri) { UseShellExecute = true });
            return true;
        }

        /// <summary>
        /// Returns the result of the getValue function. If getValue throws an exception, the onExceptionAction is invoked and the defaultValue is returned.
        /// </summary>
        public static T TryGetValue<T>(Func<T> getValue, T defaultValue, Action<Exception> onExceptionAction = null)
        {
            try
            {
                return getValue();
            }
            catch (Exception e)
            {
                onExceptionAction?.Invoke(e);
            }

            return defaultValue;
        }

        /// Converts an array of unmanaged types to unmanaged types.
        /// </summary>
        public static unsafe U[] Cast<T, U>(this T[] self) where T : unmanaged where U : unmanaged
        {
            var nBytes = sizeof(T) * self.LongLength;
            var r = new byte[nBytes];
            return r.ToArray<U>();
        }

        /// <summary>
        /// Converts an array of bytes to an unmanaged array.
        /// Throws an exception if the number of bytes does not divide equally
        /// by the size of the type.
        /// </summary>
        public static unsafe T[] ToArray<T>(this byte[] self) where T : unmanaged
        {
            var mod = self.LongLength % sizeof(T);
            if (mod != 0)
                throw new Exception($"{self.LongLength} % {sizeof(T)} = {mod} != 0");
            var n = self.LongLength / sizeof(T);
            var r = new T[n];
            fixed (void* voidPtr = self)
            {
                var ptr = (T*)voidPtr;
                for (var i = 0L; i < n; ++i)
                    r[i] = ptr[i];
            }

            return r;
        }

        public static T Minimize<T, U>(this IEnumerable<T> xs, U init, Func<T, U> func) where U : IComparable<U>
        {
            var r = default(T);
            foreach (var x in xs)
            {
                if (func(x).CompareTo(init) < 0)
                {
                    init = func(x);
                    r = x;
                }
            }

            return r;
        }

        public static T Maximize<T, U>(this IEnumerable<T> xs, U init, Func<T, U> func) where U : IComparable<U>
        {
            var r = default(T);
            foreach (var x in xs)
            {
                if (func(x).CompareTo(init) > 0)
                {
                    init = func(x);
                    r = x;
                }
            }

            return r;
        }

        /// <summary>
        /// Finds an a parent (or ancestor) directory that satisfies the criteria
        /// </summary>
        public static DirectoryInfo FindParentDirectory(DirectoryInfo currentDirectory,
            Func<DirectoryInfo, bool> predicate, int maxIterations = 15)
        {
            for (var i = 0; i < maxIterations; ++i)
            {
                if (currentDirectory is null)
                    return null;

                // base case: predicate matches.
                if (predicate(currentDirectory))
                    return currentDirectory;

                // recursive case: go up one directory.
                currentDirectory = currentDirectory?.Parent;
            }

            return null;
        }

        /// <summary>
        /// Advances a stream a fixed number of bytes.
        /// </summary>
        public static void Advance(this Stream stream, long count, int bufferSize = 4096)
        {
            if (stream.CanSeek)
            {
                stream.Position += count;
                return;
            }

            var buffer = new byte[bufferSize];
            int bytesRead;
            while ((bytesRead = stream.Read(buffer, 0, (int)Math.Min(buffer.Length, count))) > 0)
            {
                count -= bytesRead;
            }
        }

        #region Pair functions

        public static (T2, T1) Swap<T1, T2>(this (T1, T2) pair)
            => (pair.Item2, pair.Item1);

        public static (T, T) Order<T>(this (T, T) pair) where T : IComparable<T> =>
            pair.Item1.CompareTo(pair.Item2) <= 0 ? pair : pair.Swap();

        public static (T2, T2) Select<T1, T2>(this (T1, T1) pair, Func<T1, T2> f)
            => (f(pair.Item1), f(pair.Item2));

        public static string ToDelimitedString<T1, T2>(this (T1, T2) pair, string delim = ":")
            => $"{pair.Item1}{delim}{pair.Item2}";

        public static IEnumerable<T> SelectMany<T>(this IEnumerable<(T, T)> self)
            => self.Select(x => x.Item1).Concat(self.Select(x => x.Item2));

        #endregion

        public static object[,] ToArray2D(this DataTable dataTable, bool includeHeaders = true)
        {
            // on the first iteration we add the column headers
            var nRows = dataTable.Rows.Count;
            var nCols = dataTable.Columns.Count;
            var r = new object[nRows + (includeHeaders ? 1 : 0), nCols];
            if (includeHeaders)
            {
                for (var i = 0; i < dataTable.Columns.Count; i++)
                    r[0, i] = dataTable.Columns[i].ColumnName;
            }

            var curRow = includeHeaders ? 1 : 0;
            foreach (DataRow datarow in dataTable.Rows)
            {
                for (var i = 0; i < dataTable.Columns.Count; i++)
                    r[curRow, i] = datarow[i];
                curRow++;
            }

            return r;
        }

        public static object[,] ToArray2D(this object[][] self)
        {
            var nCols = self.Max(x => x.Length);
            var nRows = self.Length;
            var r = new object[nRows, nCols];
            for (var i = 0; i < nRows; ++i)
            {
                var row = self[i];
                for (var j = 0; j < row.Length; ++j)
                    r[i, j] = row[j];
            }

            return r;
        }

        public static object[,] ToArray2D(this IEnumerable<object[]> self)
            => self.ToArray().ToArray2D();

        public static object[,] ToArray2D(this IEnumerable<IEnumerable<object>> self)
            => self.Select(x => x.ToArray()).ToArray2D();

        public static string JoinStrings<T1, T2>(this IDictionary<T1, T2> self, string sep = ";")
            => self.Select(kv => $"{kv.Key}={kv.Value}").JoinStrings(sep);

        public static string JoinStrings<T>(this IEnumerable<T> self, string sep = ";")
            => string.Join(sep, self);

        public static T PopIfNotEmpty<T>(this Stack<T> self)
            => self.Count > 0 ? self.Pop() : default;

        public static DictionaryOfLists<TKey, TValue> ToDictionaryOfLists<TSrc, TKey, TValue>(
            this IEnumerable<TSrc> self, Func<TSrc, TKey> keySelector, Func<TSrc, TValue> valueSelector)
        {
            var r = new DictionaryOfLists<TKey, TValue>();
            foreach (var x in self)
                r.Add(keySelector(x), valueSelector(x));
            return r;
        }

        /// <summary>
        /// Given a grouping, creates a lookup from elements back o keys
        /// </summary>
        public static Dictionary<U, T> MapElementsToKeys<T, U>(this IEnumerable<IGrouping<T, U>> self)
            => self.SelectMany(grp => grp.Select(element => (grp.Key, element)))
                .ToDictionary(pair => pair.element, pair => pair.Key);

        /// <summary>
        /// Like Linq.ToDictionary but skips duplicate keys, without throwing an exception.
        /// </summary>
        public static Dictionary<TKey, TValue> ToDictionaryIgnoreDuplicates<TSrc, TKey, TValue>(
            this IEnumerable<TSrc> self, Func<TSrc, TKey> keySelector, Func<TSrc, TValue> valueSelector)
        {
            var r = new Dictionary<TKey, TValue>();
            foreach (var x in self)
                r.AddIfNotPresent(keySelector(x), valueSelector(x));
            return r;
        }

        /// <summary>
        /// Returns an array of one item, containing this item, unless it is null, in which case a zero-length array is returned
        /// </summary>
        public static T[] ToEmptyOrOneSizeArray<T>(this T self) where T : class
            => self == null ? Array.Empty<T>() : new[] {self};

        /// <summary>
        /// Loosely based on https://stackoverflow.com/a/11360186/184528 and https://stackoverflow.com/a/6499682/184528
        /// https://www.w3schools.com/sql/sql_datatypes.asp
        /// </summary>
        public static string ToSqlServerType(Type t)
        {
            switch (t.ToString())
            {
                case "System.Int32":
                    return "int";
                case "System.Int64":
                    return "bigint";
                case "System.Int16":
                    return "smallint";
                case "System.Byte":
                    return "tinyint";
                case "System.Bool":
                    return "bit";
                case "System.Float":
                case "System.Double":
                    return "float";
                case "System.Decimal":
                    return "money";
                case "System.DateTime":
                    return "datetime";
                case "System.String":
                    return "nvarchar(max)";
                default:
                    return "varbinary(max)";
            }
        }

        public static bool IsPrimaryKey(this DataColumn col)
            => col.Table.PrimaryKey.Any(pk => pk.ColumnName == col.ColumnName);

        /// <summary>
        /// Loosely based on https://stackoverflow.com/a/11360186/184528 and https://stackoverflow.com/a/6499682/184528
        /// </summary>
        public static string ToSqlServerCreateTableString(DataTable table)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"CREATE TABLE [{table.TableName}] (");
            for (var i = 0; i < table.Columns.Count; i++)
            {
                if (i > 0) sb.AppendLine(",");
                var col = table.Columns[i];
                var type = ToSqlServerType(col.DataType);
                sb.Append($"[{col.ColumnName}] {type}");

                if (col.AutoIncrement)
                    sb.Append($" IDENTITY({col.AutoIncrementSeed},{col.AutoIncrementStep})");

                if (!col.AllowDBNull)
                    sb.Append(" NOT NULL");

                if (col.Unique && (table.PrimaryKey.Length != 1 || !col.IsPrimaryKey()))
                    sb.Append(" UNIQUE");
            }

            if (table.PrimaryKey?.Length > 0)
            {
                sb.AppendLine(",");
                sb.Append("PRIMARY KEY (");

                for (var i = 0; i < table.PrimaryKey.Length; i++)
                {
                    sb.Append(table.PrimaryKey[i]);

                    if (i < table.PrimaryKey.Length - 1)
                        sb.Append(", ");
                }

                sb.Append(')');
            }

            return sb.AppendLine(")").ToString();
        }

        public static IEnumerable<ForeignKeyConstraint> GetForeignKeyConstraints(this DataTable table)
        {
            foreach (Constraint c in table.Constraints)
            {
                if (c is ForeignKeyConstraint fkc)
                    yield return fkc;
            }
        }

        public static string SqlServerAddConstraint(string tableName, string constraintName, string foreignKey,
            string refTable, string refColumn)
            =>
                $"ALTER TABLE [{tableName}] ADD CONSTRAINT {constraintName} FOREIGN KEY ([{foreignKey}]) REFERENCES [{refTable}]([{refColumn}])";

        public static string ToSqlServerAddConstraints(DataTable table, bool cascadeDelete)
        {
            if (!table.GetForeignKeyConstraints().Any())
                return "";

            var sb = new StringBuilder();
            sb.AppendLine($"ALTER TABLE [{table.TableName}]");

            // https://www.w3schools.com/sql/sql_foreignkey.asp
            var first = true;
            foreach (var constraint in table.GetForeignKeyConstraints())
            {
                var parentColumn = constraint.RelatedColumns[0];
                var childColumn = constraint.Columns[0];
                if (!first)
                {
                    sb.AppendLine(", ");
                }
                else
                {
                    // https://stackoverflow.com/questions/24593101/add-multiple-constraints-in-one-statement
                    sb.AppendLine("ADD");
                    first = false;
                }

                sb.Append($" CONSTRAINT {constraint.ConstraintName}");
                sb.Append($" FOREIGN KEY ([{childColumn.ColumnName}])");
                sb.Append($" REFERENCES [{parentColumn.Table.TableName}]([{parentColumn.ColumnName}])");

                if (cascadeDelete)
                    sb.Append(" ON DELETE CASCADE");
            }

            return sb.AppendLine(";").ToString();
        }

        public static string Drop(this string s, int n)
            => s.Substring(0, Math.Max(0, s.Length - n));

        // https://stackoverflow.com/questions/22794466/parsing-all-possible-types-of-varying-architectural-dimension-input
        // https://stackoverflow.com/questions/6157865/c-sharp-function-to-convert-text-input-of-feet-inches-meters-centimeters-millime

        public static Regex FeetAndInchesRegex
            = new Regex(
                "^\\s*(?<minus>-)?\\s*(((?<feet>\\d+)(?<inch>\\d{2})(?<sixt>\\d{2}))|((?<feet>[\\d.]+)')?[\\s-]*((?<inch>\\d+)?[\\s-]*((?<numer>\\d+)/(?<denom>\\d+))?\")?)\\s*$",
                RegexOptions.Compiled);

        public static Regex NumberAndUnitsRegex
            = new Regex("^\\s*(?<number>-?[0-9]*[.]?[0-9]+)([\\s]+(?<units>[a-zA-Z°²³$£%/*^@#`]*))?\\s*$", RegexOptions.Compiled);

        /// <summary>
        /// Parses the input string and outputs the value in decimal inches upon success
        /// </summary>
        public static bool TryParseFeetAndInchesToDecimalInches(string input, out double value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(input))
                return false;

            var m = FeetAndInchesRegex.Match(input);
            if (!m.Success)
                return false;

            var sign = m.Groups["minus"].Success ? -1 : 1;
            var feet = m.Groups["feet"].Success &&
                double.TryParse(
                    m.Groups["feet"].Value,
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out var f) ? f : 0;
            var inch = m.Groups["inch"].Success ? Convert.ToInt32(m.Groups["inch"].Value) : 0;
            var sixt = m.Groups["sixt"].Success ? Convert.ToInt32(m.Groups["sixt"].Value) : 0;
            var numer = m.Groups["numer"].Success ? Convert.ToInt32(m.Groups["numer"].Value) : 0;
            var denom = m.Groups["denom"].Success ? Convert.ToInt32(m.Groups["denom"].Value) : 1;
            value = sign * (feet * 12 + inch + sixt / 16.0 + numer / Convert.ToDouble(denom));
            return true;
        }

        public static byte[] UnGzipIfNeeded(this byte[] bytes)
        {
            //https://superuser.com/questions/115902/tell-if-a-gz-file-is-really-gzipped
            if (bytes.IsGzipped())
            {
                using (var mem = new MemoryStream(bytes))
                using (var zip = new GZipStream(mem, CompressionMode.Decompress))
                {
                    return zip.ReadAllBytes();
                }
            }

            return bytes;
        }

        public static bool IsGzipped(this byte[] bytes)
        {
            const byte GzipMagicByte0 = 0x1F;
            const byte GzipMagicByte1 = 0x8B;
            return bytes[0] == GzipMagicByte0 && bytes[1] == GzipMagicByte1;
        }

        public static DateTime JanFirst1970 = new DateTime(1970, 1, 1);

        public static DateTime InitializedTime = DateTime.Now.ToUniversalTime();

        public static double NowInMSec()
            => (DateTime.Now.ToUniversalTime() - JanFirst1970).TotalMilliseconds;

        public static double ElapsedMSec()
            => (InitializedTime - JanFirst1970).TotalMilliseconds;

        public static void Deconstruct<T1, T2>(this KeyValuePair<T1, T2> tuple, out T1 key, out T2 value)
        {
            key = tuple.Key;
            value = tuple.Value;
        }

        public static IEnumerable<(string name, T value)> GetEnumValueTuples<T>() where T : Enum
            => Enum.GetValues(typeof(T)).Cast<T>().Select(item => (Enum.GetName(typeof(T), item), item));

        public static IEnumerable<(string name, int value)> GetEnumIntValueTuples<T>() where T : Enum
            => GetEnumValueTuples<T>().Select(t => (t.name, (int) (object) t.value));

        public static IEnumerable<Assembly> GetReferencedAssemblies(this Type type)
        {
            yield return type.Assembly;

            foreach (var assemblyName in type.Assembly.GetReferencedAssemblies())
            {
                yield return Assembly.ReflectionOnlyLoad(assemblyName.FullName);
            }
        }

        // https://stackoverflow.com/questions/1582510/get-paths-of-assemblies-used-in-type
        public static IEnumerable<string> GetReferencedAssemblyPaths(this Type type)
            => type.GetReferencedAssemblies().Select(asm => asm.Location);

        public static Stream CreateStream(this string input)
        {
            // From: https://stackoverflow.com/a/1879470
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(input);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }

    // TODO: make the Util class safe, and move unsafe functions into a separate util class
    public static class SafeUtil
    {
        public static async Task WhenAll(params Action[] actions)
            => await Task.WhenAll(actions.Select(Task.Run));
    }

    public class EqualityComparerInt : EqualityComparer<int>
    {
        public static EqualityComparerInt Instance = new EqualityComparerInt();
        public override bool Equals(int x, int y) => x == y;
        public override int GetHashCode(int obj) => obj;
    }
}
