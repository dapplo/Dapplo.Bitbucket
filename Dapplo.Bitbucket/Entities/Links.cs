using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Dapplo.Bitbucket.Entities
{
	[DataContract]
	public class Links {
		[DataMember(Name = "self", EmitDefaultValue = false)]
		public IList<Self> Selfs {
			get;
			set;
		}
	}
}