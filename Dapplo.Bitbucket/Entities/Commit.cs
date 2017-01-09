using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Dapplo.Bitbucket.Entities {
	[DataContract]
	public class Commit {
		[DataMember(Name = "id", EmitDefaultValue = false)]
		public string Id {
			get;
			set;
		}
		[DataMember(Name = "displayId", EmitDefaultValue = false)]
		public string DisplayId {
			get;
			set;
		}
		[DataMember(Name = "author", EmitDefaultValue = false)]
		public Author Author {
			get;
			set;
		}
		[DataMember(Name = "authorTimestamp", EmitDefaultValue = false)]
		public long AuthorTimestamp {
			get;
			set;
		}
		[DataMember(Name = "message", EmitDefaultValue = false)]
		public string Message {
			get;
			set;
		}
		[DataMember(Name = "parents", EmitDefaultValue = false)]
		public IList<Parent> Parents {
			get;
			set;
		}
	}
}
