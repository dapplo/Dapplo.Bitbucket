using System.Runtime.Serialization;

namespace Dapplo.Bitbucket.Entities {
	[DataContract]
	public class Branch {
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
		[DataMember(Name = "latestChangeset", EmitDefaultValue = false)]
		public string LatestChangeset {
			get;
			set;
		}
		[DataMember(Name = "isDefault", EmitDefaultValue = false)]
		public bool IsDefault {
			get;
			set;
		}
	}
}
