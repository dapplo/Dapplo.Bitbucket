using System;
using System.Runtime.Serialization;

namespace Dapplo.Bitbucket.Entities
{
	[DataContract]
	public class BuildState {
		[DataMember(Name = "state", EmitDefaultValue = false)]
		public StashBuildState State {
			get;
			set;
		}
		[DataMember(Name = "key", EmitDefaultValue = false)]
		public string Key {
			get;
			set;
		}
		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name {
			get;
			set;
		}
		[DataMember(Name = "url", EmitDefaultValue = false)]
		public string Url {
			get;
			set;
		}
		[DataMember(Name = "description", EmitDefaultValue = false)]
		public string Description {
			get;
			set;
		}
		[DataMember(Name = "dateAdded", EmitDefaultValue = false)]
		public string DateAdded {
			get;
			set;
		}
	}
}