﻿// Sound Fingerprinting framework
// git://github.com/AddictedCS/soundfingerprinting.git
// Code license: CPOL v.1.02
// ciumac.sergiu@gmail.com
using System;
using System.Linq;
using Soundfingerprinting.Fingerprinting.FFT;

namespace Soundfingerprinting.Fingerprinting.ConstantQ
{
    /// <summary>
    ///   Class for constant Q transformation
    /// </summary>
    public class ConstQ
    {
        /// <summary>
        ///   Get constant q transformation coefficients
        /// </summary>
        /// <param name = "x">Initial signal</param>
        /// <param name = "sparKernel">Spar kernel</param>
        /// <returns>Constant q transform</returns>
        /// <remarks>
        ///   Method rewritten from Matlab implementation
        ///   http://wwwmath.uni-muenster.de/logik/Personen/blankertz/constQ/constQ.html
        /// </remarks>
        /*
         * function cq= constQ(x, sparKernel) % x must be a row vector
         * cq= fft(x,size(sparKernel,1)) * sparKernel;
         */
        public double[] GetTransform(Complex[] x, Complex[][] sparKernel)
        {
            int signalLength = x.Length;
            int sparLength = sparKernel[0].Length;
            if (signalLength != sparLength)
                throw new ArgumentException("x and sparKernel dimensions are not equal");
            Fourier.FFT(x, x.Length, FourierDirection.Forward);
            int logBins = sparKernel.GetLength(0);
            Complex[] cq = new Complex[logBins];

            for (int i = 0; i < logBins; i++)
            {
                for (int j = 0; j < signalLength /*2048*/; j++)
                {
                    cq[i].Re += x[j].Re*sparKernel[i][j].Re - x[j].Im*sparKernel[i][j].Im;
                    cq[i].Im += x[j].Re*sparKernel[i][j].Im + x[j].Im*sparKernel[i][j].Re;
                }
            }
            return cq.Select((item) => 20*Math.Log10(item.GetModulus()/FingerprintManager.HUMAN_AUDITORY_THRESHOLD)).ToArray();
        }
    }
}