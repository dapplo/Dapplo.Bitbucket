using System.Runtime.Serialization;

namespace Dapplo.Bitbucket.Entities {
	[DataContract]
	public class Link {
		[DataMember(Name = "url", EmitDefaultValue = false)]
		public string Url {
			get;
			set;
		}
		[DataMember(Name = "rel", EmitDefaultValue = false)]
		public string Rel {
			get;
			set;
		}
	}
}
