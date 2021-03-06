﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Dapplo.Bitbucket.Entities
{
    /// <summary>
    /// Results
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    [DataContract]
    public class Results<TResult> : PagingInfo, IEnumerable<TResult>
    {
        /// <summary>
        /// The size of the result
        /// </summary>
        [DataMember(Name = "size", EmitDefaultValue = false)]
        public int Size { get; set; }

        /// <summary>
        /// The starting position of the result
        /// </summary>
        [DataMember(Name = "start", EmitDefaultValue = false)]
        public int Start { get; set; }

        /// <summary>
        /// The values of the result
        /// </summary>
        [DataMember(Name = "values", EmitDefaultValue = false)]
        public IList<TResult> Values { get; set; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Implementation of GetEnumerator
        /// </summary>
        /// <returns>IEnumerator with items of type TResult</returns>
        public IEnumerator<TResult> GetEnumerator()
        {
            return Values.GetEnumerator();
        }
    }
}
