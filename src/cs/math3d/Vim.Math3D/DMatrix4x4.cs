// MIT License
// Copyright (C) 2024 VIMaec LLC.
// Copyright (C) 2019 Ara 3D. Inc
// https://ara3d.com
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Vim.Math3d
{
    /// <summary>
    /// A structure encapsulating a 4x4 matrix.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), DataContract]
    public struct DMatrix4x4 : IEquatable<DMatrix4x4>
    {
        public DVector3 Col0 => new DVector3(M11, M21, M31);
        public DVector3 Col1 => new DVector3(M12, M22, M32);
        public DVector3 Col2 => new DVector3(M13, M23, M33);
        public DVector3 Col3 => new DVector3(M14, M24, M34);

        public DVector3 Row0 => new DVector3(M11, M12, M13);
        public DVector3 Row1 => new DVector3(M21, M22, M23);
        public DVector3 Row2 => new DVector3(M31, M32, M33);
        public DVector3 Row3 => new DVector3(M41, M42, M43);

        public DVector3 GetRow(int row)
            => row == 0 ? Row0 : row == 1 ? Row1 : row == 2 ? Row2 : Row3;

        public DVector3 GetCol(int col)
            => col == 0 ? Col0 : col == 1 ? Col1 : col == 2 ? Col2 : Col3;

        /// <summary>
        /// Value at row 1, column 1 of the matrix.
        /// </summary>
        [DataMember] public double M11;
        /// <summary>
        /// Value at row 1, column 2 of the matrix.
        /// </summary>
        [DataMember] public double M12;
        /// <summary>
        /// Value at row 1, column 3 of the matrix.
        /// </summary>
        [DataMember] public double M13;
        /// <summary>
        /// Value at row 1, column 4 of the matrix.
        /// </summary>
        [DataMember] public double M14;

        /// <summary>
        /// Value at row 2, column 1 of the matrix.
        /// </summary>
        [DataMember] public double M21;
        /// <summary>
        /// Value at row 2, column 2 of the matrix.
        /// </summary>
        [DataMember] public double M22;
        /// <summary>
        /// Value at row 2, column 3 of the matrix.
        /// </summary>
        [DataMember] public double M23;
        /// <summary>
        /// Value at row 2, column 4 of the matrix.
        /// </summary>
        [DataMember] public double M24;

        /// <summary>
        /// Value at row 3, column 1 of the matrix.
        /// </summary>
        [DataMember] public double M31;
        /// <summary>
        /// Value at row 3, column 2 of the matrix.
        /// </summary>
        [DataMember] public double M32;
        /// <summary>
        /// Value at row 3, column 3 of the matrix.
        /// </summary>
        [DataMember] public double M33;
        /// <summary>
        /// Value at row 3, column 4 of the matrix.
        /// </summary>
        [DataMember] public double M34;

        /// <summary>
        /// Value at row 4, column 1 of the matrix.
        /// </summary>
        [DataMember] public double M41;
        /// <summary>
        /// Value at row 4, column 2 of the matrix.
        /// </summary>
        [DataMember] public double M42;
        /// <summary>
        /// Value at row 4, column 3 of the matrix.
        /// </summary>
        [DataMember] public double M43;
        /// <summary>
        /// Value at row 4, column 4 of the matrix.
        /// </summary>
        [DataMember] public double M44;

        /// <summary>
        /// Returns the multiplicative identity matrix.
        /// </summary>
        public static DMatrix4x4 Identity = new DMatrix4x4
        (
            1.0, 0.0, 0.0, 0.0,
            0.0, 1.0, 0.0, 0.0,
            0.0, 0.0, 1.0, 0.0,
            0.0, 0.0, 0.0, 1.0
        );

        /// <summary>
        /// Gets the translation component of this matrix.
        /// </summary>
        public DVector3 Translation
            => new DVector3(M41, M42, M43);

        /// <summary>
        /// Sets the translation component of this matrix, returning a new Matrix
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DMatrix4x4 SetTranslation(DVector3 v)
            => CreateFromRows(Row0, Row1, Row2, v);

        /// <summary>
        /// Constructs a Matrix4x4 from the given components.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DMatrix4x4(double m11, double m12, double m13, double m14,
                          double m21, double m22, double m23, double m24,
                          double m31, double m32, double m33, double m34,
                          double m41, double m42, double m43, double m44)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M14 = m14;

            M21 = m21;
            M22 = m22;
            M23 = m23;
            M24 = m24;

            M31 = m31;
            M32 = m32;
            M33 = m33;
            M34 = m34;

            M41 = m41;
            M42 = m42;
            M43 = m43;
            M44 = m44;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 CreateFromRows(DVector3 row0, DVector3 row1, DVector3 row2)
            => CreateFromRows(row0.ToDVector4(), row1.ToDVector4(), row2.ToDVector4(), new DVector4(0, 0, 0, 1));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 CreateFromRows(DVector3 row0, DVector3 row1, DVector3 row2, DVector3 row3)
            => CreateFromRows(row0.ToDVector4(), row1.ToDVector4(), row2.ToDVector4(), new DVector4(row3.X, row3.Y, row3.Z, 1));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 CreateFromRows(DVector4 row0, DVector4 row1, DVector4 row2)
            => CreateFromRows(row0.ToDVector3(), row1.ToDVector3(), row2.ToDVector3(), DVector3.Zero);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 CreateFromRows(DVector4 row0, DVector4 row1, DVector4 row2, DVector4 row3)
            => new DMatrix4x4(row0.X, row0.Y, row0.Z, row0.W,
                row1.X, row1.Y, row1.Z, row1.W,
                row2.X, row2.Y, row2.Z, row2.W,
                row3.X, row3.Y, row3.Z, row3.W);

        /// <summary>
        /// Creates a translation matrix.
        /// </summary>
        /// <param name="position">The amount to translate in each axis.</param>
        /// <returns>The translation matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 CreateTranslation(DVector3 position)
        {
            DMatrix4x4 result;

            result.M11 = 1.0;
            result.M12 = 0.0;
            result.M13 = 0.0;
            result.M14 = 0.0;
            result.M21 = 0.0;
            result.M22 = 1.0;
            result.M23 = 0.0;
            result.M24 = 0.0;
            result.M31 = 0.0;
            result.M32 = 0.0;
            result.M33 = 1.0;
            result.M34 = 0.0;

            result.M41 = position.X;
            result.M42 = position.Y;
            result.M43 = position.Z;
            result.M44 = 1.0;

            return result;
        }

        /// <summary>
        /// Creates a translation matrix.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 CreateTranslation(double x, double y, double z)
            => CreateTranslation(new DVector3(x, y, z));

        /// <summary>
        /// Creates a scaling matrix.
        /// </summary>
        /// <param name="xScale">Value to scale by on the X-axis.</param>
        /// <param name="yScale">Value to scale by on the Y-axis.</param>
        /// <param name="zScale">Value to scale by on the Z-axis.</param>
        /// <returns>The scaling matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 CreateScale(double xScale, double yScale, double zScale)
        {
            DMatrix4x4 result;

            result.M11 = xScale;
            result.M12 = 0.0;
            result.M13 = 0.0;
            result.M14 = 0.0;
            result.M21 = 0.0;
            result.M22 = yScale;
            result.M23 = 0.0;
            result.M24 = 0.0;
            result.M31 = 0.0;
            result.M32 = 0.0;
            result.M33 = zScale;
            result.M34 = 0.0;
            result.M41 = 0.0;
            result.M42 = 0.0;
            result.M43 = 0.0;
            result.M44 = 1.0;

            return result;
        }

        /// <summary>
        /// Creates a scaling matrix with a center point.
        /// </summary>
        /// <param name="xScale">Value to scale by on the X-axis.</param>
        /// <param name="yScale">Value to scale by on the Y-axis.</param>
        /// <param name="zScale">Value to scale by on the Z-axis.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>The scaling matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 CreateScale(double xScale, double yScale, double zScale, DVector3 centerPoint)
        {
            DMatrix4x4 result;

            var tx = centerPoint.X * (1 - xScale);
            var ty = centerPoint.Y * (1 - yScale);
            var tz = centerPoint.Z * (1 - zScale);

            result.M11 = xScale;
            result.M12 = 0.0;
            result.M13 = 0.0;
            result.M14 = 0.0;
            result.M21 = 0.0;
            result.M22 = yScale;
            result.M23 = 0.0;
            result.M24 = 0.0;
            result.M31 = 0.0;
            result.M32 = 0.0;
            result.M33 = zScale;
            result.M34 = 0.0;
            result.M41 = tx;
            result.M42 = ty;
            result.M43 = tz;
            result.M44 = 1.0;

            return result;
        }

        /// <summary>
        /// Creates a scaling matrix.
        /// </summary>
        /// <param name="scales">The vector containing the amount to scale by on each axis.</param>
        /// <returns>The scaling matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 CreateScale(DVector3 scales)
            => CreateScale(scales.X, scales.Y, scales.Z);

        /// <summary>
        /// Creates a scaling matrix with a center point.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 CreateScale(DVector3 scales, DVector3 centerPoint)
            => CreateScale(scales.X, scales.Y, scales.Z, centerPoint);

        /// <summary>
        /// Creates a uniform scaling matrix that scales equally on each axis.
        /// </summary>
        /// <param name="scale">The uniform scaling factor.</param>
        /// <returns>The scaling matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 CreateScale(double scale)
            => CreateScale(scale, scale, scale);

        /// <summary>
        /// Creates a uniform scaling matrix that scales equally on each axis with a center point.
        /// </summary>
        /// <param name="scale">The uniform scaling factor.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>The scaling matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 CreateScale(double scale, DVector3 centerPoint)
            => CreateScale(scale, scale, scale, centerPoint);

        /// <summary>
        /// Transposes the rows and columns of a matrix.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 Transpose(DMatrix4x4 matrix)
        {
            DMatrix4x4 result;
            result.M11 = matrix.M11;
            result.M12 = matrix.M21;
            result.M13 = matrix.M31;
            result.M14 = matrix.M41;
            result.M21 = matrix.M12;
            result.M22 = matrix.M22;
            result.M23 = matrix.M32;
            result.M24 = matrix.M42;
            result.M31 = matrix.M13;
            result.M32 = matrix.M23;
            result.M33 = matrix.M33;
            result.M34 = matrix.M43;
            result.M41 = matrix.M14;
            result.M42 = matrix.M24;
            result.M43 = matrix.M34;
            result.M44 = matrix.M44;
            return result;
        }

        /// <summary>
        /// Transposes the rows and columns of a matrix.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DMatrix4x4 Transpose()
            => Transpose(this);

        /// <summary>
        /// Returns a new matrix with the negated elements of the given matrix.
        /// </summary>
        /// <param name="value">The source matrix.</param>
        /// <returns>The negated matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 Negate(DMatrix4x4 value) => -value;

        /// <summary>
        /// Adds two matrices together.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 Add(DMatrix4x4 value1, DMatrix4x4 value2) => value1 + value2;

        /// <summary>
        /// Subtracts the second matrix from the first.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 Subtract(DMatrix4x4 value1, DMatrix4x4 value2) => value1 - value2;

        /// <summary>
        /// Multiplies a matrix by another matrix.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 Multiply(DMatrix4x4 value1, DMatrix4x4 value2) => value1 * value2;

        /// <summary>
        /// Multiplies a matrix by a scalar value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 Multiply(DMatrix4x4 value1, double value2) => value1 * value2;

        /// <summary>
        /// Returns a new matrix with the negated elements of the given matrix.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 operator -(DMatrix4x4 value)
        {
            DMatrix4x4 m;

            m.M11 = -value.M11;
            m.M12 = -value.M12;
            m.M13 = -value.M13;
            m.M14 = -value.M14;
            m.M21 = -value.M21;
            m.M22 = -value.M22;
            m.M23 = -value.M23;
            m.M24 = -value.M24;
            m.M31 = -value.M31;
            m.M32 = -value.M32;
            m.M33 = -value.M33;
            m.M34 = -value.M34;
            m.M41 = -value.M41;
            m.M42 = -value.M42;
            m.M43 = -value.M43;
            m.M44 = -value.M44;

            return m;
        }

        /// <summary>
        /// Adds two matrices together.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The resulting matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 operator +(DMatrix4x4 value1, DMatrix4x4 value2)
        {
            DMatrix4x4 m;

            m.M11 = value1.M11 + value2.M11;
            m.M12 = value1.M12 + value2.M12;
            m.M13 = value1.M13 + value2.M13;
            m.M14 = value1.M14 + value2.M14;
            m.M21 = value1.M21 + value2.M21;
            m.M22 = value1.M22 + value2.M22;
            m.M23 = value1.M23 + value2.M23;
            m.M24 = value1.M24 + value2.M24;
            m.M31 = value1.M31 + value2.M31;
            m.M32 = value1.M32 + value2.M32;
            m.M33 = value1.M33 + value2.M33;
            m.M34 = value1.M34 + value2.M34;
            m.M41 = value1.M41 + value2.M41;
            m.M42 = value1.M42 + value2.M42;
            m.M43 = value1.M43 + value2.M43;
            m.M44 = value1.M44 + value2.M44;

            return m;
        }

        /// <summary>
        /// Subtracts the second matrix from the first.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The result of the subtraction.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 operator -(DMatrix4x4 value1, DMatrix4x4 value2)
        {
            DMatrix4x4 m;

            m.M11 = value1.M11 - value2.M11;
            m.M12 = value1.M12 - value2.M12;
            m.M13 = value1.M13 - value2.M13;
            m.M14 = value1.M14 - value2.M14;
            m.M21 = value1.M21 - value2.M21;
            m.M22 = value1.M22 - value2.M22;
            m.M23 = value1.M23 - value2.M23;
            m.M24 = value1.M24 - value2.M24;
            m.M31 = value1.M31 - value2.M31;
            m.M32 = value1.M32 - value2.M32;
            m.M33 = value1.M33 - value2.M33;
            m.M34 = value1.M34 - value2.M34;
            m.M41 = value1.M41 - value2.M41;
            m.M42 = value1.M42 - value2.M42;
            m.M43 = value1.M43 - value2.M43;
            m.M44 = value1.M44 - value2.M44;

            return m;
        }

        /// <summary>
        /// Multiplies a matrix by another matrix.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The result of the multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 operator *(DMatrix4x4 value1, DMatrix4x4 value2)
        {

            DMatrix4x4 m;

            // First row
            m.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21 + value1.M13 * value2.M31 + value1.M14 * value2.M41;
            m.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22 + value1.M13 * value2.M32 + value1.M14 * value2.M42;
            m.M13 = value1.M11 * value2.M13 + value1.M12 * value2.M23 + value1.M13 * value2.M33 + value1.M14 * value2.M43;
            m.M14 = value1.M11 * value2.M14 + value1.M12 * value2.M24 + value1.M13 * value2.M34 + value1.M14 * value2.M44;

            // Second row
            m.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21 + value1.M23 * value2.M31 + value1.M24 * value2.M41;
            m.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22 + value1.M23 * value2.M32 + value1.M24 * value2.M42;
            m.M23 = value1.M21 * value2.M13 + value1.M22 * value2.M23 + value1.M23 * value2.M33 + value1.M24 * value2.M43;
            m.M24 = value1.M21 * value2.M14 + value1.M22 * value2.M24 + value1.M23 * value2.M34 + value1.M24 * value2.M44;

            // Third row
            m.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value1.M33 * value2.M31 + value1.M34 * value2.M41;
            m.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value1.M33 * value2.M32 + value1.M34 * value2.M42;
            m.M33 = value1.M31 * value2.M13 + value1.M32 * value2.M23 + value1.M33 * value2.M33 + value1.M34 * value2.M43;
            m.M34 = value1.M31 * value2.M14 + value1.M32 * value2.M24 + value1.M33 * value2.M34 + value1.M34 * value2.M44;

            // Fourth row
            m.M41 = value1.M41 * value2.M11 + value1.M42 * value2.M21 + value1.M43 * value2.M31 + value1.M44 * value2.M41;
            m.M42 = value1.M41 * value2.M12 + value1.M42 * value2.M22 + value1.M43 * value2.M32 + value1.M44 * value2.M42;
            m.M43 = value1.M41 * value2.M13 + value1.M42 * value2.M23 + value1.M43 * value2.M33 + value1.M44 * value2.M43;
            m.M44 = value1.M41 * value2.M14 + value1.M42 * value2.M24 + value1.M43 * value2.M34 + value1.M44 * value2.M44;

            return m;
        }

        /// <summary>
        /// Multiplies a matrix by a scalar value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DMatrix4x4 operator *(DMatrix4x4 value1, double value2)
        {
            DMatrix4x4 m;

            m.M11 = value1.M11 * value2;
            m.M12 = value1.M12 * value2;
            m.M13 = value1.M13 * value2;
            m.M14 = value1.M14 * value2;
            m.M21 = value1.M21 * value2;
            m.M22 = value1.M22 * value2;
            m.M23 = value1.M23 * value2;
            m.M24 = value1.M24 * value2;
            m.M31 = value1.M31 * value2;
            m.M32 = value1.M32 * value2;
            m.M33 = value1.M33 * value2;
            m.M34 = value1.M34 * value2;
            m.M41 = value1.M41 * value2;
            m.M42 = value1.M42 * value2;
            m.M43 = value1.M43 * value2;
            m.M44 = value1.M44 * value2;
            return m;
        }

        /// <summary>
        /// Returns a boolean indicating whether the given two matrices are equal.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(DMatrix4x4 value1, DMatrix4x4 value2)
            => (value1.M11 == value2.M11 && value1.M22 == value2.M22 && value1.M33 == value2.M33 && value1.M44 == value2.M44 && // Check diagonal element first for early out.
                value1.M12 == value2.M12 && value1.M13 == value2.M13 && value1.M14 == value2.M14 && value1.M21 == value2.M21 &&
                value1.M23 == value2.M23 && value1.M24 == value2.M24 && value1.M31 == value2.M31 && value1.M32 == value2.M32 &&
                value1.M34 == value2.M34 && value1.M41 == value2.M41 && value1.M42 == value2.M42 && value1.M43 == value2.M43);

        /// <summary>
        /// Returns a boolean indicating whether the given two matrices are not equal.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(DMatrix4x4 value1, DMatrix4x4 value2)
            => (value1.M11 != value2.M11 || value1.M12 != value2.M12 || value1.M13 != value2.M13 || value1.M14 != value2.M14 ||
                value1.M21 != value2.M21 || value1.M22 != value2.M22 || value1.M23 != value2.M23 || value1.M24 != value2.M24 ||
                value1.M31 != value2.M31 || value1.M32 != value2.M32 || value1.M33 != value2.M33 || value1.M34 != value2.M34 ||
                value1.M41 != value2.M41 || value1.M42 != value2.M42 || value1.M43 != value2.M43 || value1.M44 != value2.M44);

        /// <summary>
        /// Returns a boolean indicating whether this matrix instance is equal to the other given matrix.
        /// </summary>
        /// <param name="other">The matrix to compare this instance to.</param>
        /// <returns>True if the matrices are equal; False otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(DMatrix4x4 other) => this == other;

        /// <summary>
        /// Returns a boolean indicating whether this matrix instance is equal to the other given matrix within a given tolerance value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AlmostEquals(DMatrix4x4 other, float tolerance = Constants.Tolerance)
            => M11.AlmostEquals(other.M11, tolerance) &&
               M12.AlmostEquals(other.M12, tolerance) &&
               M13.AlmostEquals(other.M13, tolerance) &&
               M14.AlmostEquals(other.M14, tolerance) &&
               M21.AlmostEquals(other.M21, tolerance) &&
               M22.AlmostEquals(other.M22, tolerance) &&
               M23.AlmostEquals(other.M23, tolerance) &&
               M24.AlmostEquals(other.M24, tolerance) &&
               M31.AlmostEquals(other.M31, tolerance) &&
               M32.AlmostEquals(other.M32, tolerance) &&
               M33.AlmostEquals(other.M33, tolerance) &&
               M34.AlmostEquals(other.M34, tolerance) &&
               M41.AlmostEquals(other.M41, tolerance) &&
               M42.AlmostEquals(other.M42, tolerance) &&
               M43.AlmostEquals(other.M43, tolerance) &&
               M44.AlmostEquals(other.M44, tolerance);

        /// <summary>
        /// Returns a boolean indicating whether the given Object is equal to this matrix instance.
        /// </summary>
        /// <param name="obj">The Object to compare against.</param>
        /// <returns>True if the Object is equal to this matrix; False otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => (obj is DMatrix4x4 other) && (this == other);

        /// <summary>
        /// Returns a String representing this matrix instance.
        /// </summary>
        /// <returns>The string representation.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            var ci = CultureInfo.CurrentCulture;

            return string.Format(ci, "{{ {{M11:{0} M12:{1} M13:{2} M14:{3}}} {{M21:{4} M22:{5} M23:{6} M24:{7}}} {{M31:{8} M32:{9} M33:{10} M34:{11}}} {{M41:{12} M42:{13} M43:{14} M44:{15}}} }}",
                                 M11.ToString(ci), M12.ToString(ci), M13.ToString(ci), M14.ToString(ci),
                                 M21.ToString(ci), M22.ToString(ci), M23.ToString(ci), M24.ToString(ci),
                                 M31.ToString(ci), M32.ToString(ci), M33.ToString(ci), M34.ToString(ci),
                                 M41.ToString(ci), M42.ToString(ci), M43.ToString(ci), M44.ToString(ci));
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            unchecked
            {
                return M11.GetHashCode() + M12.GetHashCode() + M13.GetHashCode() + M14.GetHashCode() +
                       M21.GetHashCode() + M22.GetHashCode() + M23.GetHashCode() + M24.GetHashCode() +
                       M31.GetHashCode() + M32.GetHashCode() + M33.GetHashCode() + M34.GetHashCode() +
                       M41.GetHashCode() + M42.GetHashCode() + M43.GetHashCode() + M44.GetHashCode();
            }
        }

        public Matrix4x4 ToMatrix4x4()
        {
            return new Matrix4x4(
                (float) M11, (float) M12, (float) M13, (float) M14,
                (float) M21, (float) M22, (float) M23, (float) M24,
                (float) M31, (float) M32, (float) M33, (float) M34,
                (float) M41, (float) M42, (float) M43, (float) M44);
        }
    }
}
