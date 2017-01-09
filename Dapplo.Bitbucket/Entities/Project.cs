using System.Runtime.Serialization;

namespace Dapplo.Bitbucket.Entities {
	[DataContract]
	public class Project {
		[DataMember(Name = "key", EmitDefaultValue = false)]
		public string Key {
			get;
			set;
		}
		[DataMember(Name = "id", EmitDefaultValue = false)]
		public int Id {
			get;
			set;
		}
		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name {
			get;
			set;
		}
		[DataMember(Name = "description", EmitDefaultValue = false)]
		public string Description {
			get;
			set;
		}
		[DataMember(Name = "public", EmitDefaultValue = false)]
		public bool IsPublic {
			get;
			set;
		}
		[DataMember(Name = "type", EmitDefaultValue = false)]
		public string Type {
			get;
			set;
		}
		[DataMember(Name = "link", EmitDefaultValue = false)]
		public Link Link {
			get;
			set;
		}
		[DataMember(Name = "links", EmitDefaultValue = false)]
		public Links Links {
			get;
			set;
		}
	}
}
