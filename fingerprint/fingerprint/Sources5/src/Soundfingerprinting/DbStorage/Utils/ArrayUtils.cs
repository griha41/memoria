﻿// Sound Fingerprinting framework
// git://github.com/AddictedCS/soundfingerprinting.git
// Code license: CPOL v.1.02
// ciumac.sergiu@gmail.com
using System;
using System.Collections;

namespace Soundfingerprinting.DbStorage.Utils
{
    /// <summary>
    ///   Array Utilities class
    /// </summary>
    public static class ArrayUtils
    {
        /// <summary>
        ///   Get Float[] from a Byte[]
        /// </summary>
        /// <param name = "array">Byte[] to convert</param>
        /// <returns>Float[] of the same size</returns>
        public static float[] GetFloatArrayFromByte(byte[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("array cannot be null or empty");
            float[] result = new float[array.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = unchecked((sbyte) (array[i]));
            }
            return result;
        }

        /// <summary>
        ///   Get Float[] from a Byte[]
        /// </summary>
        /// <param name = "array">Byte[] to convert</param>
        /// <returns>Float[] of the same size</returns>
        public static float[] GetFloatArrayFromBool(bool[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("array cannot be null or empty");
            float[] result = new float[array.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = array[i] ? 1 : 0;
            }
            return result;
        }

        /// <summary>
        ///   Get float [] from sbyte[]
        /// </summary>
        /// <param name = "array">Array to convert</param>
        /// <returns></returns>
        public static float[] GetFloatArrayFromSByte(sbyte[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("array cannot be null or empty");
            float[] result = new float[array.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = array[i];
            }
            return result;
        }

        /// <summary>
        ///   Get byte array from it's corresponding float
        /// </summary>
        /// <param name = "array">Fingerprint array in float representation</param>
        /// <returns>Byte associate</returns>
        public static byte[] GetByteArrayFromFloat(float[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("array cannot be null or empty");
            byte[] result = new byte[array.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = unchecked((byte) (array[i]));
            }
            return result;
        }

        /// <summary>
        ///   Get sbyte [] from float []
        /// </summary>
        /// <param name = "array">Array to convert</param>
        /// <returns>Sbyte array</returns>
        public static sbyte[] GetSByteArrayFromFloat(float[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("array cannot be null or empty");
            sbyte[] result = new sbyte[array.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = unchecked((sbyte) (array[i]));
            }
            return result;
        }

        /// <summary>
        ///   Get sbyte [] from byte [] [memory hack is used, unsafe method]
        /// </summary>
        /// <param name = "array">Array to convert</param>
        /// <returns>sbyte [] array</returns>
        public static sbyte[] GetSByteArrayFromByte(byte[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("array cannot be null or empty");
            sbyte[] result = new sbyte[array.Length];
            Buffer.BlockCopy(array, 0, result, 0, array.Length);
            return result;
        }

        /// <summary>
        ///   Get byte[] from sbyte[] [memory hack is used, unsafe method]
        /// </summary>
        /// <param name = "array">Array to convert</param>
        /// <returns>Byte array</returns>
        public static byte[] GetByteArrayFromSByte(sbyte[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("array cannot be null or empty");
            byte[] result = new byte[array.Length];
            Buffer.BlockCopy(array, 0, result, 0, array.Length);
            return result;
        }

        /// <summary>
        ///   Get bytes array from Boolean values.
        /// </summary>
        /// <param name = "array">Array to be packed</param>
        /// <returns>Bytes array</returns>
        public static byte[] GetByteArrayFromBool(bool[] array)
        {
            if (array.Length%8 != 0)
                throw new ArgumentException("array, copying this array will result in an inaccurate representation of bytes");
            BitArray b = new BitArray(array);
            byte[] bytesArr = new byte[array.Length/8];
            b.CopyTo(bytesArr, 0);
            return bytesArr;
        }

        /// <summary>
        ///   Get double [] from sbyte[]
        /// </summary>
        /// <param name = "array">Array to convert</param>
        /// <returns>Double array</returns>
        public static double[] GetDoubleArrayFromSByte(sbyte[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("array cannot be null or empty");
            double[] result = new double[array.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = array[i];
            }
            return result;
        }

        /// <summary>
        ///   Get Double[] array from Byte[] of samples, read from file
        /// </summary>
        /// <param name = "array">Byte[] array to convert [containing # of samples specified in the next parameter]</param>
        /// <param name = "samplesRead">Number of samples contained within byte[]</param>
        /// <param name = "silence">Set to true if array contains silece [sum = 0]</param>
        /// <returns>Double [] of specific values</returns>
        public static float[] GetDoubleArrayFromSamples(byte[] array, int samplesRead, ref bool silence)
        {
            float[] result = null;
            double sum = 0;
            switch (array.Length/samplesRead) /*1 ,2, 4 */
            {
                case 1: /*10240 samples at 8 BitPerSample rate: 1 sample = 8 bits = 1 byte*/
                    result = new float[array.Length];
                    for (int i = 0; i < array.Length; i++)
                    {
                        sum += result[i] = unchecked((sbyte) (array[i]));
                    }
                    break;
                case 2:
                    /*10240 samples read at 16 BitPerSample rate: 1 sample = 16 bits = 2 byte*/
                    result = new float[array.Length/2];
                    for (int i = 0; i < result.Length; i++)
                    {
                        sum += result[i] = BitConverter.ToInt16(array, i*2); /*2 byte in 1 short*/
                    }
                    break;
                case 4:
                    result = new float[array.Length/4];
                    /*10240 samples read at 32 BitPerSample rate: 1 sample = 32 bits = 4 bytes*/
                    for (int i = 0; i < result.Length; i++)
                    {
                        sum += result[i] = BitConverter.ToInt32(array, i*4); /*2 byte in 1 short*/
                    }
                    break;
                default:
                    throw new ArgumentException("Samples read do not correspond to bit rate");
            }
            silence = sum == 0;
            return result;
        }

        /// <summary>
        ///   Get alligned byte [] from float []
        /// </summary>
        /// <param name = "array">Array to convert</param>
        /// <returns>Byte array</returns>
        public static byte[] GetAllignedByteArrayFromFloat(float[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("array cannot be null or empty");
            int size = sizeof (float);
            byte[] result = new byte[array.Length*size];

            for (int i = 0, n = array.Length; i < n; i++)
            {
                byte[] temp = BitConverter.GetBytes(array[i]);
                Array.Copy(temp, 0, result, i*size, size);
            }
            return result;
        }

        /// <summary>
        ///   Get Alligned float [] from byte[]
        /// </summary>
        /// <param name = "array">Array to convert</param>
        /// <returns>Float array</returns>
        public static float[] GetAllignedFloatArrayFromByte(byte[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("array cannot be null or empty");
            const int size = sizeof (float);
            float[] result = new float[array.Length/size];

            for (int i = 0, n = array.Length/size; i < n; i++)
            {
                result[i] = BitConverter.ToSingle(array, i*size);
            }
            return result;
        }

        /// <summary>
        ///   Get float [] from double []
        /// </summary>
        /// <param name = "array">Array to convert</param>
        /// <returns>Float array</returns>
        public static float[] GetFloatArrayFromDouble(double[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("array cannot be null or empty");
            float[] result = new float[array.Length];

            for (int i = 0, n = array.Length; i < n; i++)
            {
                result[i] = unchecked((float) array[i]);
            }
            return result;
        }
    }
}