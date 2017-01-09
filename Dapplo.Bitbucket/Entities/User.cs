using System.Runtime.Serialization;

namespace Dapplo.Bitbucket.Entities {
	[DataContract]
	public class User {
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
		[DataMember(Name = "id", EmitDefaultValue = false)]
		public int Id {
			get;
			set;
		}
		[DataMember(Name = "displayName", EmitDefaultValue = false)]
		public string DisplayName {
			get;
			set;
		}
		[DataMember(Name = "active", EmitDefaultValue = false)]
		public bool Active {
			get;
			set;
		}
		[DataMember(Name = "slug", EmitDefaultValue = false)]
		public string Slug {
			get;
			set;
		}
		[DataMember(Name = "type", EmitDefaultValue = false)]
		public string Type {
			get;
			set;
		}

	}
}
