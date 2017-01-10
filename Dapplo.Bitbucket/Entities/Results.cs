using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Dapplo.Bitbucket.Entities
{
	[DataContract]
	public class Results<TResult> : IEnumerable<TResult>
	{
		[DataMember(Name = "size", EmitDefaultValue = false)]
		public int Size { get; set; }

		[DataMember(Name = "limit", EmitDefaultValue = false)]
		public int Limit { get; set; }

		[DataMember(Name = "isLastPage", EmitDefaultValue = false)]
		public bool IsLastPage { get; set; }

		[DataMember(Name = "values", EmitDefaultValue = false)]
		public IList<TResult> Values { get; set; }

		[DataMember(Name = "start", EmitDefaultValue = false)]
		public int Start { get; set; }

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<TResult> GetEnumerator()
		{
			return Values.GetEnumerator();
		}
	}
}
