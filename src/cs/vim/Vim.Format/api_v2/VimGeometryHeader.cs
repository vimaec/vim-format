using System;
using System.IO;
using System.Text;
using Vim.BFast;
using Vim.Util;

namespace Vim.Format.api_v2
{
    public class VimGeometryHeader
    {
        public static class Constants
        {
            public const byte MagicA = 0x63; // MagicA + MagicB -> 63D0 (for continuity with the original G3D file format, VIM geometry maintains these magic numbers)
            public const byte MagicB = 0xD0;
            public const byte UnitA = (byte)'f'; // UnitA + UnitB -> 'ft' (for historical reasons related to Revit's precision, VIM geometry is always in feet)
            public const byte UnitB = (byte)'t';
            public const string Units = "ft";
            public const byte UpAxis = 2; // 2 -> z axis (preserved for continuity)
            public const byte ForwardVector = 0; // 0 -> x axis (preserved for continuity)
            public const byte Handedness = 0; // 0 -> left handed (preserved for continuity)
            public const byte Padding = 0; // has always been 0
        }

        public byte MagicA { get; private set; }
        public byte MagicB { get; private set; }
        public byte UnitA { get; private set; }
        public byte UnitB { get; private set; }
        public byte UpAxis { get; private set; }
        public byte ForwardVector { get; private set; }
        public byte Handedness { get; private set; }
        public byte Padding { get; private set; }

        /// <summary>
        /// Constructor. Creates a default header with correctly populated values.
        /// </summary>
        public VimGeometryHeader()
        {
            MagicA = Constants.MagicA;
            MagicB = Constants.MagicB;
            UnitA = Constants.UnitA;
            UnitB = Constants.UnitB;
            UpAxis = Constants.UpAxis;
            ForwardVector = Constants.ForwardVector;
            Handedness = Constants.Handedness;
            Padding = Constants.Padding;
        }

        /// <summary>
        /// Constructor. Loads and validates the byte values to create the header.
        /// </summary>
        public VimGeometryHeader(byte[] bytes)
        {
            if (bytes.Length < 7)
                throw new ArgumentException($"Failed to read VIM Geometry Header: {nameof(bytes)} argument must have length 7 or greater but received length {bytes.Length}");

            MagicA = bytes[0];
            MagicB = bytes[1];
            UnitA = bytes[2];
            UnitB = bytes[3];
            UpAxis = bytes[4];
            ForwardVector = bytes[5];
            Handedness = bytes[6];
            Padding = Constants.Padding;

            Validate();
        }

        public static VimGeometryHeader Read(Stream stream, long size)
        {
            stream.ThrowIfNotSeekable("Failed to read VIM Geometry Header");

            var bytes  = stream.ReadArray<byte>((int)size);

            return new VimGeometryHeader(bytes);
        }

        private string Units => Encoding.ASCII.GetString(new byte[] { UnitA, UnitB });

        public byte[] ToBytes()
            => new[] {
                MagicA,
                MagicB,
                UnitA,
                UnitB,
                UpAxis,
                ForwardVector,
                Handedness,
                Padding,
            };

        public void Validate()
        {
            if (MagicA != Constants.MagicA) throw new Exception($"First magic number must be {Constants.MagicA} and not {MagicA}");
            if (MagicB != Constants.MagicB) throw new Exception($"Second magic number must be {Constants.MagicB} and not {MagicB}");
            if (Units != Constants.Units) throw new Exception($"Units must be '{Constants.Units}' and not {Units}");
            if (UpAxis != Constants.UpAxis) throw new Exception($"Up axis must be {Constants.UpAxis} and not {UpAxis}");
            if (ForwardVector != Constants.ForwardVector) throw new Exception($"Forward vector must be {Constants.ForwardVector} and not {ForwardVector}");
            if (Handedness != Constants.Handedness) throw new Exception($"Handedness must be {Constants.Handedness} and not {Handedness}");
        }
    }
}