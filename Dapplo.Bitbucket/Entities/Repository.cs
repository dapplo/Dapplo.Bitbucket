using System.Runtime.Serialization;

namespace Dapplo.Bitbucket.Entities {
	[DataContract]
	public class Repository {
		public Repository() {
			ScmId = "git";
		}
		[DataMember(Name = "slug", EmitDefaultValue = false)]
		public string Slug {
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
		[DataMember(Name = "scmId", EmitDefaultValue = false)]
		public string ScmId {
			get;
			set;
		}
		[DataMember(Name = "state", EmitDefaultValue = false)]
		public string State {
			get;
			set;
		}
		[DataMember(Name = "statusMessage", EmitDefaultValue = false)]
		public string StatusMessage {
			get;
			set;
		}
		[DataMember(Name = "forkable", EmitDefaultValue = false)]
		public bool IsForkable {
			get;
			set;
		}
		[DataMember(Name = "project", EmitDefaultValue = false)]
		public Project Project {
			get;
			set;
		}
		[DataMember(Name = "public", EmitDefaultValue = false)]
		public bool IsPublic {
			get;
			set;
		}
		[DataMember(Name = "cloneUrl", EmitDefaultValue = false)]
		public string CloneUrl {
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
