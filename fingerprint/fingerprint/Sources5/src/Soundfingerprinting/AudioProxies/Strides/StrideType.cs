﻿// Sound Fingerprinting framework
// git://github.com/AddictedCS/soundfingerprinting.git
// Code license: CPOL v.1.02
// ciumac.sergiu@gmail.com
namespace Soundfingerprinting.AudioProxies.Strides
{
    /// <summary>
    ///   Types of strides
    /// </summary>
    public enum StrideType
    {
        /// <summary>
        ///   Static stride between 2 consecutive fingerprints
        /// </summary>
        Static,

        /// <summary>
        ///   Random stride between 2 consecutive fingerprints
        /// </summary>
        Random,

        /// <summary>
        ///   Incremental static stride between start of 2 consecutive fingerprints
        /// </summary>
        IncrementalStatic,

        /// <summary>
        ///   Incremental random stride between start of 2 consecutive fingerprints
        /// </summary>
        IncrementalRandom
    }
}