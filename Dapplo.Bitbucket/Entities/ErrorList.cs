using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Dapplo.Bitbucket.Entities
{
	[DataContract]
	public class ErrorList {
		[DataMember(Name = "errors", EmitDefaultValue = false)]
		public IList<Error> Errors {
			get;
			set;
		}
	}
}