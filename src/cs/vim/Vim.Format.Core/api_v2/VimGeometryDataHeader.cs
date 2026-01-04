using System;
using System.IO;
using System.Text;
using Vim.BFast;
using Vim.Util;

namespace Vim.Format
{
    public class VimGeometryDataHeader
    {
        public static class Constants
        {
            public const byte MagicA = 0x63; // MagicA + MagicB -> 63D0 (for continuity with the original G3D file format, VIM geometry maintains these magic numbers)
            public const byte MagicB = 0xD0;

            //------------------------------------------------------------------------------------
            // HISTORICAL NOTES ABOUT THE FIELDS NOW KNOWN AS 'FieldA' and 'FieldB'
            //
            // Martin Ashton, November 8 2025
            //
            // First off, the VIM file format's geometry has always been in feet to mirror Autodesk Revit's internal units (which are in feet).
            //
            // Prior to refactoring this code, the following constants were defined in the G3D header:
            // - 'UnitA'
            // - 'UnitB'
            //
            // The intent of 'UnitA' and 'UnitB' was to identify the units of scale of the geometry. In theory, you could combine both bytes to declare
            // units like 'm', 'cm', 'in', 'ft', etc (i.e. 'c' + 'm' = 'cm').
            // 
            // In practice however, 'UnitA' was always hard-coded to 'm' and 'UnitB' was always 0. This oversight was misleading since VIM's geometry is de-facto
            // defined in feet ('ft'). Thankfully, 'UnitA' and 'UnitB' were never actually consumed; all readers of VIM's geometry correctly interpreted it in feet.
            //
            // Now that I'm cleaning things up, I'm going to declare that 'UnitA' and 'UnitB' shall henceforth be known as 'FieldA' and 'FieldB',
            // and that 'FieldA' will always be 'm' and that 'FieldB' will always be empty.
            //
            // Since I'm the only one in here anyway, I'll also add that the 'm' stands for 'meow meow'. There, I said it. No takesie-backsies.
            //
            public const byte FieldA = (byte)'m';
            public const byte FieldB = 0;
            //------------------------------------------------------------------------------------
            public const byte UpAxis = 2; // 2 -> z axis (preserved for continuity)
            public const byte ForwardVector = 0; // 0 -> x axis (preserved for continuity)
            public const byte Handedness = 0; // 0 -> left handed (preserved for continuity)
            public const byte Padding = 0; // has always been 0
        }

        public byte MagicA { get; private set; }
        public byte MagicB { get; private set; }
        public byte FieldA { get; private set; }
        public byte FieldB { get; private set; }
        public byte UpAxis { get; private set; }
        public byte ForwardVector { get; private set; }
        public byte Handedness { get; private set; }
        public byte Padding { get; private set; }

        /// <summary>
        /// Constructor. Creates a default header with correctly populated values.
        /// </summary>
        public VimGeometryDataHeader()
        {
            MagicA = Constants.MagicA;
            MagicB = Constants.MagicB;
            FieldA = Constants.FieldA;
            FieldB = Constants.FieldB;
            UpAxis = Constants.UpAxis;
            ForwardVector = Constants.ForwardVector;
            Handedness = Constants.Handedness;
            Padding = Constants.Padding;
        }

        /// <summary>
        /// Constructor. Loads and validates the byte values to create the header.
        /// </summary>
        public VimGeometryDataHeader(byte[] bytes)
        {
            if (bytes.Length < 7)
                throw new ArgumentException($"Failed to read VIM Geometry Header: {nameof(bytes)} argument must have length 7 or greater but received length {bytes.Length}");

            MagicA = bytes[0];
            MagicB = bytes[1];
            FieldA = bytes[2];
            FieldB = bytes[3];
            UpAxis = bytes[4];
            ForwardVector = bytes[5];
            Handedness = bytes[6];
            Padding = Constants.Padding;

            Validate();
        }

        public static VimGeometryDataHeader Read(Stream stream, long size)
        {
            stream.ThrowIfNotSeekable("Failed to read VIM Geometry Header");

            var bytes  = stream.ReadArray<byte>((int)size);

            return new VimGeometryDataHeader(bytes);
        }

        public byte[] ToBytes()
            => new[] {
                MagicA,
                MagicB,
                FieldA,
                FieldB,
                UpAxis,
                ForwardVector,
                Handedness,
                Padding,
            };

        public void Validate()
        {
            if (MagicA != Constants.MagicA) throw new Exception($"First magic number must be {Constants.MagicA} and not {MagicA}");
            if (MagicB != Constants.MagicB) throw new Exception($"Second magic number must be {Constants.MagicB} and not {MagicB}");
            if (FieldA != Constants.FieldA) throw new Exception($"FieldA must be {Constants.FieldA} and not {FieldA}");
            if (FieldB != Constants.FieldB) throw new Exception($"FieldB must be {Constants.FieldB} and not {FieldB}");
            if (UpAxis != Constants.UpAxis) throw new Exception($"Up axis must be {Constants.UpAxis} and not {UpAxis}");
            if (ForwardVector != Constants.ForwardVector) throw new Exception($"Forward vector must be {Constants.ForwardVector} and not {ForwardVector}");
            if (Handedness != Constants.Handedness) throw new Exception($"Handedness must be {Constants.Handedness} and not {Handedness}");
        }
    }
}