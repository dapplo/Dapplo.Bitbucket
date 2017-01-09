using System.Runtime.Serialization;

namespace Dapplo.Bitbucket.Entities
{
	[DataContract]
	public class Author {
		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name {
			get;
			set;
		}
		[DataMember(Name = "emailAddress", EmitDefaultValue = false)]
		public string EmailAddress {
			get;
			set;
		}
	}
}